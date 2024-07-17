using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Services.PaymentServices.MomoConfig;

namespace Services.PaymentServices
{
    public class MomoService
    {
        public async Task<string> CreateOrderAsync(decimal amount, string description, string orderId)
        {
            var requestId = Guid.NewGuid().ToString();
            var requestType = "captureMoMoWallet";
            var extraData = ""; // Dữ liệu thêm nếu có

            var rawHash = $"partnerCode={MoMoConfig.PartnerCode}&accessKey={MoMoConfig.AccessKey}&requestId={requestId}&amount={amount}&orderId={orderId}&orderInfo={description}&returnUrl={MoMoConfig.ReturnUrl}&notifyUrl={MoMoConfig.NotifyUrl}&extraData={extraData}";
            var signature = CreateHMACSHA256Signature(rawHash, MoMoConfig.SecretKey);

            var param = new Dictionary<string, string>
        {
            { "partnerCode", MoMoConfig.PartnerCode },
            { "accessKey", MoMoConfig.AccessKey },
            { "requestId", requestId },
            { "amount", amount.ToString() },
            { "orderId", orderId },
            { "orderInfo", description },
            { "returnUrl", MoMoConfig.ReturnUrl },
            { "notifyUrl", MoMoConfig.NotifyUrl },
            { "extraData", extraData },
            { "requestType", requestType },
            { "signature", signature }
        };

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(MoMoConfig.Endpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Request Data: " + JsonConvert.SerializeObject(param));
                Console.WriteLine("Response: " + responseString);

                var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                if (responseData.ContainsKey("payUrl"))
                {
                    return responseData["payUrl"].ToString();
                }
                else if (responseData.ContainsKey("errorCode"))
                {
                    throw new Exception(responseData["localMessage"].ToString());
                }
                else
                {
                    throw new Exception("Unexpected response format: " + responseString);
                }
            }
        }

        public static string CreateHMACSHA256Signature(string data, string key)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(data);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string hex = BitConverter.ToString(hashmessage);
                hex = hex.Replace("-", "").ToLower();
                return hex;
            }
        }
    }
}
