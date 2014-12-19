using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Howell.Conditions
{
    /// <summary>
    /// The exception that is thrown when a method's postcondition is not valid.
    /// </summary>
    [Serializable]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly",
        MessageId = "Postcondition", Justification = "Postcondition is actually a word. " +
        "See: http://en.wikipedia.org/wiki/Postcondition.")]
    public sealed class PostconditionException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="PostconditionException"/> class.</summary>
        public PostconditionException()
            : this(SR.GetString(SR.PostconditionFailed))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PostconditionException"/> class with a
        /// specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public PostconditionException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PostconditionException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified.</param>
        public PostconditionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PostconditionException class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about
        /// the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information 
        /// about the source or destination.</param>
        private PostconditionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
