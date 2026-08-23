using Perevod36.Features.Common;
using Perevod36.Features.Translators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perevod36.Features.Languages;

public class Language
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public LanguageId Id { get; init; }

    [MaxLength(Constants.MaxLengthLanguageName)]
    public required string LanguageName { get; set; }


    [MaxLength(Constants.MaxLengthLanguageCode)]
    public required string LanguageCode { get; set; }

    public ICollection<Translator> Translators { get; set; } = new List<Translator>();
}
