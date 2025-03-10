using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class IdentityUserModel : IdentityUser
{
    public virtual DateTime? LastLoginTime { get; set; }
    public virtual DateTime? RegistrationDate { get; set; }
}
