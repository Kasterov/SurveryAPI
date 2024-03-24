using Application.Abstractions.Common;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Mail;

public class MailSenderService : IMailSenderService
{
    private readonly IConfiguration _builder;

    public MailSenderService(IConfiguration builder)
    {
        _builder = builder;
    }

    public string SendMail(string subject, string bodyMessage, string recipient)
    {
        var email = new MimeMessage();
        string? sender = _builder.GetSection("Mail:Sender").Value;
        string? password = _builder.GetSection("Mail:Password").Value;

        email.From.Add(MailboxAddress.Parse(sender)); 
        email.To.Add(MailboxAddress.Parse(recipient));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = bodyMessage };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(sender, password);

        string res = smtp.Send(email);
        smtp.Disconnect(true);

        return res;
    }
}
