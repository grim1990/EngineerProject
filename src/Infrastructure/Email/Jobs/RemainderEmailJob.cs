using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Infrastructure.Email.Jobs;

public class RemainderEmailJob : IJob
{
    private readonly IdentityDbContext _context;
    private readonly IEmailSender _emailSender;

    public RemainderEmailJob(IdentityDbContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var lastThreeDays = DateTime.Now.AddDays(-3);
        var users = await _context.Users.Where(u => u.LastLoginTime <= lastThreeDays).ToListAsync();

        foreach (var user in users)
        {
            await _emailSender.SendReminderAsync(user.Email!);
        }
    }
}