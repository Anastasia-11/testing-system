using Microsoft.AspNetCore.Identity;

namespace CourseWork.Models;

public class User : IdentityUser
{
    public int Group { get; set; }
    public IEnumerable<UserResult>? UserResults { get; set; }
}