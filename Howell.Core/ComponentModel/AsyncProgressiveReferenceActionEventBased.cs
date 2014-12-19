using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Howell.ComponentModel
{

    #region ProgressiveReferenceActionDelegate
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
    /// <param name="progressPercentage">引用参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<TRef>(ref Int32 progressPercentage, ref TRef refArg);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
    /// <param name="arg">此委托封装的方法的第一个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T, TRef>(T arg, ref Int32 progressPercentage, ref TRef refArg);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T1, T2, TRef>(T1 arg1, T2 arg2, ref Int32 progressPercentage, ref TRef refArg);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T1, T2, T3, TRef>(T1 arg1, T2 arg2, T3 arg3, ref Int32 progressPercentage, ref TRef refArg);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T1, T2, T3, T4, TRef>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref Int32 progressPercentage, ref TRef refArg);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <typeparam name="T5">此委托封装的方法的第五个参数类型。</typeparam>
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T1, T2, T3, T4, T5, TRef>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ref Int32 progressPercentage, ref TRef refArg);
    /// <summary>
    /// 封装一个异步操作实现的方法。在进度没有等于1或操作被取消并且进度没有等于0时将继续调用该操作
    /// </summary>
    /// <typeparam name="T1">此委托封装的方法的第一个参数类型。</typeparam>
    /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
    /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
    /// <typeparam name="T4">此委托封装的方法的第四个参数类型。</typeparam>
    /// <typeparam name="T5">此委托封装的方法的第五个参数类型。</typeparam>
    /// <typeparam name="T6">此委托封装的方法的第六个参数类型。</typeparam>
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="arg6">此委托封装的方法的第六个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T1, T2, T3, T4, T5, T6, TRef>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, ref Int32 progressPercentage, ref TRef refArg);
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
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="arg6">此委托封装的方法的第六个参数。</param>
    /// <param name="arg7">此委托封装的方法的第七个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T1, T2, T3, T4, T5, T6, T7, TRef>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, ref Int32 progressPercentage, ref TRef refArg);
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
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
    /// <param name="arg1">此委托封装的方法的第一个参数。</param>
    /// <param name="arg2">此委托封装的方法的第二个参数。</param>
    /// <param name="arg3">此委托封装的方法的第三个参数。</param>
    /// <param name="arg4">此委托封装的方法的第四个参数。</param>
    /// <param name="arg5">此委托封装的方法的第五个参数。</param>
    /// <param name="arg6">此委托封装的方法的第六个参数。</param>
    /// <param name="arg7">此委托封装的方法的第七个参数。</param>
    /// <param name="arg8">此委托封装的方法的第八个参数。</param>
    /// <param name="progressPercentage">输入输出参数用于传递异步操作的进度，取值范围0-100，0表示尚未开始，100表示完成异步操作</param>
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T1, T2, T3, T4, T5, T6, T7, T8, TRef>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, ref Int32 progressPercentage, ref TRef refArg);
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
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
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
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TRef>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, ref Int32 progressPercentage, ref TRef refArg);
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
    /// <typeparam name="TRef">此委托封装的方法的第一个引用参数类型。</typeparam>
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
    /// <param name="refArg">此委托封装的方法的第一个引用参数。</param>
    public delegate void ProgressiveReferenceActionDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TRef>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, ref Int32 progressPercentage, ref TRef refArg);
    #endregion
    /// <summary>
    /// 事件模型的异步操作实现类型，此类型的同步实现方案为 void 函数名();
    /// </summary>
    /// <typeparam name="TCompletedEventArgs"></typeparam>
    /// <typeparam name="TProgressChangedEventArgs"></typeparam>
    /// <typeparam name="TRef"></typeparam>
    public class AsyncProgressiveReferenceActionEventBased<TCompletedEventArgs, TProgressChangedEventArgs, TRef> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs, TProgressChangedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
        where TProgressChangedEventArgs : ProgressChangedEventArgs
        where TRef : new()
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp);
        private ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, TRef> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveReferenceActionEventBased(ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, TRef> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }

        private void Worker(AsyncOperation asyncOp)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            TRef refArg = default(TRef);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, ref  progressPercentage, ref refArg);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp, refArg);
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
            ValidateProgressChangedEventArgs(CreateConstructedFunctionSignature(typeof(Int32), typeof(Object), typeof(TRef)), 0, new Object(), new TRef());
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
    /// <typeparam name="TProgressChangedEventArgs"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRef"></typeparam>
    public class AsyncProgressiveReferenceActionEventBased<TCompletedEventArgs, TProgressChangedEventArgs, T, TRef> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs, TProgressChangedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
        where TProgressChangedEventArgs : ProgressChangedEventArgs
        where TRef : new()
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T arg);
        private ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, T, TRef> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveReferenceActionEventBased(ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, T, TRef> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T arg)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            TRef refArg = default(TRef);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg, ref  progressPercentage, ref refArg);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp, refArg);
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
        public void Async(object taskId, T arg)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T)),
                new Exception(), false, new Object(), arg);
            ValidateProgressChangedEventArgs(CreateConstructedFunctionSignature(typeof(Int32), typeof(Object), typeof(TRef)), 0, new Object(), new TRef());
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
    /// <typeparam name="TProgressChangedEventArgs"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TRef"></typeparam>
    public class AsyncProgressiveReferenceActionEventBased<TCompletedEventArgs, TProgressChangedEventArgs, T1, T2, TRef> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs, TProgressChangedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
        where TProgressChangedEventArgs : ProgressChangedEventArgs
        where TRef : new()
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2);
        private ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, T1, T2, TRef> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveReferenceActionEventBased(ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, T1, T2, TRef> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            TRef refArg = default(TRef);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, ref  progressPercentage, ref refArg);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp, refArg);
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
        public void Async(object taskId, T1 arg1, T2 arg2)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T1), typeof(T2)),
                new Exception(), false, new Object(), arg1, arg2);
            ValidateProgressChangedEventArgs(CreateConstructedFunctionSignature(typeof(Int32), typeof(Object), typeof(TRef)), 0, new Object(), new TRef());
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
    /// <typeparam name="TProgressChangedEventArgs"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="TRef"></typeparam>
    public class AsyncProgressiveReferenceActionEventBased<TCompletedEventArgs, TProgressChangedEventArgs, T1, T2, T3, TRef> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs, TProgressChangedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
        where TProgressChangedEventArgs : ProgressChangedEventArgs
        where TRef : new()
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3);
        private ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, T1, T2, T3, TRef> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveReferenceActionEventBased(ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, T1, T2, T3, TRef> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            TRef refArg = default(TRef);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, arg3, ref  progressPercentage, ref refArg);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp, refArg);
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
            ValidateProgressChangedEventArgs(CreateConstructedFunctionSignature(typeof(Int32), typeof(Object), typeof(TRef)), 0, new Object(), new TRef());
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
    /// <typeparam name="TProgressChangedEventArgs"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="TRef"></typeparam>
    public class AsyncProgressiveActionEventBased<TCompletedEventArgs, TProgressChangedEventArgs, T1, T2, T3, T4, TRef> : AsyncProgressiveOperationEventBasedSkeleton<TCompletedEventArgs, TProgressChangedEventArgs>
        where TCompletedEventArgs : AsyncCompletedEventArgs
        where TProgressChangedEventArgs : ProgressChangedEventArgs
        where TRef : new()
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        private ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, T1, T2, T3, T4, TRef> _implFunction = null;

        /// <summary>
        /// 构造事件模型的异步进度操作
        /// </summary>
        /// <param name="implFunction">
        /// 异步操作的同步实现函数
        /// 函数原型: <!--void 函数名(Object taskId,Func<Object, Boolean> isCancelled,ref progressPercentage);-->
        /// </param>
        public AsyncProgressiveActionEventBased(ProgressiveReferenceActionDelegate<Object, Func<Object, Boolean>, T1, T2, T3, T4, TRef> implFunction)
            : base()
        {
            _implFunction = implFunction;
        }
        private void Worker(AsyncOperation asyncOp, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Exception e = null;
            Int32 progressPercentage = 0;
            TRef refArg = default(TRef);
            if (!IsCancelled(asyncOp.UserSuppliedState))
            {
                try
                {
                    do
                    {
                        _implFunction(asyncOp.UserSuppliedState, IsCancelled, arg1, arg2, arg3, arg4, ref  progressPercentage, ref refArg);
                        //取消完成
                        if (progressPercentage == 0 && IsCancelled(asyncOp.UserSuppliedState) == true)
                            break;
                        //操作完成
                        if (progressPercentage == 100 && IsCancelled(asyncOp.UserSuppliedState) == false)
                            break;
                        base.ReportProgress(progressPercentage, asyncOp, refArg);
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
        public void Async(object taskId, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            //验证构造函数
            ValidateCompletedEventArgs(CreateConstructedFunctionSignature(typeof(Exception), typeof(Boolean), typeof(Object), typeof(T1), typeof(T2), typeof(T3), typeof(T4)),
                new Exception(), false, new Object(), arg1, arg2, arg3, arg4);
            ValidateProgressChangedEventArgs(CreateConstructedFunctionSignature(typeof(Int32), typeof(Object), typeof(TRef)), 0, new Object(), new TRef());
            AsyncOperation asyncOp = base.BeginAsync(taskId);
            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(Worker);
            workerDelegate.BeginInvoke(asyncOp, arg1, arg2, arg3, arg4, null, null);
        }
    }
}
