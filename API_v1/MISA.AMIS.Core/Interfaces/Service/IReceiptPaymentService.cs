using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Service
{
    public interface IReceiptPaymentService: IBaseService<ReceiptPayment>
    {
        /// <summary>
        /// Lấy mã code lớn nhất trong DB
        /// </summary>
        /// <returns></returns>
        /// CreatedBY: NXCHIEN 09/06/2021
        public ServiceResult GetReceiptPaymentCodeMax();

        /// <summary>
        /// Nhân bản đối tượng 
        /// </summary>
        /// <param name="id">Mã định danh đối tượng cần nhân bản</param>
        /// <returns>Đối tượng chứa mã Code lớn nhất trong DB</returns>
        /// CreatedBY: NXCHIEN 09/06/2021
        public ServiceResult GetDuplicateReceiptPayment(Guid id);

        /// <summary>
        /// Export file excel xuất khẩu toàn bộ phiếu chi
        /// </summary>
        /// <returns>Stream</returns>
        /// CreatedBy: NXCHIEN 17/05/2021
        public Stream ExportExcel();
    }
}
