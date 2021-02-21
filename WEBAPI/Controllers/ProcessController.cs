using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WEBAPI.Models;
using WEBAPI.Models.Dtos;
using WEBAPI.Repository.IRepository;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IMapper _mapper;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IPremiumPaymentService _premiumPaymentService;

        public ProcessController(IExpensivePaymentGateway expensivePaymentGateway, 
            IMapper mapper, ICheapPaymentGateway cheapPaymentGateway, IPremiumPaymentService premiumPaymentService)
        {
            _expensivePaymentGateway = expensivePaymentGateway;
            _mapper = mapper;
            _cheapPaymentGateway = cheapPaymentGateway;
            _premiumPaymentService = premiumPaymentService;
        }

        [HttpPost]
        public async Task<dynamic> ProcessPayment(RequestDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ValidateCreditCard(request.CreditCardNumber))
            {
                ModelState.AddModelError("", "Credit Card Number is invalid!");
                return StatusCode(404, ModelState);
            }

            if (!ValidateExpirationDate(request.ExpirationDate))
            {
                ModelState.AddModelError("", "Card expired!");
                return StatusCode(404, ModelState);
            }

            if (request.SecurityCode.Length > 0 && !ValidateSecurityCode(request.SecurityCode))
            {
                ModelState.AddModelError("", "Security code is invalid!");
                return StatusCode(404, ModelState);
            }

            if (!ValidateAmount(request.Amount))
            {
                ModelState.AddModelError("", "Amount can't be negative!");
                return StatusCode(404, ModelState);
            }

            var requestObj = _mapper.Map<Request>(request);

            var addRequest = _expensivePaymentGateway.AddRequest(requestObj);

            dynamic process;

            if (request.Amount <= 20)
            {
                process = await _cheapPaymentGateway.ProcessPayment(requestObj);
            }
            else if (request.Amount > 20 && request.Amount <= 500)
            {
                process = await _expensivePaymentGateway.ProcessPayment(requestObj);
            }
            else
            {
                process = await _premiumPaymentService.ProcessPayment(requestObj);
            }
            

            var state = new PaymentStateDto
            {
                RequestId = addRequest.Id,
                Status = process
            };

            var stateObj = _mapper.Map<PaymentState>(state);

            _expensivePaymentGateway.AddPaymentState(stateObj);

            return process;
        }

        public bool ValidateCreditCard(string creditCardNumber)
        {
            if (creditCardNumber.Contains("-"))
            {
                creditCardNumber.Replace("-", "");
            }

            if (creditCardNumber.Length == 13 || creditCardNumber.Length == 15 ||
                creditCardNumber.Length == 16 || creditCardNumber.Length == 19 || creditCardNumber.Length == 14)
            {
                return true;
            }
            return false;
        }

        public bool ValidateExpirationDate(DateTime expirationDate)
        {

            return expirationDate > DateTime.Now;
        }

        public bool ValidateSecurityCode(string securityCode)
        {
            return securityCode.Length == 3;
        }

        public bool ValidateAmount(decimal amount)
        {

            return amount > 0;
        }
    }
}
