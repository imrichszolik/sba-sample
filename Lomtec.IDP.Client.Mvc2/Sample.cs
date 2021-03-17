using Lomtec.Proxy.Client;
using Lomtec.Proxy.Client.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lomtec.IDP.Client.Demo {
    /// <summary>
    /// 
    /// </summary>
    public class Sample {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task<string> DemoUser(HttpClient client) {

            var api = new IdentityLomtecIDPAPI(client);

            //user to create
            var userToCreate = new UserCreateDto() {
                FirstName = "First",
                LastName = "Last",
                Email = "first.last@example.com"
            };

            //remove test user if exist...
            var user = api.User.GetUsers(filter: $"email eq '{userToCreate.Email}'", top: 2)?.Value?.SingleOrDefault();
            if (user != null) await api.User.DeleteUserAsync(user.UserId);

            //status json 
            var textResult = new StringBuilder();

            //get first role
            var clientRole = (await api.Role.GetRolesAsync("name eq 'client'", top: 2)).Value.SingleOrDefault();
            if (clientRole != null) {

                var result = await api.User.CreateUserAsync(userToCreate);
                if (result is UserDto) {
                    user = result as UserDto;
                    textResult.AppendFormat("NewUser -> {0}", JObject.FromObject(user).ToString());
                    textResult.AppendLine();
                    textResult.AppendLine();

                    //lockout user
                    await api.User.LockoutAsync(user.UserId, true);
                    //add role
                    await api.User.AddUserRoleAsync(user.UserId, clientRole.RoleId);
                    //add property
                    await api.User.SetPropertyAsync(user.UserId, new UserPropertyDto() { PropertyName = "subject-id-number", PropertyValue = "01234567" });

                    //get data
                    var test = await api.User.GetUserAsync(user.UserId);
                    textResult.AppendFormat("User -> {0}", JObject.FromObject(test).ToString());
                    textResult.AppendLine();
                    textResult.AppendLine();
                    var testrole = await api.User.GetUserRolesAsync(user.UserId);
                    textResult.AppendFormat("Roles -> {0}", JObject.FromObject(testrole).ToString());
                    textResult.AppendLine();
                    textResult.AppendLine();
                    var testproperty = await api.User.GetPropertiesAsync(user.UserId);
                    textResult.AppendFormat("Properties -> {0}", JObject.FromObject(testproperty).ToString());
                    textResult.AppendLine();
                }
                else {
                    textResult.AppendFormat("NewUser -> {0}", JObject.FromObject(result).ToString());
                }
            }
            else {
                textResult.Append("Client role is missing!");
            }

            return textResult.ToString();
        }
    }
}
