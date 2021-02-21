using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class PaymentState
    {
        public int Id { get; set; }
        public string Status { get; set; }
        [Required]
        public int RequestId { get; set; }

        [ForeignKey("RequestId")]
        public Request Request { get; set; }
    }
}
