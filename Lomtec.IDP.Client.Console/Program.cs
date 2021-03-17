using IdentityModel;
using IdentityModel.Client;
using Lomtec.Proxy.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Lomtec.IDP.Client.Console {
    /// <summary>
    /// 
    /// </summary>
    class Program {

        /////// <summary>
        /////// SBA
        /////// </summary>
        private const string IDP = "https://idp.sba.develop.lomtec.com/";
        private const string API = "https://identityapi.sba.develop.lomtec.com/";
        private const string ClientId = "729f4fb8-3405-498f-8244-7cc316698522";
        private const string ClientSecret = "a2sXha4Znn7LhBGrOBp6tTAAVV1bKmfGEZdbcl7SafIJRO2E450EqXc5ffdedbjC";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args) {
            // SharedSecret
            var token = await GetTokenWithSharedSecret();

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(API);
            httpClient.SetBearerToken(token.AccessToken);

            //create sample data
            var result = await Lomtec.IDP.Client.Demo.Sample.DemoUser(httpClient);
            System.Console.WriteLine(result);

            if (System.Diagnostics.Debugger.IsAttached) {
                System.Console.WriteLine("Press any key to exit");
                System.Console.ReadKey();
            }
        }

        private static async Task<TokenResponse> GetTokenWithSharedSecret() {
            var httpClient = new HttpClient();
            //tokenclient
            var tokenClient = new TokenClient(httpClient, new TokenClientOptions() { Address = $"{IDP}connect/token", ClientId = ClientId, ClientSecret = ClientSecret });
            //get token
            var token = await tokenClient.RequestClientCredentialsTokenAsync(scope: "identity");
            return token;
        }
    }
}
