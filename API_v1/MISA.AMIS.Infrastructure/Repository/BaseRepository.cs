using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MySqlConnector;
using MISA.AMIS.Core.Entities;

namespace MISA.AMIS.Infrastructure.Repository
{
    #region BaseRepository
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity> where MISAEntity : class
    {
        #region PROPERTIES
        protected string tableName = typeof(MISAEntity).Name;
        protected IConfiguration _configuration;
        protected string connectionDb;
        protected IDbConnection dbConnection;
        #endregion


        #region CONSTRUCTOR
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionDb = _configuration.GetConnectionString("connectionDB");
        }
        #endregion


        #region METHODS
        /// <summary>
        /// Xóa 1 đối tượng.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public int Delete(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Delete{tableName}";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var rowEffects = dbConnection.Execute(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả các đối tượng.
        /// </summary>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public IEnumerable<MISAEntity> GetAll()
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Get{tableName}s";
                var entities = dbConnection.Query<MISAEntity>(sql, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }

        /// <summary>
        /// Lấy 1 đối tượng theo ID.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>1 đối tượng có mã ID là entityId</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public MISAEntity GetById(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Get{tableName}ById";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return entity;
            }
        }

        /// <summary>
        /// Thêm mới 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public int Insert(MISAEntity entity)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Insert{tableName}";
                var rowEffects = dbConnection.Execute(sql, entity, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        /// <summary>
        /// Sửa 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public int Update(MISAEntity entity)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Update{tableName}";
                var rowEffects = dbConnection.Execute(sql, entity, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NXCHIEN (09/05/2021)
        public Paging<MISAEntity> GetMISAEntities(int pageSize, int pageIndex, string filter)
        {
            var res = new Paging<MISAEntity>()
            {
                Page = pageIndex,
                PageSize = pageSize
            };
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@filter", filter);
                // Tính tổng nhân viên.
                int? totalRecord = dbConnection.QueryFirstOrDefault<int>($"Proc_GetTotal{tableName}s", parameters, commandType: CommandType.StoredProcedure);
                if (totalRecord == null)
                {
                    return res;
                }
                res.TotalRecord = totalRecord;
                // Lấy danh sách nhân viên.
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageIndex", pageIndex);
                dynamicParameters.Add("@pageSize", pageSize);
                dynamicParameters.Add("@filter", filter);
                if (typeof(MISAEntity).Name.Equals("ReceiptPayment"))
                {
                    int? totalMoney = dbConnection.QueryFirstOrDefault<int>($"Proc_GetTotalMoney", dynamicParameters, commandType: CommandType.StoredProcedure);
                    if (totalMoney == null)
                    {
                        return res;
                    }
                    res.TotalMoney = totalMoney;
                }                
                var entities = dbConnection.Query<MISAEntity>($"Proc_Get{tableName}Filter", dynamicParameters, commandType: CommandType.StoredProcedure);
                res.Data = entities;
                return res;
            }
        }
        #endregion
    } 
    #endregion
}
