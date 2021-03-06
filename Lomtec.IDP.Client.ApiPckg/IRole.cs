// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lomtec.Proxy.Client
{
    using Microsoft.Rest;
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Role operations.
    /// </summary>
    public partial interface IRole
    {
        /// <summary>
        /// Get list of the role.
        /// </summary>
        /// <param name='filter'>
        /// Restricts the set of items returned. The maximum number of
        /// expressions is 100. The allowed functions are: allfunctions.
        /// </param>
        /// <param name='orderby'>
        /// Specifies the order in which items are returned. The maximum number
        /// of expressions is 5.
        /// </param>
        /// <param name='top'>
        /// Limits the number of items returned from a collection. The maximum
        /// value is 20.
        /// </param>
        /// <param name='skip'>
        /// Excludes the specified number of items of the queried collection
        /// from the result.
        /// </param>
        /// <param name='count'>
        /// Indicates whether the total count of items within a collection are
        /// returned in the result.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        Task<HttpOperationResponse<RoleDtoIEnumerableODataValue>> GetRolesWithHttpMessagesAsync(string filter = default(string), string orderby = default(string), int? top = default(int?), int? skip = default(int?), bool? count = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Create the role.
        /// </summary>
        /// <param name='body'>
        /// The role.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        Task<HttpOperationResponse<RoleDto>> CreateRoleWithHttpMessagesAsync(RoleCreateDto body = default(RoleCreateDto), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Update the role.
        /// </summary>
        /// <param name='body'>
        /// The role.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        Task<HttpOperationResponse<RoleDto>> UpdateRoleWithHttpMessagesAsync(RoleUpdateDto body = default(RoleUpdateDto), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Get the role.
        /// </summary>
        /// <param name='roleId'>
        /// The role identifier.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        Task<HttpOperationResponse<RoleDto>> GetRoleWithHttpMessagesAsync(System.Guid roleId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Delete the role.
        /// </summary>
        /// <param name='roleId'>
        /// The role identifier.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        Task<HttpOperationResponse<RoleDto>> DeleteRoleWithHttpMessagesAsync(System.Guid roleId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Get role users.
        /// </summary>
        /// <param name='roleId'>
        /// The role identifier.
        /// </param>
        /// <param name='filter'>
        /// Restricts the set of items returned. The maximum number of
        /// expressions is 100. The allowed functions are: allfunctions.
        /// </param>
        /// <param name='orderby'>
        /// Specifies the order in which items are returned. The maximum number
        /// of expressions is 5.
        /// </param>
        /// <param name='top'>
        /// Limits the number of items returned from a collection. The maximum
        /// value is 20.
        /// </param>
        /// <param name='skip'>
        /// Excludes the specified number of items of the queried collection
        /// from the result.
        /// </param>
        /// <param name='count'>
        /// Indicates whether the total count of items within a collection are
        /// returned in the result.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        Task<HttpOperationResponse<UserDtoIEnumerableODataValue>> GetRoleUsersWithHttpMessagesAsync(System.Guid roleId, string filter = default(string), string orderby = default(string), int? top = default(int?), int? skip = default(int?), bool? count = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
