using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using System;
using System.IO;
using System.Threading;

namespace Kakadu.Backend.Services
{
    public class EmailService
    {
        private static readonly string SENDER_NAME = "KakaduTEAM";
        private static readonly string FROM_MAIL = "kakaduhoreca@gmail.com";
        private static readonly string APPLICATION_NAME = "KakaduHoreca";

        private readonly UserCredential _credential;
        private readonly GmailService _service;

        public EmailService()
        {
            _credential = GetCredential();
            _service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = _credential,
                ApplicationName = APPLICATION_NAME
            });
        }

        private UserCredential GetCredential()
        {
            using (var stream = new FileStream("credential.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "GoogleToken";
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { GmailService.Scope.GmailSend, GmailService.Scope.GmailCompose },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
        }

        public void SendEmail(string recipient, string message, string subject)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(SENDER_NAME, FROM_MAIL));
            msg.To.Add(new MailboxAddress("", recipient));
            msg.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;
            msg.Body = bodyBuilder.ToMessageBody();

            using (var stream = new MemoryStream())
            {
                msg.WriteTo(stream);
                var emailMessage = new Message
                {
                    Raw = Base64UrlEncode(stream.ToArray())
                };

                _service.Users.Messages.Send(emailMessage, "me").Execute();
            }
        }

        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
    }
}
