using Microsoft.EntityFrameworkCore;
using Perevod36.Features.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Perevod36.Features.Customers;
using Perevod36.Features.Managers;
using Perevod36.Features.Translators;
using Perevod36.Features.TranslationTypes;

namespace Perevod36.Features.Orders
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public OrderId Id { get; init; }

        //OrderNumber не обязан быть уникальным
        public string OrderNumber { get; private set; } = null!;

        [Column(TypeName = "date")]
        public DateOnly Created { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public int NumberOfPages { get; set; } = 1;

        [Precision(10, 2)]
        public decimal Cost { get; private set; }

        public Guid CustomerId { get; set; }
        public Guid TranslatorId { get; set; }
        public Guid ManagerId { get; set; }
        public TranslationTypeId TranslationTypeId { get; set; }

    
        public Customer Customer { get; set; } = null!;
        public Translator Translator { get; set; } = null!;
        public Manager Manager { get; set; } = null!;
        public TranslationType TranslationType { get; set; } = null!;

        public void CalculateCost()
        {
            Cost = NumberOfPages * TranslationType.PricePerPage;
        }

        public void CreateOrderNumber()
        {
            const byte code = 4;
            int length = Customer.PhoneNumber!.Length;//null невозможен
            Random rnd = new Random();
            OrderNumber = Customer.PhoneNumber.Substring(length - code, code) + "_" + rnd.Next(1000, 10000).ToString();
        }
    }
}
