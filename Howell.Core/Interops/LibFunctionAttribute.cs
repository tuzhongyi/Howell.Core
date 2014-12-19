using System;

namespace Howell.Interops
{
    /// <summary>
    /// LibHowellFunction Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Delegate, AllowMultiple = true)]
    public sealed class LibFunctionAttribute : Attribute
    {
        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get; private set; }
        /// <summary>
        /// MinVersion Supported
        /// </summary>
        public Version MinVersion { get; private set; }
        /// <summary>
        /// MaxVersion Supported
        /// </summary>
        public Version MaxVersion { get; private set; }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="functionName">function name</param>
        public LibFunctionAttribute(string functionName)
            : this(functionName, null)
        {
        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="functionName">function name</param>
        /// <param name="minVersion">min version supported</param>
        public LibFunctionAttribute(string functionName, string minVersion)
            : this(functionName, minVersion, null)
        {
        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="functionName">function name</param>
        /// <param name="minVersion">min version supported</param>
        /// <param name="maxVersion">max version supported</param>
        public LibFunctionAttribute(string functionName, string minVersion, string maxVersion)
        {
            FunctionName = functionName;
            if (minVersion != null)
                MinVersion = new Version(minVersion);
            if (maxVersion != null)
                MaxVersion = new Version(maxVersion);
        }
    }
}
