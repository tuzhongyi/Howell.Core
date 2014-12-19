using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Howell.ComponentModel
{

    /// <summary>
    /// 事件模型的异步操作实现类型 抽象类
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    public abstract class AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// TaskIds
        /// </summary>
        protected HybridDictionary _taskIdToLifetime = new HybridDictionary();
        /// <summary>
        /// Completed Delegate
        /// </summary>
        protected System.Threading.SendOrPostCallback _onCompletedDelegate;
        /// <summary>
        /// 默认构造
        /// </summary>
        protected AsyncOperationEventBasedSkeleton()
        {
            _onCompletedDelegate = new System.Threading.SendOrPostCallback(OnCompleted);
        }
        /// <summary>
        /// Completion
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="canceled"></param>
        /// <param name="asyncOp"></param>
        /// <param name="args"></param>
        protected void Completion(Exception exception, Boolean canceled, AsyncOperation asyncOp, params object[] args)
        {
            // If the task was not previously canceled,
            // remove the task from the lifetime collection.
            if (!canceled)
            {
                lock (_taskIdToLifetime.SyncRoot)
                {
                    _taskIdToLifetime.Remove(asyncOp.UserSuppliedState);
                }
            }
            // Package the results of the operation in a 
            // DeviceConnectionCompletedEventArgs.
            List<Object> instanceArgs = new List<Object>();
            instanceArgs.Add(exception);
            instanceArgs.Add(canceled);
            instanceArgs.Add(asyncOp.UserSuppliedState);
            if (args != null)
            {
                foreach (var arg in args)
                {
                    instanceArgs.Add(arg);
                }
            }
            Object e = Activator.CreateInstance(typeof(TCompletedEventArgs), instanceArgs.ToArray());
            // End the task. The asyncOp object is responsible 
            // for marshaling the call.
            asyncOp.PostOperationCompleted(_onCompletedDelegate, e);
            // Note that after the call to OperationCompleted, 
            // asyncOp is no longer usable, and any attempt to use it
            // will cause an exception to be thrown.
        }
        /// <summary>
        /// 打开操作被取消
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        protected Boolean IsCancelled(Object taskId)
        {
            return (_taskIdToLifetime[taskId] == null);
        }
        /// <summary>
        /// 未使用
        /// </summary>
        /// <param name="asyncOp"></param>
        /// <param name="action"></param>
        /// <param name="args"></param>
        protected void WorkerFunction(AsyncOperation asyncOp, Action action, params Object[] args)
        {

            Exception e = null;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, args);
        }
        /// <summary>
        /// OnCompleted
        /// </summary>
        /// <param name="operationState"></param>
        private void OnCompleted(object operationState)
        {
            TCompletedEventArgs e = operationState as TCompletedEventArgs;
            if (Completed != null)
            {
                try
                {
                    Completed(this, e);
                }
                catch { }
            }
        }
        /// <summary>
        /// 构造函数签名创建工具
        /// </summary>
        /// <param name="types">参数类型表</param>
        /// <returns></returns>
        protected String CreateConstructedFunctionSignature(params Type[] types)
        {
            String message = "";
            message += "(";           
            if (types != null)
            {
                foreach (var arg in types)
                {
                    if (message == "(")
                    {
                        message += arg.Name;
                    }
                    else
                    {
                        message += ", " + arg.Name;
                    }
                }
            }
            message += ");";
            return message;
        }
        /// <summary>
        /// 验证泛型构造的有效性
        /// </summary>
        /// <param name="signature">构造函数签名</param>
        /// <param name="args">构造参数列表</param>
        protected void ValidateCompletedEventArgs(String signature, params Object[] args)
        {
            try
            {
                Activator.CreateInstance(typeof(TCompletedEventArgs), args.ToArray());
            }
            catch (Exception ex)
            {

                throw new InvalidCastException(String.Format("Type {0} constructor is not {1}.", typeof(TCompletedEventArgs).FullName, signature), ex);
            }
        }
        /// <summary>
        /// 开始异步操作
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        protected AsyncOperation BeginAsync(object taskId)
        {
            // Create an AsyncOperation for taskId.
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(taskId);
            // Multiple threads will access the task dictionary,
            // so it must be locked to serialize access.
            lock (_taskIdToLifetime.SyncRoot)
            {
                if (_taskIdToLifetime.Contains(taskId))
                {
                    throw new ArgumentException(
                        "Task ID parameter must be unique",
                        "taskId");
                }
                _taskIdToLifetime[taskId] = asyncOp;
            }
            return asyncOp;
        }
        /// <summary>
        /// 异步操作完成事件
        /// </summary>
        public event EventHandler<TCompletedEventArgs> Completed;
        /// <summary>
        /// 取消异步操作
        /// </summary>
        /// <param name="taskId"></param>
        public virtual void CancelAsync(object taskId)
        {
            AsyncOperation asyncOp = _taskIdToLifetime[taskId] as AsyncOperation;
            if (asyncOp != null)
            {
                lock (_taskIdToLifetime.SyncRoot)
                {
                    _taskIdToLifetime.Remove(taskId);
                }
            }
        }
    }

}
