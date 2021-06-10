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
        /// <summary>
        /// Lấy mã code lớn nhất trong DB
        /// </summary>
        /// <returns></returns>
        /// CreatedBY: NXCHIEN 09/06/2021
        public string GetReceiptPaymentCodeMax();

        /// <summary>
        /// Kiểm tra trùng thuộc tính đối tượng
        /// </summary>
        /// <param name="attribute">thuộc tính cần check</param>
        /// <param name="receiptPaymentId">Mã định danh đối tượng</param>
        /// <param name="http">Phương thức gửi lên server</param>
        /// <param name="attributeValue">Giá trị</param>
        /// <returns></returns>
        /// CreatedBY: NXCHIEN 09/06/2021
        public bool CheckReceiptPaymentAttributeExist(string attribute, Guid? receiptPaymentId, HTTPType http, string attributeValue);


    }
}
