﻿using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
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

        public ServiceResult GetDuplicateReceiptPayment(Guid id)
        {
            var receiptPayment = _receiptPaymentRepository.GetById(id);
            var refCode = GetReceiptPaymentCodeMax().Data.ToString();
            receiptPayment.RefCode = refCode;

            _serviceResult.Data = receiptPayment;
            _serviceResult.Status = StatusCode.Success;
            _serviceResult.Code = MISACode.Success;

            return _serviceResult;
        }

        //public ServiceResult GetAllReceiptPayment()
        //{
        //    var receiptPayments = _receiptPaymentRepository.GetAll();
        //    decimal totalMoney = 0;
        //    //for (int i = 0; i < receiptPayments.Count(); i++)
        //    //{
        //    //    totalMoney += receiptPayments.t
        //    //}
        //    foreach (var item in receiptPayments)
        //    {
        //        totalMoney += item.TotalAmount;
        //    }

        //    OnStateServiceResult(receiptPayments, StatusCode.Success, MISACode.Success);

        //    return _serviceResult;
        //}
    }
}
