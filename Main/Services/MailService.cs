
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace API.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
        Task SendToken(string email, string subject, string htmlMessage);
        Task SendTutorInter(string email, string subject, string content);
    }
    public class MailService : IMailService
    {
        public MailService()
        {

        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(new MailboxAddress("OnDemandTutor", "phucplhse173164@fpt.edu.vn"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;

            string htmlBody = @"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f9f9f9;
            margin: 0;
            padding: 0;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
        .wrapper {
            display: flex;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .form {
            width: 100%;
            padding: 20px;
            border-radius: 10px;
            background-color: #fff3e0;
        }
        .header {
            text-align: center;
            margin-bottom: 20px;
        }
        .logo img {
            height: 90px;
            margin: 0 auto;
            display: block;
        }
        .header h1 {
            font-size: 24px;
            color: #ff5722;
            margin-bottom: 10px;
        }
        .headertext {
            font-size: 16px;
            color: #ff9800;
            margin-bottom: 30px;
        }
        .confirm-button {
            border: 1px solid #4caf50;
            background-color: #4caf50;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding: 12px 45px;
            text-transform: uppercase;
            transition: transform 80ms ease-in;
            width: 200px;
            margin: 0 auto;
            display: block;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
        }
        .confirm-button:hover {
            background-color: #388e3c;
            border-color: #388e3c;
        }
        .footer {
            text-align: center;
            margin-top: 40px;
            padding-top: 20px;
            border-top: 1px solid #dddddd;
            color: #777777;
            font-size: 14px;
        }
        .footer b {
            display: block;
            margin-bottom: 10px;
        }
        .footer span {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            margin-bottom: 5px;
        }
        .footer span.emoji {
            margin-right: 5px;
        }
        .footer div {
            text-align: center;
        }
    </style>
</head>

<body>
    <div class=""wrapper"">
        <div class=""form"">
            <div class=""header"">
                <div class=""logo"">
                    <img src=""https://d226aj4ao1t61q.cloudfront.net/ai2shais_blog_confirmationmail.png"" alt=""Logo"">
                </div>
                <h1>On Demand Tutor <span class=""emoji"">🎉</span></h1>
                <div class=""headertext"">
                    <span>(Chúng tôi cần xác thực địa chỉ email của bạn để kích hoạt tài khoản)</span>
                </div>
                <a href=""" + htmlMessage + @""" class=""confirm-button"">Xác nhận</a>
            </div>
            <div class=""footer"">
                <div><span class=""emoji"">🏫</span> <span>FPT University.</span></div>
                <b>Địa chỉ của chúng tôi:</b>
                <div><span class=""emoji"">📍</span> <span>Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh.</span></div>
                <div><span class=""emoji"">✉️</span> <span>ondemandtutor1809@gmail.com</span></div>
                <div><span class=""emoji"">📞</span> <span>Hotline: 0337523349</span></div>
            </div>
        </div>
    </div>
</body>
</html>";

            emailToSend.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlBody
            };
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                emailClient.Authenticate("phucplhse173164@fpt.edu.vn", "gjcmqiztazynynya");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;
        }

        public Task SendToken(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(new MailboxAddress("OnDemandTutor", "phucplhse173164@fpt.edu.vn"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;

            string htmlBody = @"
<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
        .wrapper {
            display: flex;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .form {
            width: 100%;
            padding: 20px;
            border-radius: 10px;
            background-color: #fafafa;
        }
        .header {
            text-align: center;
            margin-bottom: 20px;
        }
        .logo img {
            height: 90px;
            margin: 0 auto;
            display: block;
        }
        .header h1 {
            font-size: 24px;
            color: #333;
            margin-bottom: 20px;
        }
        .headertext {
            font-size: 16px;
            color: #666;
            margin-bottom: 30px;
        }
        .confirm-button {
            border: 1px solid #039a21;
            background-color: #039a21;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding: 12px 45px;
            text-transform: uppercase;
            transition: transform 80ms ease-in;
            width: 200px;
            margin: 0 auto;
            display: block;
            text-decoration: none;
            border-radius: 5px;
        }
        .confirm-button:hover {
            background-color: #027a1a;
            border-color: #027a1a;
        }
        .footer {
            text-align: center;
            margin-top: 40px;
            padding-top: 20px;
            border-top: 1px solid #dddddd;
            color: #777777;
            font-size: 14px;
        }
        .footer b {
            display: block;
            margin-bottom: 10px;
        }
        .footer span {
            display: block;
            margin-bottom: 5px;
        }
    </style>
</head>

<body>
    <div class=""wrapper"">
        <div class=""form"">
            <div class=""header"">
                <div class=""logo"">
                    <img src=""https://d226aj4ao1t61q.cloudfront.net/ai2shais_blog_confirmationmail.png"" alt=""Logo"">
                </div>
                <h1>On Demand Tutor</h1>
                <div class=""headertext"">
                    <span>(Click Xác Nhận để thay đổi mật khẩu của bạn)</span>
                </div>
                <a href=""" + htmlMessage + @""" class=""confirm-button"">Xác nhận</a>
            </div>
            <div class=""footer"">
                <span>FPT University.</span>
                <b>Địa chỉ của chúng tôi:</b>
                <span>Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh.</span>
                <span>ondemandtutor1809@gmail.com</span>
                <span>Hotline: 0337523349</span>
            </div>
        </div>
    </div>
</body>

</html>";

            emailToSend.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlBody
            };
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                emailClient.Authenticate("phucplhse173164@fpt.edu.vn", "gjcmqiztazynynya");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;
        }

        public Task SendTutorInter(string email, string subject, string content)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(new MailboxAddress("OnDemandTutor", "phucplhse173164@fpt.edu.vn"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;

            string htmlBody = @"
<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
        .wrapper {
            display: flex;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .form {
            width: 100%;
            background-color: #efc15f5c;
            padding: 20px;
            border-radius: 10px;
        }
        .header {
            text-align: center;
            margin-bottom: 20px;
        }
        .logo img {
            height: 90px;
            margin: 0 auto;
            display: block;
        }
        .headertext {
            margin-top: 25px;
            margin-bottom: 10px;
            font-size: 16px;
            color: #333333;
        }
        .footer {
            text-align: center;
            margin-top: 40px;
            padding-top: 20px;
            border-top: 1px solid #dddddd;
            color: #777777;
            font-size: 14px;
        }
        .footer b {
            display: block;
            margin-bottom: 10px;
        }
        .footer span {
            display: block;
            margin-bottom: 5px;
        }
    </style>
</head>

<body>
    <div class=""wrapper"">
        <div class=""form"">
            <div class=""header"">
                <div class=""logo"">
                    <img src=""https://d226aj4ao1t61q.cloudfront.net/ai2shais_blog_confirmationmail.png"" alt=""Logo"">
                </div>
                <h1>On Demand Tutor</h1>
                <div class=""headertext"">
                    <span>" + content + @"</span>
                </div>
            </div>
            <div class=""footer"">
                <span>FPT University.</span>
                <b>Địa chỉ của chúng tôi:</b>
                <span>Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh.</span>
                <span>ondemandtutor1809@gmail.com</span>
                <span>Hotline: 0337523349</span>
            </div>
        </div>
    </div>
</body>

</html>";

            emailToSend.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlBody
            };
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                emailClient.Authenticate("phucplhse173164@fpt.edu.vn", "gjcmqiztazynynya");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;
        }
    }
}
