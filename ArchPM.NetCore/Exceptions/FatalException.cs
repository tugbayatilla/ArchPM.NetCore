﻿using System;

namespace ArchPM.NetCore.Exceptions
{
    /// <summary>
    /// No log, no action anymore.
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <seealso cref="ArchPM.NetCore.Exceptions.IArchPMNetCoreException" />
    public class FatalException : Exception, IArchPMNetCoreException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FatalException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FatalException(String message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FatalException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public FatalException(String message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FatalException"/> class.
        /// </summary>
        public FatalException()
        {

        }
    }
}
