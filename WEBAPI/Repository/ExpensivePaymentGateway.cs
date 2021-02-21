using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Data;
using WEBAPI.Models;
using WEBAPI.Models.Dtos;
using WEBAPI.Repository.IRepository;

namespace WEBAPI.Repository
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        private readonly IOptions<StripeSettings> _options;
        private readonly ApplicationDbContext _context;

        public ExpensivePaymentGateway(IOptions<StripeSettings> options, ApplicationDbContext context)
        {
            _options = options;
            _context = context;
        }
        

        public async Task<dynamic> ProcessPayment(Request request)
        {

            try
            {
                StripeConfiguration.ApiKey = _options.Value.SecretKey;

                var optionsToken = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = request.CreditCardNumber,
                        ExpMonth = request.ExpirationDate.Month,
                        ExpYear = request.ExpirationDate.Year,
                        Cvc = request.SecurityCode,
                    },

                };

                var serviceToken = new TokenService();
                var stripeToken = await serviceToken.CreateAsync(optionsToken);

                var options = new ChargeCreateOptions
                {
                    Amount = (long?)(request.Amount * 100),
                    Currency = "eur",
                    Description = "Test Payment",
                    Source = stripeToken.Id
                };

                var service = new ChargeService();
                var charge = await service.CreateAsync(options);

                if (charge.Paid)
                {
                    return "Success";
                }
                else
                {
                    return "Failed";
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public Request AddRequest(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();

            var updatedRequest = request.Id;
            return _context.Requests.FirstOrDefault(x => x.Id == updatedRequest);
        }

        public bool AddPaymentState(PaymentState paymentState)
        {
            _context.PaymentStates.Add(paymentState);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
