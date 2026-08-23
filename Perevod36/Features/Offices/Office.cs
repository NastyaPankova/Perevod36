using Perevod36.Features.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perevod36.Features.Offices;

public class Office
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public OfficeId Id { get; init; }
    [MaxLength(Constants.MaxLengthOfficeName)]
    public string OfficeName { get; set; } = null!;

    [Phone]
    [MaxLength(Constants.MaxLengthPhoneNumber)]
    public string PhoneNumber { get; set; } = null!;

    [MaxLength(Constants.MaxLengthAddres)]
    public string Addres { get; set; } = null!;
}
