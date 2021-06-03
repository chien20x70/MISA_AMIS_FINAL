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
        IReceiptPaymentService _receiptPaymentService;
        public ReceiptPaymentController(IReceiptPaymentRepository receiptPaymentRepository, IReceiptPaymentService receiptPaymentService)
            : base(receiptPaymentRepository, receiptPaymentService)
        {
            _receiptPaymentService = receiptPaymentService;
        }

        [HttpGet("ReceiptPaymentCode")]
        public IActionResult GetCode()
        {
            try
            {
                _serviceResult = _receiptPaymentService.GetReceiptPaymentCodeMax();
            }
            catch (Exception ex)
            {
                OnServiceResultException(ex);
            }
            return Ok(_serviceResult);
        }

        [HttpGet("ReceiptPaymentCopy")]
        public IActionResult GetEmployeeCopy(Guid id)
        {
            try
            {
                _serviceResult = _receiptPaymentService.GetDuplicateReceiptPayment(id);
            }
            catch (Exception ex)
            {
                OnServiceResultException(ex);
            }
            return Ok(_serviceResult);
        }
    }
}
