using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class ReceiptPaymentRepository:BaseRepository<ReceiptPayment>, IReceiptPaymentRepository
    {
        public ReceiptPaymentRepository(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Lấy mã code lớn nhất trong DB
        /// </summary>
        /// <returns></returns>
        /// CreatedBY: NXCHIEN 09/06/2021
        public string GetReceiptPaymentCodeMax()
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sqlCommand = $"Proc_GetReceiptPaymentCodeMax";
                var employeeCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
                return employeeCode;
            }
        }

        /// <summary>
        /// Kiểm tra trùng thuộc tính đối tượng
        /// </summary>
        /// <param name="attribute">thuộc tính cần check</param>
        /// <param name="receiptPaymentId">Mã định danh đối tượng</param>
        /// <param name="http">Phương thức gửi lên server</param>
        /// <param name="attributeValue">Giá trị</param>
        /// <returns></returns>
        /// CreatedBY: NXCHIEN 09/06/2021
        public bool CheckReceiptPaymentAttributeExist(string attribute, Guid? receiptPaymentId, HTTPType http, string attributeValue)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                DynamicParameters parameters = new DynamicParameters();
                if (http == HTTPType.ADD)
                {
                    parameters.Add($"@{attribute}", attributeValue);
                    parameters.Add("@receiptPaymentId", null);
                }
                else if (http == HTTPType.UPDATE)
                {
                    parameters.Add($"@{attribute}", attributeValue);
                    parameters.Add("@receiptPaymentId", receiptPaymentId);
                }
                var sqlCommand = $"Proc_Check{attribute}Exist";
                var check = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, parameters, commandType: CommandType.StoredProcedure);
                return check;
            }
        }
    }
}
