﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class ReceiptPayment
    {
        /// <summary>
        /// Mã ID thu chi tiền mặt
        /// </summary>
        public Guid ReceiptPaymentID { get; set; }

        /// <summary>
        /// Mã ref số chứng từ
        /// </summary>
        public string RefCode { get; set; }

        /// <summary>
        /// Ngày hạch toán
        /// </summary>
        public DateTime AccountingDate { get; set; }

        /// <summary>
        /// Diễn giải (lý do thu chi trong CashDialog)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Tổng tiền của form chi tiết
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Tên đối tượng
        /// </summary>
        public string OrganizationUnitName { get; set; }

        /// <summary>
        /// Lý do thu/chi mặc định là Chi khác
        /// </summary>
        public string ReasonName { get; set; }

        /// <summary>
        /// Địa chỉ đối tượng
        /// </summary>
        public string OrganizationUnitAddress { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string Employee { get; set; }

        /// <summary>
        /// Người nhận
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// Ngày phiếu chi
        /// </summary>
        public DateTime RefDate { get; set; }

        /// <summary>
        /// Kèm theo
        /// </summary>
        public int RefAttach { get; set; }

        /// <summary>
        /// Chi tiết phần chi tiền (bảng detail trong CashDialog)
        /// </summary>
        public string ReceiptPaymentDetail { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        public IEnumerable<ReceiptPaymentDetail> ListDetail { get; set; }
    }
}
