using Microsoft.AspNetCore.Identity;
using Perevod36.Features.Common;
using System.ComponentModel.DataAnnotations;

namespace Perevod36.Features.ApplicationUsers;

public class ApplicationUser : IdentityUser<Guid>
{
    //хранение ФИО в одной колонке вместо трех - сознательное решение для упрощения
    [Required]
    [MaxLength(Constants.MaxLenghtName)]
    public string FullName { get; set; } = null!;

}
