using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.PaymentServices
{
    public class VnPayService
    {
        private SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
        private SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());

        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }

        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _responseData.Add(key, value);
            }
        }

        public string GetResponseData(string key)
        {
            _responseData.TryGetValue(key, out var value);
            return value;
        }

        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret, SortedDictionary<string, string> vnp_Params)
        {
            var querystring = string.Join("&", vnp_Params.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

            var signData = HmacSHA512(vnp_HashSecret, querystring);

            var paymentUrl = $"{baseUrl}?{querystring}&vnp_SecureHash={signData}";

            return paymentUrl;
        }

        public string CreateSignature(string vnp_HashSecret, SortedDictionary<string, string> vnp_Params)
        {
            var querystring = string.Join("&", vnp_Params.Select(kvp => $"{kvp.Key}={(kvp.Value)}"));

            var signData = HmacSHA512(vnp_HashSecret, querystring);

            return signData;
        }

        public string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputData));
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString();
        }

        public bool ValidateSignature(string inputHash, string secretKey)
        {
            var rawData = new StringBuilder();
            foreach (var kv in _responseData)
            {
                if (rawData.Length > 0)
                {
                    rawData.Append("&");
                }
                rawData.Append(kv.Key + "=" + kv.Value);
            }

            var signData = rawData.ToString();
            var vnp_SecureHash = HmacSHA512(secretKey, signData);
            return vnp_SecureHash.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

        public class VnPayCompare : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == y) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                return string.CompareOrdinal(x, y);
            }
        }

    }
}
