using System.ComponentModel.DataAnnotations;
using Fluxus.Common.Security.Interfaces;
using Fluxus.Domain.Common;

namespace Fluxus.Domain.Entities
{
    public class User : BaseEntity, IUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User";
    }
}
