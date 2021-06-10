﻿using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IBaseRepository<MISAEntity> where MISAEntity : class
    {
        /// <summary>
        /// Lấy danh sách tất cả các đối tượng.
        /// </summary>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// Lấy 1 đối tượng theo ID.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>1 đối tượng có mã ID là entityId</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public MISAEntity GetById(Guid entityId);

        /// <summary>
        /// Thêm mới 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public int Insert(MISAEntity entity);

        /// <summary>
        /// Sửa 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public int Update(MISAEntity entity);

        /// <summary>
        /// Xóa 1 đối tượng.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public int Delete(Guid entityId);

        /// <summary>
        /// Lấy danh sách đối tượng có lọc
        /// </summary>
        /// <param name="pageSize">số lượng đối tượng / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: NXCHIEN (17/05/2021)
        public Paging<MISAEntity> GetMISAEntities(int pageSize, int pageIndex, string filter);

        /// <summary>
        /// Lấy danh sách đối tượng có lọc
        /// </summary>
        /// <param name="pageSize">số lượng đối tượng / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: NXCHIEN (09/06/2021)
        public Paging<MISAEntity> GetMISAEntitiesByDateNotNull(int pageSize, int pageIndex, string filter, DateTime startDate, DateTime endDate);
    } 
}
