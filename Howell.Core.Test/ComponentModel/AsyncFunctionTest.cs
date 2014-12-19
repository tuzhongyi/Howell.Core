using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace Howell.ComponentModel
{
    public class PictureBoxLoadCompletedEventArgs : AsyncCompletedEventArgs
    {
        public PictureBoxLoadCompletedEventArgs(Exception error, Boolean cancelled, Object userState, MemoryStream imageData, Uri imagePath)
            : base(error, cancelled, userState)
        {
        }
        public Uri ImagePath { get; private set; }
        public MemoryStream ImageData { get; private set; }
    }
    public class PictureBoxLoadProgressChangedEventArgs : ProgressChangedEventArgs
    {
        public PictureBoxLoadProgressChangedEventArgs(int progressPercentage,Object userState,Int32 fileLength )
            : base(progressPercentage,userState)
        {
            this.FileLength = fileLength;
        }
        /// <summary>
        /// 文件长度
        /// </summary>
        public Int32 FileLength
        {
            get; private set; 
        }
    }
    public class PictureBox
    {
        private Object _taskId = new Object();
        private Howell.ComponentModel.AsyncFunctionEventBased<PictureBoxLoadCompletedEventArgs, Uri, MemoryStream> _asyncLoadFunction;
        public PictureBox()
        {
            InitializeAsyncFunctions();
        }
        private void InitializeAsyncFunctions()
        {
            _asyncLoadFunction = new AsyncFunctionEventBased<PictureBoxLoadCompletedEventArgs, Uri, MemoryStream>(LoadAsyncImpl);
            _asyncLoadFunction.Completed += new EventHandler<PictureBoxLoadCompletedEventArgs>(_asyncLoadFunction_Completed);
        }

        void _asyncLoadFunction_Completed(object sender, PictureBoxLoadCompletedEventArgs e)
        {
            if(LoadCompleted != null)
            {
                LoadCompleted(this, e);
            }
        }

        public void LoadAsync(Uri imagePath)
        {
            _asyncLoadFunction.Async(_taskId, imagePath);
        }
        public void LoadCancelAsync()
        {
            _asyncLoadFunction.CancelAsync(_taskId);
        }
        private MemoryStream LoadAsyncImpl(Object taskId,Func<Object,Boolean> isCancelled, Uri imagePath)
        {
            if(isCancelled(taskId)==true)
            {
                //cancel it.
            }
            return LoadImpl(imagePath);
        }
        private  MemoryStream LoadImpl(Uri imagePath)
        {
            System.Threading.Thread.Sleep(1000);
            //throw new NotImplementedException();
            return new MemoryStream();
        }

        public event EventHandler<PictureBoxLoadCompletedEventArgs> LoadCompleted;

    }
    public class PictureBox2
    {
        private Object _taskId = new Object();
        private Howell.ComponentModel.AsyncProgressiveReferenceFunctionEventBased<PictureBoxLoadCompletedEventArgs, PictureBoxLoadProgressChangedEventArgs, Uri, Int32, MemoryStream> _asyncLoadFunction;
        public PictureBox2()
        {
            InitializeAsyncFunctions();
        }
        private void InitializeAsyncFunctions()
        {
            _asyncLoadFunction = new AsyncProgressiveReferenceFunctionEventBased<PictureBoxLoadCompletedEventArgs, PictureBoxLoadProgressChangedEventArgs, Uri, Int32, MemoryStream>(LoadAsyncImpl);
            _asyncLoadFunction.Completed += new EventHandler<PictureBoxLoadCompletedEventArgs>(_asyncLoadFunction_Completed);
            _asyncLoadFunction.ProgressChanged += new EventHandler<PictureBoxLoadProgressChangedEventArgs>(_asyncLoadFunction_ProgressChanged);
        }

        void _asyncLoadFunction_ProgressChanged(object sender, PictureBoxLoadProgressChangedEventArgs e)
        {
            if(ProgressChanged != null)
            {
                ProgressChanged(this, e);
            }
        }

        void _asyncLoadFunction_Completed(object sender, PictureBoxLoadCompletedEventArgs e)
        {
            if (LoadCompleted != null)
            {
                LoadCompleted(this, e);
            }
        }

        public void LoadAsync(Uri imagePath)
        {
            _asyncLoadFunction.Async(_taskId, imagePath);
        }
        public void LoadCancelAsync()
        {
            _asyncLoadFunction.CancelAsync(_taskId);
        }
        private MemoryStream LoadAsyncImpl(Object taskId, Func<Object, Boolean> isCancelled, Uri imagePath, ref int progress, ref  Int32 fileLength)
        {
            if (isCancelled(taskId) == true)
            {
                if (progress != 0)
                {
                    progress -= 10;
                    return null;
                }
                //cancel it.
            }
            return LoadImpl(imagePath, ref progress, ref fileLength);
        }
        private MemoryStream LoadImpl(Uri imagePath, ref int progress, ref Int32 fileLength)
        {            
            System.Threading.Thread.Sleep(1000);
            //throw new NotImplementedException();            
            progress += 10;
            fileLength += 1023;
            return new MemoryStream();
        }

        public event EventHandler<PictureBoxLoadCompletedEventArgs> LoadCompleted;

        public event EventHandler<PictureBoxLoadProgressChangedEventArgs> ProgressChanged;

    }
    class AsyncFunctionTest
    {
        private static EventWaitHandle waitHandle = null;
        public static void Test()
        {
            waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            PictureBox2 pic = new PictureBox2();
            pic.LoadCompleted += new EventHandler<PictureBoxLoadCompletedEventArgs>(pic_LoadCompleted);
            pic.ProgressChanged += new EventHandler<PictureBoxLoadProgressChangedEventArgs>(pic_ProgressChanged);
            Console.WriteLine("PictureBox.LoadAsync Begin.");
            try
            {
                pic.LoadAsync(new Uri(@"C:\bmp"));
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            waitHandle.WaitOne();
            Console.WriteLine("PictureBox.LoadAsync Completed.");
            waitHandle.Close();
            waitHandle = null;
        }

        static void pic_ProgressChanged(object sender, PictureBoxLoadProgressChangedEventArgs e)
        {
            Console.WriteLine("Progress Changed:{0} Length:{1}", e.ProgressPercentage, e.FileLength);
        }

        static void pic_LoadCompleted(object sender, PictureBoxLoadCompletedEventArgs e)
        {
            Console.WriteLine("Picture Load Completed:{0}", e.ImagePath);
            waitHandle.Set();
        }
    }
}
