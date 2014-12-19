using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Conditions
{
    /// <summary>
    /// This enumeration is used to determine the type of exception the validator should throw.
    /// </summary>
    public enum ConstraintViolationType
    {
        /// <summary>Lets the Validator to throw the default exception for that instance.</summary>
        Default = 0,

        /// <summary>
        /// Lets the Validator optionally throw an exception type appropriate for values that are out of range.
        /// </summary>
        OutOfRangeViolation,

        /// <summary>
        /// Lets the Validator optionally throw an <see cref="System.ComponentModel.InvalidEnumArgumentException"/>.
        /// </summary>
        InvalidEnumViolation,
    }
}
