using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Howell.ComponentModel
{

    #region ProgressiveActionDelegate
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate(ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T">此委托封装的方法的第一个参数类型。</typeparam>
    /// <param name="arg">此委托封装的方法的第一个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T>(T arg, ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T1, T2>(T1 arg1, T2 arg2, ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <typeparam name="T5">此委托封装的方法的第五个参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <typeparam name="T5">此委托封装的方法的第五个参数类型。</typeparam>
    /// <typeparam name="T6">此委托封装的方法的第六个参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="arg6">此委托封装的方法的第六个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <typeparam name="T5">此委托封装的方法的第五个参数类型。</typeparam>
    /// <typeparam name="T6">此委托封装的方法的第六个参数类型。</typeparam>
    /// <typeparam name="T7">此委托封装的方法的第七个参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="arg6">此委托封装的方法的第六个参数。</param>
    /// <param name="arg7">此委托封装的方法的第七个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <typeparam name="T5">此委托封装的方法的第五个参数类型。</typeparam>
    /// <typeparam name="T6">此委托封装的方法的第六个参数类型。</typeparam>
    /// <typeparam name="T7">此委托封装的方法的第七个参数类型。</typeparam>
    /// <typeparam name="T8">此委托封装的方法的第八个参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="arg6">此委托封装的方法的第六个参数。</param>
    /// <param name="arg7">此委托封装的方法的第七个参数。</param>
    /// <param name="arg8">此委托封装的方法的第八个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <typeparam name="T5">此委托封装的方法的第五个参数类型。</typeparam>
    /// <typeparam name="T6">此委托封装的方法的第六个参数类型。</typeparam>
    /// <typeparam name="T7">此委托封装的方法的第七个参数类型。</typeparam>
    /// <typeparam name="T8">此委托封装的方法的第八个参数类型。</typeparam>
    /// <typeparam name="T9">此委托封装的方法的第九个参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="arg6">此委托封装的方法的第六个参数。</param>
    /// <param name="arg7">此委托封装的方法的第七个参数。</param>
    /// <param name="arg8">此委托封装的方法的第八个参数。</param>
    /// <param name="arg9">此委托封装的方法的第九个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, ref Int32 progressPercentage);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <typeparam name="T5">此委托封装的方法的第五个参数类型。</typeparam>
    /// <typeparam name="T6">此委托封装的方法的第六个参数类型。</typeparam>
    /// <typeparam name="T7">此委托封装的方法的第七个参数类型。</typeparam>
    /// <typeparam name="T8">此委托封装的方法的第八个参数类型。</typeparam>
    /// <typeparam name="T9">此委托封装的方法的第九个参数类型。</typeparam>
    /// <typeparam name="T10">此委托封装的方法的第十个参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="arg6">此委托封装的方法的第六个参数。</param>
    /// <param name="arg7">此委托封装的方法的第七个参数。</param>
    /// <param name="arg8">此委托封装的方法的第八个参数。</param>
    /// <param name="arg9">此委托封装的方法的第九个参数。</param>
    /// <param name="arg10">此委托封装的方法的第十个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    public delegate void ProgressiveActionDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, ref Int32 progressPercentage);
    #endregion
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 void 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    public class AsyncProgressiveActionEventBased<TCompletedEventArgs> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp);
        private ProgressiveActionDelegate<Object, Func<Object, Boolean>> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveActionEventBased(ProgressiveActionDelegate<Object, Func<Object, Boolean>> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }

        private void Worker(AsyncOperation asyncOp)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, ref  progressPercentage);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp);
                    } while (true);
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
    public class AsyncProgressiveActionEventBased<TCompletedEventArgs, T> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T arg);
        private ProgressiveActionDelegate<Object, Func<Object, Boolean>, T> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveActionEventBased(ProgressiveActionDelegate<Object, Func<Object, Boolean>, T> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T arg)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg, ref  progressPercentage);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp);
                    } while (true);
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
        /// <param name="taskId"></param>
        /// <param name="arg"></param>
        public void Async(object taskId,T arg)
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
    public class AsyncProgressiveActionEventBased<TCompletedEventArgs, T1, T2> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2);
        private ProgressiveActionDelegate<Object, Func<Object, Boolean>, T1, T2> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveActionEventBased(ProgressiveActionDelegate<Object, Func<Object, Boolean>, T1, T2> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1,T2 arg2)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, ref  progressPercentage);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp);
                    } while (true);
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
        public void Async(object taskId, T1 arg1,T2 arg2)
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
    public class AsyncProgressiveActionEventBased<TCompletedEventArgs, T1, T2, T3> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3);
        private ProgressiveActionDelegate<Object, Func<Object, Boolean>, T1, T2, T3> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveActionEventBased(ProgressiveActionDelegate<Object, Func<Object, Boolean>, T1, T2, T3> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, arg3, ref  progressPercentage);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp);
                    } while (true);
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
    public class AsyncProgressiveActionEventBased<TCompletedEventArgs, T1, T2, T3, T4> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        private ProgressiveActionDelegate<Object, Func<Object, Boolean>, T1, T2, T3, T4> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveActionEventBased(ProgressiveActionDelegate<Object, Func<Object, Boolean>, T1, T2, T3, T4> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3,T4 arg4)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, arg3, arg4, ref  progressPercentage);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp);
                    } while (true);
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
        public void Async(object taskId, T1 arg1, T2 arg2, T3 arg3,T4 arg4)
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
