using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class ReceiptPaymentDetail
    {
        public string DescriptionDetail { get; set; }
        public string DebtAccount { get; set; }
        public string CreditAccount { get; set; }
        public decimal Amount { get; set; }
        public string OrganizationUnitCode { get; set; }
        public string OrganizationUnitName { get; set; }
    }
}
