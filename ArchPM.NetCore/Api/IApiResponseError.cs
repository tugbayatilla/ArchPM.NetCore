using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
