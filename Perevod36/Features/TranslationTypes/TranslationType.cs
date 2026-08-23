using Microsoft.EntityFrameworkCore;
using Perevod36.Features.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perevod36.Features.TranslationTypes;

public class TranslationType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TranslationTypeId Id { get; init; } 

    [MaxLength(Constants.MaxLengthTranslationTypeName)]
    public required string TranslationTypeName { get; set; }
       
    [Precision(10, 2)]
    public decimal PricePerPage { get; set; }
}
