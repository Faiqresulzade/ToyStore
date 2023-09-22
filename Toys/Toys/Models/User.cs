using Microsoft.AspNetCore.Identity;

namespace Toys.Models
{

    public class User:IdentityUser
    {
        internal string Name { get; set; }
        internal string Surname { get; set; }
    }
}
