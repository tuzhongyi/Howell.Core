using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Howell.ComponentModel
{

    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 void 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    public class AsyncActionEventBased<TCompletedEventArgs> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp);
        private Action<Object, Func<Object, Boolean>> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled);-->
        /// </param>
        public AsyncActionEventBased(Action<Object, Func<Object, Boolean>> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }

        private void Worker(AsyncOperation asyncOp)
        {
            Exception e = null;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    _implFunction(asyncOp.UserSuppliedState, IsCancelled);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp);
        }
        /// <summary>
        /// 异步操作行为
        /// </summary>
        /// <param name="taskId"></param>
        public void Async(object taskId)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object)),
                new Exception(), false, new Object());
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, null, null);
        }
    }
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 void 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class AsyncActionEventBased<TCompletedEventArgs, T> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T arg);
        private Action<Object, Func<Object, Boolean>, T> _implFunction = null;
        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled);-->
        /// </param>
        public AsyncActionEventBased(Action<Object, Func<Object, Boolean>, T> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T arg)
        {
            Exception e = null;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, arg);
        }
        /// <summary>
        /// 异步操作行为
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="taskId"></param>
        public void Async(object taskId, T arg)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T)),
                new Exception(), false, new Object(), arg);
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, arg, null, null);
        }
    }
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 void 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class AsyncActionEventBased<TCompletedEventArgs, T1,T2> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2);
        private Action<Object, Func<Object, Boolean>, T1, T2> _implFunction = null;
        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled);-->
        /// </param>
        public AsyncActionEventBased(Action<Object, Func<Object, Boolean>, T1, T2> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2)
        {
            Exception e = null;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, arg1, arg2);
        }
        /// <summary>
        /// 异步操作行为
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void Async(object taskId, T1 arg1, T2 arg2)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T1), typeof(T2)),
                new Exception(), false, new Object(), arg1, arg2);
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, arg1, arg2, null, null);
        }
    }
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 void 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public class AsyncActionEventBased<TCompletedEventArgs, T1, T2, T3> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3);
        private Action<Object, Func<Object, Boolean>, T1, T2, T3> _implFunction = null;
        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled);-->
        /// </param>
        public AsyncActionEventBased(Action<Object, Func<Object, Boolean>, T1, T2, T3> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3)
        {
            Exception e = null;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, arg3);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, arg1, arg2, arg3);
        }
        /// <summary>
        /// 异步操作行为
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        public void Async(object taskId, T1 arg1, T2 arg2, T3 arg3)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T1), typeof(T2), typeof(T3)),
                new Exception(), false, new Object(), arg1, arg2, arg3);
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, arg1, arg2, arg3, null, null);
        }
    }
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 void 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    public class AsyncActionEventBased<TCompletedEventArgs, T1, T2, T3, T4> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        private Action<Object, Func<Object, Boolean>, T1, T2, T3, T4> _implFunction = null;
        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled);-->
        /// </param>
        public AsyncActionEventBased(Action<Object, Func<Object, Boolean>, T1, T2, T3, T4> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Exception e = null;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, arg3, arg4);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, arg1, arg2, arg3, arg4);
        }
        /// <summary>
        /// 异步操作行为
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        public void Async(object taskId, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T1), typeof(T2), typeof(T3), typeof(T4)),
                new Exception(), false, new Object(), arg1, arg2, arg3, arg4);
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, arg1, arg2, arg3, arg4, null, null);
        }
    }
}
