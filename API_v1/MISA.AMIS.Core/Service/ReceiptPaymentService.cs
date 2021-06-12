using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Service
{
    public class ReceiptPaymentService:BaseService<ReceiptPayment>, IReceiptPaymentService
    {
        IReceiptPaymentRepository _receiptPaymentRepository;
        public ReceiptPaymentService(IReceiptPaymentRepository receiptPaymentRepository):base(receiptPaymentRepository)
        {
            _receiptPaymentRepository = receiptPaymentRepository;
        }

        /// <summary>
        /// Lấy mã code lớn nhất trong DB
        /// </summary>
        /// <returns></returns>
        /// CreatedBY: NXCHIEN 09/06/2021
        public ServiceResult GetReceiptPaymentCodeMax()
        {
            string codeMax = _receiptPaymentRepository.GetReceiptPaymentCodeMax();
            string keyChar = string.Empty;
            string numberChar = string.Empty;

            if (codeMax == null)
            {
                return new ServiceResult()
                {
                    Status = StatusCode.Success,
                    Code = MISACode.Success,
                    Data = Properties.Resources.Default_Value_EmployeeCode,
                };
            }
            // Tách chữ và số ra hai mảng
            for (int i = 0; i < codeMax.Length; i++)
            {
                if (Char.IsDigit(codeMax[i]))
                {
                    numberChar += codeMax[i];
                }
                else
                {
                    keyChar += codeMax[i];
                }
            }
            // Tăng code lên 1 đơn vị
            // NV-00001         valueCode = 2   l = 5-1=4   result = NV-00002
            // NV-09             valueCode = 10   l =2-2=0   result = NV-10
            int valueCode = int.Parse(numberChar) + 1;

            int l = numberChar.Length - valueCode.ToString().Length;
            var result = keyChar;
            if (l > 0)
            {
                for (int i = 0; i < l; i++)
                {
                    result += "0";
                }
            }
            result += valueCode.ToString();
            return new ServiceResult()
            {
                Status = StatusCode.Success,
                Code = MISACode.Success,
                Data = result,
            }; ;
        }

        /// <summary>
        /// Nhân bản đối tượng 
        /// </summary>
        /// <param name="id">Mã định danh đối tượng cần nhân bản</param>
        /// <returns>Đối tượng chứa mã Code lớn nhất trong DB</returns>
        /// CreatedBY: NXCHIEN 09/06/2021
        public ServiceResult GetDuplicateReceiptPayment(Guid id)
        {
            var receiptPayment = _receiptPaymentRepository.GetById(id);
            var refCode = GetReceiptPaymentCodeMax().Data.ToString();
            receiptPayment.ReceiptPaymentCode = refCode;

            _serviceResult.Data = receiptPayment;
            _serviceResult.Status = StatusCode.Success;
            _serviceResult.Code = MISACode.Success;

            return _serviceResult;
        }

        /// <summary>
        /// Validate dữ liệu riêng từng đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <param name="http">Phương thức POST hay PUT</param>
        /// Created By: NXCHIEN 17/05/2021
        protected override ServiceResult CustomValidate(ReceiptPayment entity, HTTPType http)
        {
            // Check trùng mã code
            // Khởi tạo giá trị
            var receiptPaymentCode = entity.ReceiptPaymentCode;
            var receiptPaymentId = entity.ReceiptPaymentId;
            var checkCodeExist = _receiptPaymentRepository.CheckReceiptPaymentAttributeExist(Properties.AttributeResource.ReceiptPaymentCode, receiptPaymentId, http, receiptPaymentCode);
            // Kiểm tra trùng hay không
            if (checkCodeExist)
            {
                return new ServiceResult()
                {
                    Status = StatusCode.Error,
                    Code = MISACode.BadRequest,
                    Data = Properties.AttributeResource.Msg_ReceiptPaymentCode + $" <{receiptPaymentCode}>" + Properties.Resources.Msg_Code_Exist,
                };
            }
            return null;
        }

        /// <summary>
        /// Export file excel xuất khẩu toàn bộ nhân viên
        /// </summary>
        /// <returns>Stream</returns>
        /// CreatedBy: NXCHIEN 17/05/2021
        public Stream ExportExcel()
        {
            // Lấy ra danh sách tất cả nhân viên
            var res = _receiptPaymentRepository.GetMISAEntities(300, 1, "");
            var list = res.Data.ToList();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(stream);
            var workSheet = package.Workbook.Worksheets.Add(Properties.ExcelResource.ExcelName);

            // set độ rộng col
            workSheet.Column(1).Width = 5;
            workSheet.Column(2).Width = 15;
            workSheet.Column(3).Width = 15;
            workSheet.Column(4).Width = 40;
            workSheet.Column(5).Width = 15;
            workSheet.Column(6).Width = 30;
            workSheet.Column(7).Width = 40;
            workSheet.Column(8).Width = 15;
            workSheet.Column(9).Width = 20;

            // Format hiển thị dòng A1
            using (var range = workSheet.Cells["A1:I1"])
            {
                range.Merge = true;
                range.Value = Properties.ExcelResource.ExcelName;
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 16;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            }

            // style cho excel.
            workSheet.Cells[3, 1].Value = Properties.ExcelReceiptPaymentResource.ExcelColumnSTT;
            workSheet.Cells[3, 2].Value = Properties.ExcelReceiptPaymentResource.ExcelColumnNHT;
            workSheet.Cells[3, 3].Value = Properties.ExcelReceiptPaymentResource.ExcelColumnSCT;
            workSheet.Cells[3, 4].Value = Properties.ExcelReceiptPaymentResource.ExcelColumnDG;
            workSheet.Cells[3, 5].Value = Properties.ExcelReceiptPaymentResource.ExcelColumnST;
            workSheet.Cells[3, 6].Value = Properties.ExcelReceiptPaymentResource.ExcelColumnDT;
            workSheet.Cells[3, 7].Value = Properties.ExcelReceiptPaymentResource.ExcelColumnDC;
            workSheet.Cells[3, 8].Value = Properties.ExcelReceiptPaymentResource.ExcelColumnLD;
            workSheet.Cells[3, 9].Value = Properties.ExcelReceiptPaymentResource.ExcelColumTNV;

            //Format hiển thị row title
            using (var range = workSheet.Cells["A3:I3"])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["B3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["E3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }

            int i = 0;
            // đổ dữ liệu từ list vào.
            foreach (var item in list)
            {
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 2].Value = item.AccountingDate.ToString(Properties.ExcelResource.ExcelFormatDate);
                workSheet.Cells[i + 4, 3].Value = item.ReceiptPaymentCode;
                workSheet.Cells[i + 4, 4].Value = item.Description;
                workSheet.Cells[i + 4, 5].Value = item.TotalAmount;
                workSheet.Cells[i + 4, 6].Value = item.OrganizationUnitName;
                workSheet.Cells[i + 4, 7].Value = item.OrganizationUnitAddress;
                workSheet.Cells[i + 4, 8].Value = item.ReasonName;
                workSheet.Cells[i + 4, 9].Value = item.FullName;

                using (var range = workSheet.Cells[i + 4, 1, i + 4, 9])
                {
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Cells[i + 4 , 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[i + 4, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[i + 4, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }
                i++;
            }

            using (var range = workSheet.Cells[list.Count() + 4, 1, list.Count() + 4, 9])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                workSheet.Cells[list.Count() + 4, 2].Value = Properties.ExcelReceiptPaymentResource.ExcelColumSUM;
                workSheet.Cells[list.Count() + 4, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[list.Count() + 4, 5].Value = res.TotalMoney;
                workSheet.Cells[list.Count() + 4, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            
            package.Save();
            stream.Position = 0;
            return package.Stream;
        }
    }
}
