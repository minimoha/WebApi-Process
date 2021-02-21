using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;
using WEBAPI.Models.Dtos;

namespace WEBAPI.Repository.IRepository
{
    public interface IExpensivePaymentGateway
    {
        Task<dynamic> ProcessPayment(Request request);
        Request AddRequest(Request request);
        bool AddPaymentState(PaymentState paymentState);
        bool Save();
    }
}
