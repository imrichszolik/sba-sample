using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lomtec.IDP.Client.Mvc2 {
    /// <summary>
    /// 
    /// </summary>
    public class ClientConfiguration {
        /// <summary>
        /// Gets or sets the authority.
        /// </summary>
        /// <value>
        /// The authority.
        /// </value>
        public string Authority { get; set; }
        /// <summary>
        /// Gets or sets the identity API.
        /// </summary>
        /// <value>
        /// The identity API.
        /// </value>
        public string IdentityApi { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }
        /// <summary>
        /// Gets or sets the client secreet.
        /// </summary>
        /// <value>
        /// The client secreet.
        /// </value>
        public string ClientSecret { get; set; }
        /// <summary>
        /// Gets or sets my URL.
        /// </summary>
        /// <value>
        /// My URL.
        /// </value>
        public string MyUrl { get; set; }
    }
}
