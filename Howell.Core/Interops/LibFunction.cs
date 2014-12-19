using System;
using System.Runtime.InteropServices;

#if !SILVERLIGHT
using System.ComponentModel;
#endif

namespace Howell.Interops
{
    /// <summary>
    /// LibHowellFunction class
    /// </summary>
    /// <typeparam name="T">Function signature type</typeparam>
    public sealed class LibFunction<T>
    {
        private T myDelegate;
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="libHowellHandle"></param>
        public LibFunction(IntPtr libHowellHandle)
            : this(libHowellHandle, null)
        {
        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="libHowellHandle"></param>
        /// <param name="currentHowellVersion"></param>
        public LibFunction(IntPtr libHowellHandle, Version currentHowellVersion)
        {
            IsAvailable = false;
            object[] attrs = typeof(T).GetCustomAttributes(typeof(LibFunctionAttribute), false);
            if (attrs.Length == 0)
                throw new Exception("Could not find the LibHowellFunctionAttribute.");
            var cpt = 0;
            foreach (LibFunctionAttribute attr in attrs)
            {
                if (attr == null)
                    return;
                if (cpt == 0)
                    FunctionNames = string.Format("'{0}'", attr.FunctionName);
                else
                    FunctionNames += string.Format(" or '{0}'", attr.FunctionName);
                if ((attr.MinVersion == null || currentHowellVersion >= attr.MinVersion) && (attr.MaxVersion == null || currentHowellVersion < attr.MaxVersion))
                {
                    FunctionName = attr.FunctionName;
                    CreateDelegate(libHowellHandle);
                    IsAvailable = true;
                    FunctionNames = FunctionName;
                    return;
                }
                cpt++;
            }
        }

        /// <summary>
        /// The function name in lib.
        /// </summary>
        public string FunctionName { get; private set; }
        /// <summary>
        /// Invoke the method.
        /// </summary>
        public T Invoke
        {
            get
            {
                if (!IsAvailable)
                    throw new MissingMethodException(string.Format("The {0} function is not available for this version of libHowell.", FunctionNames));
                return myDelegate;
            }
        }
        /// <summary>
        /// Check if this method is available with this version of libHowell.
        /// </summary>
        public bool IsAvailable { get; private set; }
        /// <summary>
        /// Create function delegate
        /// </summary>
        /// <param name="libHowellDllPointer"></param>
        private void CreateDelegate(IntPtr libHowellDllPointer)
        {
            try
            {
                IntPtr procAddress = Win32Interop.GetProcAddress(libHowellDllPointer, FunctionName);
                if (procAddress == IntPtr.Zero)
                    throw new Win32Exception();
                Delegate delegateForFunctionPointer = Marshal.GetDelegateForFunctionPointer(procAddress, typeof(T));
                myDelegate = (T)Convert.ChangeType(delegateForFunctionPointer, typeof(T), null);
            }
            catch (Win32Exception e)
            {
                throw new MissingMethodException(String.Format("The address of the function {0} does not exist in libHowell library.", FunctionNames), e);
            }
        }
        /// <summary>
        /// Function names 
        /// </summary>
        public string FunctionNames { get; private set; }
    }
}
