using System;

namespace ArchPM.NetCore.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiException
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public String Message { get; set; }
        /// <summary>
        /// Gets or sets the detailed message.
        /// </summary>
        /// <value>
        /// The detailed message.
        /// </value>
        public String DetailedMessage { get; set; }
    }
}
