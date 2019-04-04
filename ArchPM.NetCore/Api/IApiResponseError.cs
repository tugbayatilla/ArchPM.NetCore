using System.Collections.Generic;

namespace ArchPM.NetCore.Api
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApiResponseError
    {
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        List<ApiException> ApiExceptions { get; set; }
    }
}
