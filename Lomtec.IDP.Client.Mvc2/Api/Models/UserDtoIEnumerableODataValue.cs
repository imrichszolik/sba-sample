// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lomtec.Proxy.Client.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class UserDtoIEnumerableODataValue
    {
        /// <summary>
        /// Initializes a new instance of the UserDtoIEnumerableODataValue
        /// class.
        /// </summary>
        public UserDtoIEnumerableODataValue()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the UserDtoIEnumerableODataValue
        /// class.
        /// </summary>
        public UserDtoIEnumerableODataValue(IList<UserDto> value = default(IList<UserDto>))
        {
            Value = value;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public IList<UserDto> Value { get; set; }

    }
}
