using Perevod36.Features.Common;
using Perevod36.Features.ApplicationUsers;
using Perevod36.Features.Offices;

namespace Perevod36.Features.Managers;

public class Manager: ApplicationUser
{
    public OfficeId OfficeId { get; set; }

    public Office Office { get; set; } = null!;
}
