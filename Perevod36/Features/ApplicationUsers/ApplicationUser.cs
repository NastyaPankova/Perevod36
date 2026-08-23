using Microsoft.AspNetCore.Identity;
using Perevod36.Features.Common;
using System.ComponentModel.DataAnnotations;

namespace Perevod36.Features.ApplicationUsers;

public class ApplicationUser : IdentityUser<Guid>
{
    [Required]
    [MaxLength(Constants.MaxLenghtName)]
    public required string FullName { get; set; }

}
