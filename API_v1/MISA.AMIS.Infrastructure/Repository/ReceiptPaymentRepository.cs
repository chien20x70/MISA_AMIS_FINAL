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

        public string GetReceiptPaymentCodeMax()
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sqlCommand = $"Proc_GetReceiptPaymentCodeMax";
                var employeeCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
                return employeeCode;
            }
        }
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
