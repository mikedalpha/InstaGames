using System.Collections.Generic;
using PayPal.Api;

namespace GroupProject.WebApp
{
    public class PayPalConfiguration
    {
        public static readonly string ClientId;
        public static readonly string ClientSecret;

        static PayPalConfiguration()
        {
            var config = GetConfig();
            ClientId = "";
            ClientSecret = "";
        }

        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId , ClientSecret , GetConfig()).GetAccessToken();
            return accessToken;
        }

        //This will return APIContext object
        public static APIContext GetApiContext()
        {
            var apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}

