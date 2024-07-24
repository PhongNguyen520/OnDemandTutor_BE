using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PaymentServices
{
    public class MomoConfig
    {
        public static class MoMoConfig
        {
            public static string PartnerCode = "MOMO";
            public static string AccessKey = "F8BBA842ECF85";
            public static string SecretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";
            public static string Endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
            public static string RedirectUrl = "http://localhost:3000/classes";
            public static string IpnUrl = "https://momo.vn";
        }

    }
}
