using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
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
    }
}
