using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Howell.Text
{
    /// <summary>
    /// NameValuePair
    /// </summary>
    [Serializable]
    internal sealed class NameValuePair
    {
        // Fields
        [OptionalField(VersionAdded = 2)]
        private readonly int _length;
        private readonly string _name;
        private NameValuePair _next;
        private readonly string _value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        internal NameValuePair(string name, string value, int length)
        {
            this._name = name;
            this._value = value;
            this._length = length;
        }

        /// <summary>
        /// 
        /// </summary>
        internal int Length
        {
            get
            {
                return this._length;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal string Name
        {
            get
            {
                return this._name;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal NameValuePair Next
        {
            get
            {
                return this._next;
            }
            set
            {
                if ((this._next != null) || (value == null))
                {
                    throw new InvalidOperationException("NameValuePair GetNext.");
                }
                this._next = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal string Value
        {
            get
            {
                return this._value;
            }
        }
    }


}
