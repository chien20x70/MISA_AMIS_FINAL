﻿using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MISA.AMIS.Core.Enums;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class EmployeeRepository: BaseRepository<Employee>, IEmployeeRepository
    {
        #region CONSTRUCTOR
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region METHODS
        /// <summary>
        /// Kiểm tra trùng attribute của đối tượng nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã code nhân viên</param>
        /// <param name="employeeId">Mã ID nhân viên</param>
        /// <param name="http">Phương thứ PUT hay POST</param>
        /// <param name="attributeValue">Giá trị của attribute</param>
        /// <returns>TRUE hoặc FALSE</returns>
        /// Created By: NXCHIEN 07/05/2021
        public bool CheckEmployeeAttributeExist(string attribute, Guid? employeeId, HTTPType http, string attributeValue)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                DynamicParameters parameters = new DynamicParameters();
                if (http == HTTPType.ADD)
                {
                    parameters.Add($"@{attribute}", attributeValue);
                    parameters.Add("@employeeId", null);
                }
                else if (http == HTTPType.UPDATE)
                {
                    parameters.Add($"@{attribute}", attributeValue);
                    parameters.Add("@employeeId", employeeId);
                }
                var sqlCommand = $"Proc_Check{attribute}Exist";
                var check = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, parameters, commandType: CommandType.StoredProcedure);
                return check;
            }
        }

        /// <summary>
        /// Lấy ra EmployeeCode lớn nhất trong DB
        /// </summary>
        /// <returns>EmployeeCode</returns>
        /// Created By: NXCHIEN 09/05/2021
        public string GetEmployeeCodeMax()
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sqlCommand = $"Proc_GetEmployeeCodeMax";
                var employeeCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
                return employeeCode;
            }
        }
        #endregion
    }
}
