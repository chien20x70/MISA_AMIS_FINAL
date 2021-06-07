using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IReceiptPaymentRepository: IBaseRepository<ReceiptPayment>
    {
        public string GetReceiptPaymentCodeMax();

        public bool CheckReceiptPaymentAttributeExist(string attribute, Guid? receiptPaymentId, HTTPType http, string attributeValue);
    }
}
