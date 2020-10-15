using IdentityModel;
using IdentityModel.Client;
using Lomtec.IDP.Client.Mvc2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcHybrid {
    public class Startup {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) {

            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services) {

            services.AddMvc();
            services.AddHttpClient();

            var config = Configuration.GetSection("ClientConfig").Get<ClientConfiguration>();
            services.Configure<ClientConfiguration>((o) => { o.Authority = config.Authority; o.IdentityApi = config.IdentityApi; o.ClientId = config.ClientId; o.ClientSecret = config.ClientSecret; });

            services.AddSingleton<IDiscoveryCache>(r => {
                var factory = r.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache(config.Authority, () => factory.CreateClient());
            });

            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie(options => {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.Cookie.Name = "test-client-2";
                })
                .AddOpenIdConnect("oidc", options => {

                    //mame reverzne proxy na https preto potrebujeme toto...
                    if (!string.IsNullOrEmpty(config.MyUrl)) {
                        options.Events.OnRedirectToIdentityProvider = async context => {
                            context.ProtocolMessage.RedirectUri = config.MyUrl + "signin-oidc";
                            await Task.CompletedTask;
                        };
                    }

                    options.Authority = config.Authority;
                    options.RequireHttpsMetadata = false;

                    options.ClientId = config.ClientId;
                    options.ClientSecret = config.ClientSecret;
                    options.ResponseType = "code id_token";
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("role");
                    options.Scope.Add("offline_access");

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.ClaimActions.MapAllExcept("iss", "nbf", "exp", "aud", "nonce", "iat", "c_hash");

                    options.TokenValidationParameters.RoleClaimType = JwtClaimTypes.Role;
                    options.TokenValidationParameters.NameClaimType = JwtClaimTypes.Name;
                });

            
        }

        public void Configure(IApplicationBuilder app) {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
