using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Net.NetworkInformation
{
    /// <summary>
    /// Physical address extension functions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PhysicalAddressExtensions
    {
        /// <summary>
        /// Convert to a formatted string containing the address contained in this instance.
        /// </summary>
        /// <param name="address">The instance of address.</param>
        /// <returns>Returns the formatted String representation of the address of this instance.</returns>
        public static String ToFormattedString(this PhysicalAddress address)
        {
            Byte[] addressBytes = address.GetAddressBytes();
            if (addressBytes.Length != 6)
            {
                throw new FormatException("PhysicalAddress format is illegal.");
            }
            return String.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}-{4:X2}-{5:X2}", addressBytes[0], addressBytes[1], addressBytes[2], addressBytes[3], addressBytes[4], addressBytes[5]);
        }
    }
}
