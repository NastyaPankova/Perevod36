using Microsoft.EntityFrameworkCore;
using Perevod36.Features.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perevod36.Features.Orders
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public OrderId Id { get; init; }

        [Column(TypeName = "date")]
        public DateOnly Created { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public int NumberOfPages { get; set; } = 1;

        [Precision(10, 2)]
        public decimal Cost { get; set; }

        public ClientId ClientId { get; set; }
        public TranslatorId TranslatorId { get; set; }
        public ManagerId ManagerId { get; set; }
        public TranslationTypeId TranslationTypeId { get; set; }

    
        public Client Client { get; set; } = null!;
        public Translator Translator { get; set; } = null!;
        public Manager Manager { get; set; } = null!;
        public TranslationType TranslationType { get; set; } = null!;
    }
}
