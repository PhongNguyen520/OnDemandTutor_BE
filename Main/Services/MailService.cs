
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

            string htmlBody =
            @"<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>

<body>
    <div id=""wrapper"" style=""display: flex; max-width:600px; margin: 0 auto;"">
        <div class=""form"" style=""
        width: 600px;
        height: 800px;
        background-color: #efc15f5c;"">
            <div class=""header"" style=""margin-top: 50px;"">
                <div class=""logo""
                    style=""height: 90px; width: 100%;  margin: 10px auto; border-radius: 10px; display: flex; align-items: center; justify-content: center;"">
                    <img src=""https://d226aj4ao1t61q.cloudfront.net/ai2shais_blog_confirmationmail.png""
                        alt=""Logo"" srcset="""" style=""width: 100%; height: 100%; margin: 10px 5px 15px 60px;"">
                </div>
                <div class=""text"" style=""text-align: center;"">
                    <h1>On Demand Tutor</h1>
                    <a href=" + htmlMessage + @" 
                    style=""border: 1px solid #039a21;
                    background-color: #039a21;
                    color: #FFFFFF;
                    font-size: 12px;
                    font-weight: bold;
                    padding: 12px 45px;
                    letter-spacing: 1px;
                    text-transform: uppercase;
                    transition: transform 80ms ease-in;
                    width: 200px;
                    margin: 0 auto;
                    display: block;
                    font-size: 24px;
                    text-decoration: none;"">
                    Xác nhận
                    </a>
                    <div class=""headertext"" style=""margin-top: 25px; margin-bottom: 10px;"">
                        <span>(Chúng tôi cần xác thực địa chỉ email của bạn để kích hoạt tài khoản)</span>
                    </div>
                </div>
            </div>

            <div class=""footer"" style=""display: flex;  margin-top: 200px;"">
                <div class=""footercontent"" style=""width: 480px; text-align: center; margin: 0px auto;"">
                    <div class=""span1"" style=""margin-top: 10px; margin-bottom: 10px;"">
                        <span>FPT University.</span>
                    </div>
                    <div><b>Địa chỉ của chúng tôi:</b></div>
                    <div class=""span2"" style=""margin-top: 10px;"">
                        <span>Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh.</span>
                    </div>
                    <div class=""span3"" style=""margin-top: 10px;"">
                        <span>ondemandtutor1809@gmail.com</span>
                    </div>
                    <div class=""span4"" style=""margin-top: 10px;""><span>Hotline: 0337523349</span></div>
                </div>
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

            string htmlBody =
            @"<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>

<body>
    <div id=""wrapper"" style=""display: flex; max-width:600px; margin: 0 auto;"">
        <div class=""form"" style=""
        width: 600px;
        height: 800px;
        background-color: #efc15f5c;"">
            <div class=""header"" style=""margin-top: 50px;"">
                <div class=""logo""
                    style=""height: 90px; width: 100%;  margin: 10px auto; border-radius: 10px; display: flex; align-items: center; justify-content: center;"">
                    <img src=""https://d226aj4ao1t61q.cloudfront.net/ai2shais_blog_confirmationmail.png""
                        alt=""Logo"" srcset="""" style=""width: 100%; height: 100%; margin: 10px 5px 15px 60px;"">
                </div>
                <div class=""text"" style=""text-align: center;"">
                    <h1>On Demand Tutor</h1>
                    <a href=" + htmlMessage + @" 
                    style=""border: 1px solid #039a21;
                    background-color: #039a21;
                    color: #FFFFFF;
                    font-size: 12px;
                    font-weight: bold;
                    padding: 12px 45px;
                    letter-spacing: 1px;
                    text-transform: uppercase;
                    transition: transform 80ms ease-in;
                    width: 200px;
                    margin: 0 auto;
                    display: block;
                    font-size: 24px;
                    text-decoration: none;"">
                    Xác nhận
                    </a>
                    <div class=""headertext"" style=""margin-top: 25px; margin-bottom: 10px;"">
                        <span>(Click Xác Nhận để thay đổi mật khẩu của bạn)</span>
                    </div>
                </div>
            </div>

            <div class=""footer"" style=""display: flex;  margin-top: 200px;"">
                <div class=""footercontent"" style=""width: 480px; text-align: center; margin: 0px auto;"">
                    <div class=""span1"" style=""margin-top: 10px; margin-bottom: 10px;"">
                        <span>FPT University.</span>
                    </div>
                    <div><b>Địa chỉ của chúng tôi:</b></div>
                    <div class=""span2"" style=""margin-top: 10px;"">
                        <span>Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh.</span>
                    </div>
                    <div class=""span3"" style=""margin-top: 10px;"">
                        <span>ondemandtutor1809@gmail.com</span>
                    </div>
                    <div class=""span4"" style=""margin-top: 10px;""><span>Hotline: 0337523349</span></div>
                </div>
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

            string htmlBody =
            @"<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>

<body>
    <div id=""wrapper"" style=""display: flex; max-width:600px; margin: 0 auto;"">
        <div class=""form"" style=""
        width: 600px;
        height: 800px;
        background-color: #efc15f5c;"">
            <div class=""header"" style=""margin-top: 50px;"">
                <div class=""logo""
                    style=""height: 90px; width: 100%;  margin: 10px auto; border-radius: 10px; display: flex; align-items: center; justify-content: center;"">
                    <img src=""https://d226aj4ao1t61q.cloudfront.net/ai2shais_blog_confirmationmail.png""
                        alt=""Logo"" srcset="""" style=""width: 100%; height: 100%; margin: 10px 5px 15px 60px;"">
                </div>
                
                    <div class=""headertext"" style=""margin-top: 25px; margin-bottom: 10px;"">
                        <span>" + content + @"</span>
                    </div>
                </div>
            </div>

            <div class=""footer"" style=""display: flex;  margin-top: 200px;"">
                <div class=""footercontent"" style=""width: 480px; text-align: center; margin: 0px auto;"">
                    <div class=""span1"" style=""margin-top: 10px; margin-bottom: 10px;"">
                        <span>FPT University.</span>
                    </div>
                    <div><b>Địa chỉ của chúng tôi:</b></div>
                    <div class=""span2"" style=""margin-top: 10px;"">
                        <span>Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh.</span>
                    </div>
                    <div class=""span3"" style=""margin-top: 10px;"">
                        <span>ondemandtutor1809@gmail.com</span>
                    </div>
                    <div class=""span4"" style=""margin-top: 10px;""><span>Hotline: 0337523349</span></div>
                </div>
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
