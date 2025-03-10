namespace Infrastructure.Email.Options;

public class EmailOptions
{
    public const string Smtp = "Smtp";

    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string FromAddress { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string CronExpression { get; set; } = null!;
}