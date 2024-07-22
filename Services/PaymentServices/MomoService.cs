using BusinessObjects;
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
        private readonly IPaymentTransactionService _transactionService = new PaymentTransactionService();
        private readonly IWalletService _walletService = new WalletService();

        public async Task<string> CreateOrderAsync(int type, decimal amount, string description, string orderId, string walletId, string paymentDestinationId)
        {
            var requestId = Guid.NewGuid().ToString();
            //var requestType = "captureWallet";
            var requestType = "payWithATM";
            //var requestType = "onDelivery";
            var extraData = "";
            var lang = "vi";

            var rawHash = $"accessKey={MoMoConfig.AccessKey}&amount={amount}&extraData={extraData}&ipnUrl={MoMoConfig.IpnUrl}&orderId={orderId}&orderInfo={description}&partnerCode={MoMoConfig.PartnerCode}&redirectUrl={MoMoConfig.RedirectUrl}&requestId={requestId}&requestType={requestType}";


            var signature = CreateHMACSHA256Signature(rawHash, MoMoConfig.SecretKey);

            var param = new Dictionary<string, string>
            {
                { "partnerCode", MoMoConfig.PartnerCode },
                { "accessKey", MoMoConfig.AccessKey },
                { "requestId", requestId },
                { "amount", amount.ToString() },
                { "orderId", orderId },
                { "orderInfo", description },
                { "redirectUrl", MoMoConfig.RedirectUrl },
                { "ipnUrl", MoMoConfig.IpnUrl },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature },
                { "lang", lang },
            };

            var payment = new PaymentTransaction()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = (float)amount,
                Description = description,
                TranDate = DateTime.Now,
                Type = type,
                IsValid = false,
                WalletId = walletId,
                PaymentDestinationId = paymentDestinationId
            };

            _transactionService.AddTransaction(payment);

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

        public async Task<bool> PaymentReturnAsync(string id, Dictionary<string, string> responseParams)
        {
            if (!responseParams.TryGetValue("signature", out var receivedSignature))
            {
                throw new Exception("Missing signature");
            }

            responseParams.Remove("signature");

            if (!VerifySignature(responseParams, MoMoConfig.SecretKey, receivedSignature))
            {
                throw new Exception("Invalid signature");
            }

            var payment = _transactionService.GetTransactions().FirstOrDefault(s => s.Id == id);
            if (payment != null)
            {
                var amount = float.Parse(responseParams["amount"]);
                if (responseParams["resultCode"] == "0")
                {
                    payment.IsValid = true;
                    _transactionService.UpdateTransaction(payment);

                    var wallet = _walletService.GetWallets().FirstOrDefault(w => w.WalletId == payment.WalletId);
                    if (wallet != null)
                    {
                        wallet.Balance += amount;
                        _walletService.UpdateWallets(wallet);
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public static bool VerifySignature(Dictionary<string, string> responseParams, string secretKey, string receivedSignature)
        {
            var sortedParams = responseParams.OrderBy(kvp => kvp.Key);
            var rawHash = string.Join("&", sortedParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var generatedSignature = CreateHMACSHA256Signature(rawHash, secretKey);
            return generatedSignature == receivedSignature;
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
