using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class Request
    {
        public int Id { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

    }
}
