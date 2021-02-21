using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models.Dtos
{
    public class PaymentStateDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        [Required]
        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}
