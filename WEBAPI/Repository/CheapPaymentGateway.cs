﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;
using WEBAPI.Repository.IRepository;

namespace WEBAPI.Repository
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        public Task<dynamic> ProcessPayment(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
