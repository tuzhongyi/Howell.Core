using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Collections.Generic
{
    /// <summary>
    /// IEnumerable{T} extension functions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Convert the instance of IEnumerable{Byte} to hex string.
        /// </summary>
        /// <param name="enumerator">The instance of IEnumerable{Byte}</param>
        /// <returns>returns a hex string.</returns>
        public static String ToHexString(this IEnumerable<Byte> enumerator)
        {
            String result = "";
            foreach (var item in enumerator)
            {
                result += item.ToString("X2");
            }
            return result;
        }
    }
}
