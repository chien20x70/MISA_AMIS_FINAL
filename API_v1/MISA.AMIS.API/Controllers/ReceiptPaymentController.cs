using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class ReceiptPaymentController : BaseController<ReceiptPayment>
    {
        public ReceiptPaymentController(IReceiptPaymentRepository receiptPaymentRepository, IReceiptPaymentService receiptPaymentService)
            : base(receiptPaymentRepository, receiptPaymentService)
        {

        }
    }
}
