using System;
using System.Collections.Generic;
using System.Text;

namespace Howell
{

    ///<summary>
    /// CommonString class
    ///</summary>
    public static class CommonStrings
    {
        /// <summary>
        /// "C:\Program Files (x86)\Howell\Device\"
        /// </summary>
        public const string LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64 = @"C:\Program Files (x86)\Howell\Device\";

        /// <summary>
        /// "C:\Program Files\Howell\Device\"
        /// </summary>
        public const string LIBVLC_DLLS_PATH_DEFAULT_VALUE_X86 = @"C:\Program Files\Howell\Device\";

        /// <summary>
        /// "C:\Program Files (x86)\Howell\Device\plugins\"
        /// </summary>
        public const string PLUGINS_PATH_DEFAULT_VALUE_AMD64 = @"C:\Program Files (x86)\Howell\Device\plugins\";

        /// <summary>
        /// "C:\Program Files\Howell\Device\plugins\"
        /// </summary>
        public const string PLUGINS_PATH_DEFAULT_VALUE_X86 = @"C:\Program Files\Howell\Device\plugins\";

        internal const string VLC_DOTNET_PROPERTIES_CATEGORY = "Howell DotNet";
    }
}
