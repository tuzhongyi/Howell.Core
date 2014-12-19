﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{

    /// <summary>
    /// Extensions for the standard string class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// <para>
        /// Converts a string of characters from Big Endian byte order to
        /// Little Endian byte order.
        /// </para>
        /// <para>
        /// Assumptions this makes about the string. Every two characters
        /// make up the smallest data unit (analogous to byte). The entire
        /// string is the size of the systems natural unit of data (analogous
        /// to a word).
        /// </para>
        /// </summary>
        /// <param name="value">
        /// A string in Big Endian Byte order.
        /// </param>
        /// <returns>
        /// A string in Little Endian Byte order.
        /// </returns>
        /// <remarks>
        /// This function was designed to take in a Big Endian string of
        /// hexadecimal digits.
        /// <example>
        /// input:
        ///     DEADBEEF
        /// output:
        ///     EFBEADDE
        /// </example>
        /// </remarks>
        public static string ToLittleEndian(this string value)
        {
            // Guard
            if (value == null)
            {
                throw new NullReferenceException();
            }

            char[] bigEndianChars = value.ToCharArray();

            // Guard
            if (bigEndianChars.Length % 2 != 0)
            {
                return string.Empty;
            }

            int i, ai, bi, ci, di;
            char a, b, c, d;

            for (i = 0; i < bigEndianChars.Length / 2; i += 2)
            {
                // front byte ( in hex )
                ai = i;
                bi = i + 1;

                // back byte ( in hex )
                ci = bigEndianChars.Length - 2 - i;
                di = bigEndianChars.Length - 1 - i;

                a = bigEndianChars[ai];
                b = bigEndianChars[bi];
                c = bigEndianChars[ci];
                d = bigEndianChars[di];

                bigEndianChars[ci] = a;
                bigEndianChars[di] = b;
                bigEndianChars[ai] = c;
                bigEndianChars[bi] = d;
            }

            return new string(bigEndianChars);
        }
    }
}
