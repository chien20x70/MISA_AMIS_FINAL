<template>
  <div class="content">
    <div class="box">
      <div class="content-item">
        <div class="content-item-text">Nhân viên</div>
        <div class="component-btn">
          <button class="btn-add tooltip tooltip--position30-30" @click="onBtnAddClick" v-hotkey="keymap">
            Thêm mới nhân viên
              <span class="tooltip__text">Ctrl + 1</span>
          </button>
        </div>
      </div>
    </div>
    <div class="content-table">
      <div class="item">
        <div class="item-right">
          <input
            ref="focusInputSearch"
            type="text"
            placeholder="Tìm theo mã, tên nhân viên"
            class="input-search"
            :value="filter"
            @input="onChangeInputEmployeeFilter"
          />
          <div class="icon icon-24 refresh" @click="onBtnRefreshClick"></div>
          <div class="icon icon-24 excel__nav" @click="onBtnExportClick"></div>
        </div>
      </div>
      <div class="content-table-height">
        <table class="tblListEmployee" border="0" width="100%">
          <thead>
            <tr>
              <th class="first__th"><input type="checkbox" class="check-box" /></th>
              <th style="min-width: 150px; border-left: none;">MÃ NHÂN VIÊN</th>
              <th style="min-width: 250px"> <div class="resize">TÊN NHÂN VIÊN</div></th>
              <th style="min-width: 120px">GIỚI TÍNH</th>
              <th style="min-width: 150px">NGÀY SINH</th>
              <th style="min-width: 200px">SỐ CMND</th>
              <th style="min-width: 250px"><div class="resize">CHỨC DANH</div></th>
              <th style="min-width: 250px">TÊN ĐƠN VỊ</th>
              <th style="min-width: 150px">SỐ TÀI KHOẢN</th>
              <th style="min-width: 250px">TÊN NGÂN HÀNG</th>
              <th style="min-width: 250px">CHI NHÁNH TK NGÂN HÀNG</th>
              <th style="min-width: 120px; z-index: 101;">CHỨC NĂNG</th>
            </tr>
          </thead>
          <tbody v-if="employees != undefined">
            <tr
              v-for="(employee, index) in employees"
              :key="index"
              @dblclick="onRowTableDblClick(employee.employeeId)"
            >
              <td class="first__th" style="z-index: 2;"><input type="checkbox" class="check-box" /></td>
              <td style="border-left: none;">{{ employee.employeeCode }}</td>
              <td>{{ employee.fullName }}</td>
              <td>{{ employee.genderName }}</td>
              <td>{{ employee.dateOfBirth | dateFormatDDMMYY }}</td>
              <td>{{ employee.identifyNumber }}</td>
              <td>{{ employee.positionName }}</td>
              <td>{{ employee.departmentName }}</td>
              <td>{{ employee.bankAccountNumber }}</td>
              <td>{{ employee.bankName }}</td>
              <td>{{ employee.bankBranchName }}</td>
              <td class="editclass" :style="{ 'z-index': 100 - index }">
                <div class="btn-edit">
                  <button
                    class="btn-btn hover"
                    @click="onBtnEditClick(employee.employeeId, employee.employeeCode)"
                  >
                    <div class="flex btn-btn-text">
                      <span
                        class="pr-4"
                        style="color: #0075c0; font-weight: 600"
                        >Sửa</span
                      >
                    </div>
                  </button>
                  <DropdownDuplicateAndDelete
                    @showPopup="
                      showPopup(employee.employeeId, employee.employeeCode)
                    "
                    @showDialog="duplicateClick(employee.employeeId)"
                  />
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="message" v-if="employees == undefined">
          <div class="img-report">
            <img
              src="https://actappg2.misacdn.net/img/bg_report_nodata.76e50bd8.svg"
              alt=""
              class="nodata-img"
            />
          </div>
          <div class="img-report">Không có dữ liệu</div>
        </div>
      <div class="content-navpage">
        <div class="content-navpage-text-left">
          Tổng số: <span style="font-weight: 700">{{ totalRecord }}</span> bản
          ghi
        </div>
        <div class="footer-complete">
          <ComboboxFilter
            :selectState="valueSelect"
            @passValueToSelect="getValueFromComboboxFilter"
          />
          <div class="autocomplete">
            <div class="selected-option">
              <input type="text" class="input-select" :value="msgSelected" />
              <div class="icon-selected">
                <div
                  class="icon icon-16 arrow-dropdown"
                  @click="showSelected"
                  :class="{ tranform: valueSelect == false }"
                ></div>
              </div>
            </div>
          </div>
          <button
            class="style margin"
            :class="{ disable: pageIndex == 1 }"
            @click="onPageChange(pageIndex - 1)"
          >
            Trước
          </button>
          <button
            class="btn-filter margin"
            :class="{ active: pageIndex == 1 }"
            @click="onPageChange(1)"
          >
            1
          </button>
          <button v-if="pageIndex > 3" class="btn-filter style margin disable">
            ...
          </button>
          <button
            v-for="p in pageIndexs"
            :key="p"
            class="btn-filter margin"
            :class="{ active: pageIndex == p }"
            @click="onPageChange(p)"
          >
            {{ p }}
          </button>
          <button
            v-if="pageIndex < totalPages - 3"
            class="btn-filter style margin disable"
          >
            ...
          </button>
          <button
            class="btn-filter margin"
            :class="{
              display: totalPages == 1,
              active: pageIndex == totalPages,
            }"
            @click="onPageChange(totalPages)"
          >
            {{ totalPages }}
          </button>
          <button
            class="style margin"
            :class="{ disable: pageIndex == totalPages }"
            @click="onPageChange(pageIndex + 1)"
          >
            Sau
          </button>
        </div>
      </div>
    </div>
    <EmployeeDialog
      v-if="show"
      @hideDialog="hideDialog"
      @hideDialogNotLoad="hideDialogNotLoad"
      :employee="selectedEmployee"
      :flag="status"
      @saveAndAdd="onBtnAddClick"     
    />
    <Popup     
      v-if="valuePopup"
      @hidePopupNotLoad="hidePopupNotLoad"
      @hidePopup="hidePopup"
      :employeeCode="recordCode"
      :employeeId="recordId"
    />
    <div class="fa-3x" v-if="isBusy">
      <i class="fas fa-spinner fa-spin" style="color: green"></i>
    </div>
  </div>
</template>
<script>
//#region Import
import EmployeeDialog from "./EmployeeDialog.vue";
import DropdownDuplicateAndDelete from "../common/DropdownDuplicateAndDelete.vue";
import Popup from "../common/Popup.vue";
import ComboboxFilter from "../common/ComboboxFilter.vue";
//#endregion
export default {
  //#region Khai báo
  components: {
    EmployeeDialog,
    DropdownDuplicateAndDelete,
    Popup,
    ComboboxFilter,
  },
  data() {
    return {
      employees: [], // Mảng nhân viên
      show: false, // Giá trị hiển thị dialog
      selectedEmployee: {}, // data 1 nhân viên khi dblClick hoặc click btn Sửa
      recordId: null, // Lưu giá trị của EmployeeId để truyền qua Popup
      status: null, // Trạng thái nút là Thêm mới hay Sửa
      isBusy: false, // Trạng thái của icon Loading
      valuePopup: false, // Giá trị hiển thị Popup
      recordCode: null, // Lưu giá trị Employeecode truyền qua Popup
      totalRecord: 0, // Tổng số bản ghi Empployee
      pageSize: 20, // Bao nhiêu nhân viên / trang
      filter: "", // Giá trị truyền vào input để lọc
      pageIndex: 1, // Trang hiện tại
      totalPages: 1, // Tổng số trang
      valueSelect: true, // Hiển thị combobox filter phân trang
      msgSelect: " bản ghi trên 1 trang", // message default
      msgSelected: "20 bản ghi trên 1 trang", // message hiển thị khi phân trang.
    };
  },
  //#endregion
  
  created() {
    /**
     * Lọc dữ liệu hiển thì mặc định 20 bản ghi/ trang
     * CreatedBy: NXCHIEN 17/05/2021
     */
    this.filterData();
  },

  //#region METHODS
  methods: {
    /**
     * Click thêm mới 1 nhân viên
     * CreatedBy: NXCHIEN 17/05/2021
     */
    onBtnAddClick() {
      this.filterData();
      this.axios
        .get("/Employees/employeeCode")
        .then((response) => {
          // Hiển thị dialog
          this.show = true;
          // Gán giá trị là nút Thêm mới
          this.status = "add";
          // Gán tất cả các ô data của dialog rỗng
          this.onBtnAddAssignSelectedEmployee(response);
        })
        .catch(() => {});
    },
    /**
     * Gán SelectedEmployee khi click BtnAdd.
     * CreatedBY: NXCHIEN 30/05/2021
     */
    onBtnAddAssignSelectedEmployee(response){
      // Gán tất cả các ô data của dialog rỗng
      this.selectedEmployee = {};
      // Gán code Max cho ô Mã nhân viên và 1 số thuộc tính khác.
      this.selectedEmployee.employeeCode = response.data.data;
      this.selectedEmployee.fullName = "";
      this.selectedEmployee.departmentId = "";
      this.selectedEmployee.gender = 1;
      this.selectedEmployee.email = "";
    },
    /* 
    Đóng dialog mà không load lại dữ liệu (được gọi từ Dialog)
    CreatedBy: NXCHIEN 17/05/2021  
    */
    hideDialogNotLoad() {
      this.show = false;
      this.$refs.focusInputSearch.focus();
    },

    /* 
    Đóng dialog có load lại dữ liệu (được gọi từ Dialog)
    CreatedBy: NXCHIEN 17/05/2021 
    */
    hideDialog() {
      this.show = false;
      this.filterData();
      this.$refs.focusInputSearch.focus();
    },

    /* 
    Hiển thị Popup được gọi từ (dropdownDuplicateAndDelete)
    CreatedBy: NXCHIEN 17/05/2021  
    */
    showPopup(employeeId, employeeCode) {

      this.valuePopup = true;
      // Lưu giá trị Id khi click vào nút sửa trên bảng.
      this.recordId = employeeId;
      this.recordCode = employeeCode;
    },

    /* 
    Đóng popup có load lại dữ liệu
    CreatedBy: NXCHIEN 17/05/2021  
    */
    hidePopup() {
      this.valuePopup = false;
      this.filterData();
      this.$refs.focusInputSearch.focus();
    },

    /* 
    Đóng popup mà không load lại dữ liệu
    CreatedBy: NXCHIEN 17/05/2021  
    */
    hidePopupNotLoad() {
      this.valuePopup = false;
      this.$refs.focusInputSearch.focus();
    },

    /**
     * Hàm dùng chung khi click nhân bản và dblClick 1 dòng trong bảng
     * CreatedBy: NXCHIEN 17/05/2021
     */
    onShowDialogAndAssignSelectedEmployee(response){
      //show Dialog
      this.show = true;
      //Fill employee vào dialog
      this.selectedEmployee = response.data.data;
      // format lại dữ liệu hiển thị
      if (this.selectedEmployee.dateOfBirth !== null) {
        this.selectedEmployee.dateOfBirth = this.dateFormatYYMMDD(
          this.selectedEmployee.dateOfBirth
        );
      }

      if (this.selectedEmployee.dateOfIN !== null) {
        this.selectedEmployee.dateOfIN = this.dateFormatYYMMDD(
          this.selectedEmployee.dateOfIN
        );
      }
    },

    /* 
    dblClick vào 1 dòng trong table
    - Lấy ra 1 nhân viên được chọn
    CreatedBy: NXCHIEN 17/05/2021  
    */
    onRowTableDblClick(eId) {
    
      this.axios
        .get("/Employees/" + eId)
        .then((response) => {

          // gán cờ thành nút sửa
          this.status = "edit";

          this.onShowDialogAndAssignSelectedEmployee(response);
        })
        .catch(() => {});
    },

    duplicateClick(value) {
      this.axios
        .get("/Employees/EmployeeCopy?id=" + value)
        .then((response) => {
          // gán cờ thành nút thêm mới
          this.status = "add";
          
          this.onShowDialogAndAssignSelectedEmployee(response);
        })
        .catch(() => {});

    },
    /* 
    Click nút Sửa trong table
    CreatedBy: NXCHIEN 17/05/2021  
    */
    onBtnEditClick(employeeClickId) {
      this.onRowTableDblClick(employeeClickId);
    },

    /* 
    Load lại dữ liệu khi click vào nút refresh
    CreatedBy: NXCHIEN 17/05/2021  
    */
    onBtnRefreshClick() {
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() =>{
        this.totalPages = 1;
        this.pageIndex = 1;
        this.filterData();
      }, 500);
    },

    /* 
    Lọc data bằng các tham số truyền vào
    CreatedBy: NXCHIEN 17/05/2021  
    */
    filterData() {
      this.isBusy = true;
      this.axios
        .get(
          `/Employees/Filter?pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&filter=${this.filter}`
        )
        .then((response) => {
          // Gán mảng nhân viên ban đầu = data từ server trả về
          this.employees = response.data.data.data;
          // tổng số bản ghi = tổng số bản ghi từ server trả về
          this.totalRecord = response.data.data.totalRecord;
          // tổng số trang.
          this.totalPages = response.data.data.totalPages;
          if (response.data.data.totalRecord == undefined) {
            this.totalRecord = 0;
          }
        })
        .catch(() => {})
        .then(() => {
          this.isBusy = false;
        });
    },

    /* 
    Kiểm tra giá trị input thay đổi thì lọc mảng nhân viên bằng cách gọi tới API
    CreatedBy: NXCHIEN 17/05/2021  
    */
    onChangeInputEmployeeFilter(e) {
      let val = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        // Lấy chuỗi cần lọc rồi gán vào biến filter
        this.filter = val;
        // Gán trang về 1
        this.pageIndex = 1;
        console.log(this.filter);
        // Gọi hàm lọc có delay 1s để không gửi quá nhiều request lên serve
        this.filterData();
      }, 1000);
    },

    /* 
    Kiểm tra click thay đổi phân trang
    - Lọc lại mảng nhân viên khi click.
    CreatedBy: NXCHIEN 17/05/2021  
    */
    onPageChange(page) {
      // Thay đổi trang khi click button phần phân trang
      this.pageIndex = page;
      // Lọc dữ liệu.
      this.filterData();
    },

    /* 
    Export data ra file excel
    - mở 1 cửa số mới call đến API
    CreatedBy: NXCHIEN 17/05/2021 
    */
    onBtnExportClick() {
      // Mở 1 cửa số mới gọi API để tải về.
      window.open(
        `https://localhost:44314/api/v1/Employees/ExportingExcel`,
        "_blank"
      );
    },

    /**
     * Lấy dữ liệu từ comboboxFilter để phân trang
     * CreatedBy: NXCHIEN 17/05/2021
     */
    getValueFromComboboxFilter(value) {
      // gán message thành message cần để hiển thị  "20 bản ghi trên 1 trang..."
      this.msgSelected = "";
      this.msgSelected = value + this.msgSelect;

      // số nhân viên trên/ trang
      this.pageSize = value;
      // trang số 1
      this.pageIndex = 1;
      // Lọc nhân viên
      this.filterData();
      this.valueSelect = true;
    },

    showSelected() {
      this.valueSelect = !this.valueSelect;
    },
    /* 
    Format dữ liệu ngày tháng năm theo định dạng yyyy-mm-dd
    CreatedBy: NXCHIEN 17/05/2021  
    */
    dateFormatYYMMDD(date) {
      var newDate = new Date(date);
      var day = newDate.getDate();
      var month = newDate.getMonth() + 1;
      var year = newDate.getFullYear();
      day = day < 10 ? "0" + day : day;
      month = month < 10 ? "0" + month : month;
      return `${year}-${month}-${day}`;
    },
  },
  //#endregion
  
  computed: {
    /* 
    Mảng chứa các phần tử ở giữa nút số 1 và trang cuối cùng trong phân trang
    CreatedBy: NXCHIEN 17/05/2021  
    */
    pageIndexs: function() {
      let ps = []; // Khởi tạo mảng

      // Nếu trang hiện tại > 3 thì bắt đầu từ nút trang hiện tại trừ 1 còn nếu <=3 thì nút bắt đầu là 2
      let start = this.pageIndex > 3 ? this.pageIndex - 1 : 2;
      // Nút kết thúc < tổng số trang - 3 thì nút kết thúc là giá trị trang hiện tại + 1 còn nếu không thì tổng số trang - 1
      let end =
        this.pageIndex < this.totalPages - 3
          ? this.pageIndex + 1
          : this.totalPages - 1;

      // Đẩy vào mảng ps chứa các nút được phép hiển thị khi phân trang
      for (let i = start; i <= end; i++) ps.push(i);
      return ps;
    },
    keymap() {
      return {       
        "Ctrl + 1": this.onBtnAddClick,
      };
    },
  },
  mounted(){
    this.$refs.focusInputSearch.focus();
  }
};
</script>
<style scoped>
.content .box {
  padding: 20px 0px 16px 0px;
}
.box .content-item {
  align-items: center;
  display: flex;
  justify-content: space-between;
}
.content-item .content-item-text {
  font-size: 24px;
  color: black;
  background-size: contain;
  background-position: center;
  font-weight: bold;
}
.component-btn {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.btn-edit {
  position: relative;
  display: flex;
  align-items: center;
}
.btn-btn {
  padding: 0 16px;
  border-radius: 30px;
  color: #111;
  height: 36px;
  position: relative;
  border: 1px solid #8d9096;
  background-color: transparent;
  transition: all 0.2s ease;
  cursor: pointer;
  box-sizing: border-box;
  background: 0 0;
  overflow: visible;
  outline: none;
}
.btn-btn-text {
  font-weight: 600;
  position: relative;
  color: inherit;
  display: inline-block;
  transition: all 0.25s ease;
  white-space: nowrap;
  font-size: 13px;
  line-height: 13px;
  justify-content: center;
  align-items: center;
}
.pr-4 {
  padding-right: 4px !important;
}

.flex {
  display: flex !important;
}

.border {
  border-radius: 0 30px 30px 0;
  border: none;
}

.content .content-table {
  background-color: white !important;
  width: 100%;
  height: calc(100% - 81px);
}
.content-table .item {
  padding: 16px 16px 10px;
  display: flex;
  align-items: center;
  justify-content: flex-end;
}
.item .item-right {
  display: flex;
  align-items: center;
  width: 310px;
  justify-content: space-between;
}
.content-table .content-table-height {
  height: calc(100% - 52px);
  overflow-y: auto;
  overflow-x: auto;
  width: calc(100% - 41px);
  display: flex;
  margin-left: 16px;
  margin-top: 5px;
}
.first__th {
  width: 40px;
  position: sticky;
  left: 0px;
  top: 0;
  z-index: 3;
  border-left: none;
  border-bottom: none;
  display: flex;
  border-right: 1px solid #c7c7c7;
  align-items: center;
}
.check-box {
  padding: 0;
}
.content-navpage {
  position: absolute;
  bottom: 10px;
  width: calc(100% - 253px);
  /* width: 100%; */
  height: 50px;
  /* border-top: 1px solid #a29d9d; */
  background-color: white;
  align-items: center;
  display: flex;
  justify-content: space-between;
  z-index: 100;
}

.content-navpage .content-navpage-text-left {
  margin-left: 18px;
}
.content-navpage .content-navpage-button {
  display: flex;
}
.content-navpage .content-navpage-text-right {
  margin-right: 10px;
}

.hover {
  border: none;
  padding: 6px 0px 6px 16px;
}

.fa-3x {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}

.footer-complete {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-right: 200px;
  margin-right: 40px;
}
.autocomplete {
  display: flex;
  min-height: 32px;
  border: 1px solid #babec5;
  border-radius: 2px;
  background-color: #fff;
  height: 32px;
  width: 200px;
}
.selected-option {
  display: flex;
  flex-basis: 100%;
  flex-grow: 1;
  flex-wrap: wrap;
  width: calc(100% - 32px);
  align-items: center;
  padding: 6px 0 6px 12px;
  justify-content: space-around;
}
.style {
  border: none;
  background-color: white;
}
.margin {
  margin-left: 10px;
}
.disable {
  cursor: not-allowed;
  pointer-events: none;
}
.active {
  font-weight: 800;
  border: 1px solid #bbb;
}
.message {
  height: 191px;
  width: 100% !important;
  display: block;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
.img-report {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
}
.nodata-img {
  position: sticky;
  margin: 50px 50px 30px;
  width: 132px;
  height: 74px;
  left: 45%;
}
.input-select {
  background-color: transparent;
  display: flex;
  padding: 0;
  height: 19px;
  width: 154px;
  border: none;
}
.icon-selected {
  display: flex;
  align-items: center;
  justify-content: center;
}
.icon-selected:hover {
  background-color: #bbb;
}

.tranform {
  transform: rotate(180deg);
  transition: transform 0.15s linear;
}

.display {
  display: none;
}
table tr{
  border-bottom: 1px solid #bbb;
}
table th, td {
  border-bottom: 1px solid #c7c7c7;
  border-left: 1px solid #c7c7c7;
  box-shadow: inset 0px 0px #c7c7c7, 0 1px #c7c7c7;  
}
tbody tr td{
  border-left: 1px dotted #bbb;
}
.editclass{
  min-width: 120px; 
  border-left: 1px dotted #c7c7c7; 
  text-align: center;
}
</style>
