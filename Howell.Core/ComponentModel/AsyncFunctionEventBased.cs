using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Howell.ComponentModel
{
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 TResult 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class AsyncFunctionEventBased<TCompletedEventArgs, TResult> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
        where TResult : new()
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp);
        private Func<Object, Func<Object, Boolean>,TResult> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--TResult 函数名(Object taskId,Func<Object, Boolean,TResult> isCancelled);-->
        /// </param>
        public AsyncFunctionEventBased(Func<Object, Func<Object, Boolean>, TResult> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp)
        {
            Exception e = null;
            TResult result = default(TResult);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    result = _implFunction(asyncOp.UserSuppliedState, IsCancelled);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, result);
        }
        /// <summary>
        /// 异步操作行为
        /// </summary>
        /// <param name="taskId"></param>
        public void Async(object taskId)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object),typeof(TResult)),
                new Exception(), false, new Object(), default(TResult));
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, null, null);
        }
    }
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 TResult 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class AsyncFunctionEventBased<TCompletedEventArgs, T, TResult> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T arg);
        private Func<Object, Func<Object, Boolean>, T, TResult> _implFunction = null;
        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--TResult 函数名(Object taskId,Func<Object, Boolean,TResult> isCancelled);-->
        /// </param>
        public AsyncFunctionEventBased(Func<Object, Func<Object, Boolean>, T, TResult> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T arg)
        {
            Exception e = null;
            TResult result = default(TResult);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    result = _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, arg, result);
        }
        /// <summary>
        /// 异步操作行为
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="arg"></param>
        public void Async(object taskId,T arg)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T), typeof(TResult)),
                new Exception(), false, new Object(), arg, default(TResult));
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, arg, null, null);
        }
    }

    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 TResult 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class AsyncFunctionEventBased<TCompletedEventArgs, T1, T2, TResult> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2);
        private Func<Object, Func<Object, Boolean>, T1, T2, TResult> _implFunction = null;
        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--TResult 函数名(Object taskId,Func<Object, Boolean,TResult> isCancelled);-->
        /// </param>
        public AsyncFunctionEventBased(Func<Object, Func<Object, Boolean>, T1, T2, TResult> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2)
        {
            Exception e = null;
            TResult result = default(TResult);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    result = _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, arg1, arg2, result);
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
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T1), typeof(T2), typeof(TResult)),
                new Exception(), false, new Object(), arg1, arg2, default(TResult));
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, arg1, arg2, null, null);
        }
    }
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 TResult 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public class AsyncFunctionEventBased<TCompletedEventArgs, T1, T2, T3, TResult> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3);
        private Func<Object, Func<Object, Boolean>, T1, T2, T3, TResult> _implFunction = null;
        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--TResult 函数名(Object taskId,Func<Object, Boolean,TResult> isCancelled);-->
        /// </param>
        public AsyncFunctionEventBased(Func<Object, Func<Object, Boolean>, T1, T2, T3, TResult> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3)
        {
            Exception e = null;
            TResult result = default(TResult);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    result = _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, arg3);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, arg1, arg2, arg3, result);
        }
        /// <summary>
        /// 异步操作行为
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        public void Async(object taskId, T1 arg1, T2 arg2,T3 arg3)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T1), typeof(T2), typeof(T3), typeof(TResult)),
                new Exception(), false, new Object(), arg1, arg2, arg3, default(TResult));
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, arg1, arg2, arg3, null, null);
        }
    }
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 TResult 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    public class AsyncFunctionEventBased<TCompletedEventArgs, T1, T2, T3, T4, TResult> : AsyncOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3,T4 arg4);
        private Func<Object, Func<Object, Boolean>, T1, T2, T3, T4, TResult> _implFunction = null;
        /// <summary>
        /// 构造事件模型的异步操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--TResult 函数名(Object taskId,Func<Object, Boolean,TResult> isCancelled);-->
        /// </param>
        public AsyncFunctionEventBased(Func<Object, Func<Object, Boolean>, T1, T2, T3, T4, TResult> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Exception e = null;
            TResult result = default(TResult);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    result = _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, arg3,arg4);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            Completion(e, IsCancelled(asyncOp.UserSuppliedState), asyncOp, arg1, arg2, arg3,arg4, result);
        }
        /// <summary>
        /// 异步操作行为
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        public void Async(object taskId, T1 arg1, T2 arg2, T3 arg3,T4 arg4)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(TResult)),
                new Exception(), false, new Object(), arg1, arg2, arg3, arg4, default(TResult));
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, arg1, arg2, arg3, arg4, null, null);
        }
    }

}
