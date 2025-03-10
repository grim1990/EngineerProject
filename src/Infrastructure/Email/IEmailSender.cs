namespace Infrastructure.Email;

public interface IEmailSender
{
    Task SendConfirmationAsync(string emailAddress, string callbackUrl);
    Task SendReminderAsync(string emailAddress);
}