using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Repository.IRepository
{
    public interface ICheapPaymentGateway
    {
        Task<dynamic> ProcessPayment(Request request);
    }
}
