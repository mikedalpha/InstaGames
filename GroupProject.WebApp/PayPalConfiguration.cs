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
            ClientId = "Abyuy-820caOjBjm0r0WZW7dML01numyBKJRDqJQAbHxw3K2AhZV5G5UKw98i0mWr75kAkMqCW5zRv0J";
            ClientSecret = "ELz5ypWHBC7-pvsTKDPaCSl85tNfZUdxAQnygaXY--2_IIwtTaiHld5ps7T6FPRzU8OzVIxva-RGxGxJ";
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

