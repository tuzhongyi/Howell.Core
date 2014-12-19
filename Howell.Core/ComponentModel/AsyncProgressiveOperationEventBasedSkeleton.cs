using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Howell.ComponentModel
{

    /// <summary>
    /// 事件模型的异步步进操作实现类型 抽象类
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    public abstract class AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs, ProgressChangedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        protected  AsyncProgressiveOperationEventBasedSkeleton()
            : base()
        {
        }
    }

    /// <summary>
    /// 事件模型的异步步进操作实现类型 抽象类
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="TProgressChangedEventArgs"></typeparam>
    public abstract class AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs, TProgressChangedEventArgs> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
        where TProgressChangedEventArgs : ProgressChangedEventArgs
    {
        /// <summary>
        /// ProgressChanged Delegate
        /// </summary>
        protected System.Threading.SendOrPostCallback _onProgressChangedDelegate;
        /// <summary>
        /// 构造
        /// </summary>
        protected AsyncProgressiveOperationEventBasedSkeleton()
            : base()
        {
            _onProgressChangedDelegate = new System.Threading.SendOrPostCallback(OnProgressChanged);
        }
        /// <summary>
        /// 报告异步操作进度函数
        /// </summary>
        /// <param name="progressPercentage"></param>
        /// <param name="asyncOp"></param>
        /// <param name="args"></param>
        protected void ReportProgress(Int32 progressPercentage, AsyncOperation asyncOp, params object[] args)
        {
            List<Object> instanceArgs = new List<Object>();
            instanceArgs.Add(progressPercentage);
            instanceArgs.Add(asyncOp.UserSuppliedState);
            if(args != null)
            {
                foreach (var arg in args)
                {
                    instanceArgs.Add(arg);
                }
            }
            Object e = Activator.CreateInstance(typeof(TProgressChangedEventArgs), instanceArgs.ToArray());            
            // End the task. The asyncOp object is responsible 
            // for marshaling the call.
            asyncOp.Post(_onProgressChangedDelegate, e);
        }
        /// <summary>
        /// OnProgressChanged
        /// </summary>
        /// <param name="operationState"></param>
        protected void OnProgressChanged(object operationState)
        {
            TProgressChangedEventArgs e = operationState as TProgressChangedEventArgs;
            if (ProgressChanged != null)
            {
                try
                {
                    ProgressChanged(this, e);
                }
                catch { }
            }
        }
        /// <summary>
        /// 验证泛型构造的有效性
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="args">构造参数列表</param>        
        protected void ValidateProgressChangedEventArgs(String signature, params Object[] args)
        {
            try
            {
                Activator.CreateInstance(typeof(TProgressChangedEventArgs), args.ToArray());
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(String.Format("Type {0} constructor is not {1}.", typeof(TProgressChangedEventArgs).FullName, signature), ex);
            }
        }
        /// <summary>
        /// 异步操作进度改变事件
        /// </summary>
        public event EventHandler<TProgressChangedEventArgs> ProgressChanged;


    }
}
