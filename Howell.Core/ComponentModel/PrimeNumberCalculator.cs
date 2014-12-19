using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Collections.Specialized;
using System.Collections;

namespace Howell.ComponentModel
{
    #region PrimeNumberCalculator Implementation

    public delegate void ProgressChangedEventHandler(
        ProgressChangedEventArgs e);

    public delegate void CalculatePrimeCompletedEventHandler(
        object sender,
        CalculatePrimeCompletedEventArgs e);

    // This class implements the Event-based Asynchronous Pattern.
    // It asynchronously computes whether a number is prime or
    // composite (not prime).
    public class PrimeNumberCalculator : Component
    {
        private delegate void WorkerEventHandler(
            int numberToCheck,
            AsyncOperation asyncOp);

        private SendOrPostCallback onProgressReportDelegate;
        private SendOrPostCallback onCompletedDelegate;

        private HybridDictionary userStateToLifetime =
            new HybridDictionary();

        private System.ComponentModel.Container components = null;

        /////////////////////////////////////////////////////////////
        #region Public events

        public event ProgressChangedEventHandler ProgressChanged;
        public event CalculatePrimeCompletedEventHandler CalculatePrimeCompleted;

        #endregion

        /////////////////////////////////////////////////////////////
        #region Construction and destruction

        public PrimeNumberCalculator(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            InitializeDelegates();
        }

        public PrimeNumberCalculator()
        {
            InitializeComponent();
            InitializeDelegates();
        }

        protected virtual void InitializeDelegates()
        {
            onProgressReportDelegate =
                new SendOrPostCallback(ReportProgress);
            onCompletedDelegate =
                new SendOrPostCallback(CalculateCompleted);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion // Construction and destruction

        /////////////////////////////////////////////////////////////
        ///
        #region Implementation

        // This method starts an asynchronous calculation. 
        // First, it checks the supplied task ID for uniqueness.
        // If taskId is unique, it creates a new WorkerEventHandler 
        // and calls its BeginInvoke method to start the calculation.
        public virtual void CalculatePrimeAsync(
            int numberToTest,
            object taskId)
        {
            // Create an AsyncOperation for taskId.
            AsyncOperation asyncOp =
                AsyncOperationManager.CreateOperation(taskId);

            // Multiple threads will access the task dictionary,
            // so it must be locked to serialize access.
            lock (userStateToLifetime.SyncRoot)
            {
                if (userStateToLifetime.Contains(taskId))
                {
                    throw new ArgumentException(
                        "Task ID parameter must be unique",
                        "taskId");
                }

                userStateToLifetime[taskId] = asyncOp;
            }

            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(CalculateWorker);
            workerDelegate.BeginInvoke(
                numberToTest,
                asyncOp,
                null,
                null);
        }

        // Utility method for determining if a 
        // task has been canceled.
        private bool TaskCanceled(object taskId)
        {
            return (userStateToLifetime[taskId] == null);
        }

        // This method cancels a pending asynchronous operation.
        public void CancelAsync(object taskId)
        {
            AsyncOperation asyncOp = userStateToLifetime[taskId] as AsyncOperation;
            if (asyncOp != null)
            {
                lock (userStateToLifetime.SyncRoot)
                {
                    userStateToLifetime.Remove(taskId);
                }
            }
        }

        // This method performs the actual prime number computation.
        // It is executed on the worker thread.
        private void CalculateWorker(
            int numberToTest,
            AsyncOperation asyncOp)
        {
            bool isPrime = false;
            int firstDivisor = 1;
            Exception e = null;

            // Check that the task is still active.
            // The operation may have been canceled before
            // the thread was scheduled.
            if (!TaskCanceled(asyncOp.UserSuppliedState))
            {
                try
                {
                    // Find all the prime numbers up to 
                    // the square root of numberToTest.
                    ArrayList primes = BuildPrimeNumberList(
                        numberToTest,
                        asyncOp);

                    // Now we have a list of primes less than
                    // numberToTest.
                    isPrime = IsPrime(
                        primes,
                        numberToTest,
                        out firstDivisor);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }

            //CalculatePrimeState calcState = new CalculatePrimeState(
            //        numberToTest,
            //        firstDivisor,
            //        isPrime,
            //        e,
            //        TaskCanceled(asyncOp.UserSuppliedState),
            //        asyncOp);

            //this.CompletionMethod(calcState);

            this.CompletionMethod(
                numberToTest,
                firstDivisor,
                isPrime,
                e,
                TaskCanceled(asyncOp.UserSuppliedState),
                asyncOp);

            //completionMethodDelegate(calcState);
        }

        // This method computes the list of prime numbers used by the
        // IsPrime method.
        private ArrayList BuildPrimeNumberList(
            int numberToTest,
            AsyncOperation asyncOp)
        {
            ProgressChangedEventArgs e = null;
            ArrayList primes = new ArrayList();
            int firstDivisor;
            int n = 5;

            // Add the first prime numbers.
            primes.Add(2);
            primes.Add(3);

            // Do the work.
            while (n < numberToTest &&
                   !TaskCanceled(asyncOp.UserSuppliedState))
            {
                if (IsPrime(primes, n, out firstDivisor))
                {
                    // Report to the client that a prime was found.
                    e = new CalculatePrimeProgressChangedEventArgs(
                        n,
                        (int)((float)n / (float)numberToTest * 100),
                        asyncOp.UserSuppliedState);

                    asyncOp.Post(this.onProgressReportDelegate, e);

                    primes.Add(n);

                    // Yield the rest of this time slice.
                    Thread.Sleep(0);
                }

                // Skip even numbers.
                n += 2;
            }

            return primes;
        }

        // This method tests n for primality against the list of 
        // prime numbers contained in the primes parameter.
        private bool IsPrime(
            ArrayList primes,
            int n,
            out int firstDivisor)
        {
            bool foundDivisor = false;
            bool exceedsSquareRoot = false;

            int i = 0;
            int divisor = 0;
            firstDivisor = 1;

            // Stop the search if:
            // there are no more primes in the list,
            // there is a divisor of n in the list, or
            // there is a prime that is larger than 
            // the square root of n.
            while (
                (i < primes.Count) &&
                !foundDivisor &&
                !exceedsSquareRoot)
            {
                // The divisor variable will be the smallest 
                // prime number not yet tried.
                divisor = (int)primes[i++];

                // Determine whether the divisor is greater
                // than the square root of n.
                if (divisor * divisor > n)
                {
                    exceedsSquareRoot = true;
                }
                // Determine whether the divisor is a factor of n.
                else if (n % divisor == 0)
                {
                    firstDivisor = divisor;
                    foundDivisor = true;
                }
            }

            return !foundDivisor;
        }

        // This method is invoked via the AsyncOperation object,
        // so it is guaranteed to be executed on the correct thread.
        private void CalculateCompleted(object operationState)
        {
            CalculatePrimeCompletedEventArgs e =
                operationState as CalculatePrimeCompletedEventArgs;

            OnCalculatePrimeCompleted(e);
        }

        // This method is invoked via the AsyncOperation object,
        // so it is guaranteed to be executed on the correct thread.
        private void ReportProgress(object state)
        {
            ProgressChangedEventArgs e =
                state as ProgressChangedEventArgs;

            OnProgressChanged(e);
        }

        protected void OnCalculatePrimeCompleted(
            CalculatePrimeCompletedEventArgs e)
        {
            if (CalculatePrimeCompleted != null)
            {
                CalculatePrimeCompleted(this, e);
            }
        }

        protected void OnProgressChanged(ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(e);
            }
        }

        // This is the method that the underlying, free-threaded 
        // asynchronous behavior will invoke.  This will happen on
        // an arbitrary thread.
        private void CompletionMethod(
            int numberToTest,
            int firstDivisor,
            bool isPrime,
            Exception exception,
            bool canceled,
            AsyncOperation asyncOp)
        {
            // If the task was not previously canceled,
            // remove the task from the lifetime collection.
            if (!canceled)
            {
                lock (userStateToLifetime.SyncRoot)
                {
                    userStateToLifetime.Remove(asyncOp.UserSuppliedState);
                }
            }

            // Package the results of the operation in a 
            // CalculatePrimeCompletedEventArgs.
            CalculatePrimeCompletedEventArgs e =
                new CalculatePrimeCompletedEventArgs(
                numberToTest,
                firstDivisor,
                isPrime,
                exception,
                canceled,
                asyncOp.UserSuppliedState);

            // End the task. The asyncOp object is responsible 
            // for marshaling the call.
            asyncOp.PostOperationCompleted(onCompletedDelegate, e);

            // Note that after the call to OperationCompleted, 
            // asyncOp is no longer usable, and any attempt to use it
            // will cause an exception to be thrown.
        }


        #endregion

        /////////////////////////////////////////////////////////////
        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion

    }

    public class CalculatePrimeProgressChangedEventArgs :
            ProgressChangedEventArgs
    {
        private int latestPrimeNumberValue = 1;

        public CalculatePrimeProgressChangedEventArgs(
            int latestPrime,
            int progressPercentage,
            object userToken)
            : base(progressPercentage, userToken)
        {
            this.latestPrimeNumberValue = latestPrime;
        }

        public int LatestPrimeNumber
        {
            get
            {
                return latestPrimeNumberValue;
            }
        }
    }

    public class CalculatePrimeCompletedEventArgs :
        AsyncCompletedEventArgs
    {
        private int numberToTestValue = 0;
        private int firstDivisorValue = 1;
        private bool isPrimeValue;

        public CalculatePrimeCompletedEventArgs(
            int numberToTest,
            int firstDivisor,
            bool isPrime,
            Exception e,
            bool canceled,
            object state)
            : base(e, canceled, state)
        {
            this.numberToTestValue = numberToTest;
            this.firstDivisorValue = firstDivisor;
            this.isPrimeValue = isPrime;
        }

        public int NumberToTest
        {
            get
            {
                // Raise an exception if the operation failed or 
                // was canceled.
                RaiseExceptionIfNecessary();

                // If the operation was successful, return the 
                // property value.
                return numberToTestValue;
            }
        }

        public int FirstDivisor
        {
            get
            {
                // Raise an exception if the operation failed or 
                // was canceled.
                RaiseExceptionIfNecessary();

                // If the operation was successful, return the 
                // property value.
                return firstDivisorValue;
            }
        }

        public bool IsPrime
        {
            get
            {
                // Raise an exception if the operation failed or 
                // was canceled.
                RaiseExceptionIfNecessary();

                // If the operation was successful, return the 
                // property value.
                return isPrimeValue;
            }
        }
    }


    #endregion
}
