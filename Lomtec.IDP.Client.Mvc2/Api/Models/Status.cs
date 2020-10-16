// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lomtec.Proxy.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Status
    {
        /// <summary>
        /// Initializes a new instance of the Status class.
        /// </summary>
        public Status()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Status class.
        /// </summary>
        public Status(bool? succeeded = default(bool?), string message = default(string), string code = default(string))
        {
            Succeeded = succeeded;
            Message = message;
            Code = code;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "succeeded")]
        public bool? Succeeded { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

    }
}
