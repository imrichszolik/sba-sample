using IdentityModel.Client;
using Lomtec.IDP.Client.Mvc2;
using Lomtec.Proxy.Client;
using Lomtec.Proxy.Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MvcHybrid.Controllers {
    public class HomeController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDiscoveryCache _discoveryCache;
        private readonly ClientConfiguration options;
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="discoveryCache">The discovery cache.</param>
        /// <param name="configuration">The configuration.</param>
        public HomeController(IHttpClientFactory httpClientFactory, IDiscoveryCache discoveryCache, IOptions<ClientConfiguration> options) {
            _httpClientFactory = httpClientFactory;
            _discoveryCache = discoveryCache;
            this.options = options.Value;
        }
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() {
            return View();
        }

        /// <summary>
        /// Secures this instance.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Secure() {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> GetUserInfo() {
            var token = await HttpContext.GetTokenAsync("access_token");

            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(token);

            var response = await client.GetStringAsync(options.Authority + "connect/userinfo");
            ViewBag.Json = JObject.Parse(response).ToString();

            return View("CallApi");
        }

        [Authorize]
        public async Task<IActionResult> GetUsers() {
            var token = await HttpContext.GetTokenAsync("access_token");

            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(token);

            var response = await client.GetStringAsync(options.IdentityApi + "api/v1/user");
            ViewBag.Json = JObject.Parse(response).ToString();

            return View("CallApi");
        }

        [Authorize]
        public async Task<IActionResult> GetRoles() {
            var token = await HttpContext.GetTokenAsync("access_token");

            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(token);

            var response = await client.GetStringAsync(options.IdentityApi + "api/v1/role");
            ViewBag.Json = JObject.Parse(response).ToString();

            return View("CallApi");
        }

        [Authorize]
        public async Task<IActionResult> CreateUser() {
            var token = await HttpContext.GetTokenAsync("access_token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(options.IdentityApi);
            client.SetBearerToken(token);

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
                    textResult.AppendFormat("User -> {0}",JObject.FromObject(test).ToString());
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

            ViewBag.Json = textResult.ToString();
            return View("CallApi");
        }

        public async Task<IActionResult> RenewTokens() {
            var disco = await _discoveryCache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            var rt = await HttpContext.GetTokenAsync("refresh_token");
            var tokenClient = _httpClientFactory.CreateClient();

            var tokenResult = await tokenClient.RequestRefreshTokenAsync(new RefreshTokenRequest {
                Address = disco.TokenEndpoint,

                ClientId = options.ClientId,
                ClientSecret = options.ClientSecret,
                RefreshToken = rt
            });

            if (!tokenResult.IsError) {
                var old_id_token = await HttpContext.GetTokenAsync("id_token");
                var new_access_token = tokenResult.AccessToken;
                var new_refresh_token = tokenResult.RefreshToken;
                var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResult.ExpiresIn);

                var info = await HttpContext.AuthenticateAsync("Cookies");

                info.Properties.UpdateTokenValue("refresh_token", new_refresh_token);
                info.Properties.UpdateTokenValue("access_token", new_access_token);
                info.Properties.UpdateTokenValue("expires_at", expiresAt.ToString("o", CultureInfo.InvariantCulture));

                await HttpContext.SignInAsync("Cookies", info.Principal, info.Properties);
                return Redirect("~/Home/Secure");
            }

            ViewData["Error"] = tokenResult.Error;
            return View("Error");
        }

        public IActionResult Logout() {
            return new SignOutResult(new[] { "Cookies", "oidc" });
        }

        public async Task<IActionResult> LogoutLocal() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Secure");
        }

        public IActionResult Error() {
            return View();
        }
    }
}