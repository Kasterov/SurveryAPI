namespace Application.Abstractions.Common;

public interface IMailSenderService
{
    public string SendMail(string subject, string bodyMessage, string recipient);
}
