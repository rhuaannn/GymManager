using Microsoft.AspNetCore.Identity;

namespace GymManager.Data;

public class ApplicationUser : IdentityUser
{
    public bool IsAdmin { get; set; } = false; 
}

