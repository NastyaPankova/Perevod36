using Perevod36.Features.ApplicationUsers;
using Perevod36.Features.Common;
using Perevod36.Features.Languages;

namespace Perevod36.Features.Translators;

public class Translator : ApplicationUser
{
    public bool IsFreelance { get; set; } = false;

    public ICollection<Language> Languages { get; set; } = new List<Language>();
}
