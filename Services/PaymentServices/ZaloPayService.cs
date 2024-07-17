using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.PaymentServices;

public class ZaloPayService
{
    public async Task<string> CreateOrderAsync(decimal amount, string description)
    {
        var orderId = Guid.NewGuid().ToString();
        var appTransId = DateTime.Now.ToString("yyMMdd") + "_" + orderId;
        var appTime = GetTimeStamp(DateTime.Now);

        var embeddata = new { merchantinfo = "embeddata123" };
        var items = new[]
        {
            new { itemid = "knb", itemname = "kim nguyen bao", itemprice = 198400, itemquantity = 1 }
        };

        var param = new Dictionary<string, string>
        {
            { "appid", ZaloPayConfig.AppId.ToString() },
            { "appuser", "demo" },
            { "apptime", appTime.ToString() },
            { "amount", amount.ToString() }, 
            { "apptransid", appTransId },
            { "embeddata", JsonConvert.SerializeObject(embeddata) },
            { "item", JsonConvert.SerializeObject(items) },
            { "description", description },
            { "bankcode", "zalopayapp" }
        };

        var data = param["appid"] + "|" + param["apptransid"] + "|" + param["appuser"] + "|" + param["amount"] + "|"
                   + param["apptime"] + "|" + param["embeddata"] + "|" + param["item"];

        var mac = CreateHMACSHA256Signature(data, ZaloPayConfig.Key1);
        param.Add("mac", mac);

        using (var client = new HttpClient())
        {
            var content = new FormUrlEncodedContent(param);
            var response = await client.PostAsync(ZaloPayConfig.Endpoint, content);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Request Data: " + JsonConvert.SerializeObject(param));
            Console.WriteLine("Response: " + responseString);

            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                if (responseData.ContainsKey("returncode") && responseData["returncode"].ToString() == "1")
                {
                    return responseData["orderurl"].ToString();
                }
                else if (responseData.ContainsKey("returnmessage"))
                {
                    throw new Exception(responseData["returnmessage"].ToString());
                }
                else
                {
                    throw new Exception("Unexpected response format: " + responseString);
                }
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

    public static long GetTimeStamp(DateTime date)
    {
        return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
    }
}
