#nullable disable

using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Api.Areas.Identity.Pages.Account;

public class ForgotPasswordModel : PageModel
{
    private readonly UserManager<IdentityUserModel> _userManager;
    private readonly IConfiguration _configuration;

    public ForgotPasswordModel(UserManager<IdentityUserModel> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _configuration = config;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var host = _configuration.GetValue<string>("Smtp:Host");
        var port = _configuration.GetValue<int>("Smtp:Port");
        var fromAddress = _configuration.GetValue<string>("Smtp:FromAddress");
        var userName = _configuration.GetValue<string>("Smtp:UserName");
        var password = _configuration.GetValue<string>("Smtp:Password");

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code },
                protocol: Request.Scheme);

            using (MailMessage mail = new())
            {
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(user.Email);
                mail.Subject = ("Reset email link");
                mail.Body = callbackUrl;
                mail.IsBodyHtml = true;

                using SmtpClient smtp = new(host, port);

                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }

            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        return Page();
    }
}