<template>
  <div class="content">
    <div class="box">
      <div class="content-item">
        <div class="content-item-text">Thu chi tiền mặt</div>
        <div class="component-btn">
          <button class="btn-add tooltip tooltip--position30-30" @click="onBtnAddClick" v-hotkey="keymap">
            Thêm chi tiền
              <span class="tooltip__text">Ctrl + 2</span>
          </button>
        </div>
      </div>
    </div>
    <div class="content-table">
      <div class="item">
        <div class="btn-utilities">
          <div class="check--all">
            <div class="icon icon-24 mi-arrow-check-all"></div>
          </div>
          <button class="btn-btn" style="border: 2px solid #b1b2b3">
            <div class="flex btn-btn-text tooltip tooltip--position-10-0">
              <span class="pr-4" style="color: #afafaf">Thực hiện hàng loạt</span>
              <div class="icon icon-16 arrow-up--black" style="opacity: 0.5"></div>
              <span class="tooltip__text">Chức năng đang phát triển</span>
            </div>
          </button>
          <CashFilter @onBtnCashFilterClick="onBtnCashFilterClick"/>
          <div v-if="startDate != ''" class="dateFilter">{{startDate | dateFormatDDMMYY}} - {{endDate | dateFormatDDMMYY}}</div>
          <div v-if="startDate != ''" class="icon icon-16 mi-close--small" @click="onBtnDeleteCondition"></div>
        </div>
        <div class="item-right">
          <input
            ref="focusInputSearch"
            type="text"
            placeholder="Nhập từ khóa tìm kiếm"
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
              <th class="first__th"><input type="checkbox" class="check-box" @click="onBtnCheckedClick"/></th>
              <th style="min-width: 150px; border-left: none; text-align: center;">NGÀY HẠCH TOÁN</th>
              <th style="min-width: 125px">SỐ CHỨNG TỪ</th>
              <th style="min-width: 400px">DIỄN GIẢI</th>
              <th style="min-width: 150px; text-align: right;">SỐ TIỀN</th>
              <th style="min-width: 228px">ĐỐI TƯỢNG</th>
              <th style="min-width: 323px">LÝ DO THU/CHI</th>
              <th style="min-width: 120px; z-index: 101;">CHỨC NĂNG</th>
            </tr>
          </thead>
          <tbody v-if="cashs != undefined">
            <tr
              v-for="(cash, index) in cashs"
              :key="index"
              @dblclick="onRowTableDblClick(cash.receiptPaymentId)"
              @click="onRowTableClick(cash.receiptPaymentId)"
              :class="{'row-color': cash.receiptPaymentId == recordId}"
            >
              <td class="first__th" style="z-index: 2;"><input type="checkbox" class="check-box" /></td>
              <td style="border-left: none; text-align: center;">{{ cash.accountingDate | dateFormatDDMMYY}}</td>
              <td>{{ cash.receiptPaymentCode }}</td>
              <td style="max-width: 500px">{{ cash.description }}</td>
              <td style="text-align: right;">{{ cash.totalAmount | formatMoney}}</td>
              <td>{{ cash.organizationUnitName }}</td>
              <td>{{ cash.reasonName }}</td>
              <td class="editclass" :style="{ 'z-index': 100 - index }">
                <div class="btn-edit">
                  <button
                    class="btn-btn hover"
                    @click="onBtnEditClick(cash.receiptPaymentId, cash.receiptPaymentCode)"
                  >
                    <div class="flex btn-btn-text">
                      <span
                        class="pr-4 btn--edit"
                        >Sửa</span
                      >
                    </div>
                  </button>
                  <DropdownDuplicateAndDelete
                    @showPopup="
                      showCashPopup(cash.receiptPaymentId, cash.receiptPaymentCode)
                    "
                    @showDialog="duplicateClick(cash.receiptPaymentId)"
                  />
                </div>
              </td>
            </tr>
          </tbody>
          <tfoot>
              <tr>
                <th style=" width: 40px;
                  position: sticky;
                  left: 0px;
                  z-index: 2;"></th>
                <th style="min-width: 150px; border-left: none; text-align: center;">Tổng</th>
                <th style="min-width: 125px"></th>
                <th style="min-width: 320px"></th>
                <th style="min-width: 150px; text-align: right;">{{totalMoney | formatMoney}}</th>
                <th style="min-width: 228px"></th>
                <th style="min-width: 323px"></th>
                <th style="min-width: 120px; z-index: 92;"></th>
              </tr>
          </tfoot>
        </table>        
      </div>
      <div class="message" v-if="!cashs || cashs.length <= 0">
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
          <div v-if="totalPages > 1">
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
    </div>
    <CashDialog v-if="show" @hideCashDialogNotLoad="hideCashDialogNotLoad" @hideCashDialog="hideCashDialog" :cash="selectedCash" :flag="status"/>
    <CashPopup     
      v-if="valuePopup"
      @hideCashPopupNotLoad="hideCashPopupNotLoad"
      @hideCashPopup="hideCashPopup"
      :refId="recordId"
      :formMode="formMode"
      :message="message"
    />
    <div class="fa-3x" v-if="isBusy">
      <i class="fas fa-spinner fa-spin" style="color: green"></i>
    </div>
    
  </div>
</template>
<script>
//#region Import
import CashDialog from "./CashDialog.vue";
import DropdownDuplicateAndDelete from "../common/DropdownDuplicateAndDelete.vue";
import CashPopup from "../common/CashPopup.vue";
import ComboboxFilter from "../common/ComboboxFilter.vue";
import CashFilter from "../common/CashFilter.vue";
import moment from 'moment'
import {arrDetailAdd, STR_DESCRIPTION_DETAIL, STR_DELETE_RP, STR_CONTINUE, STR_CASHLIST, FORMMODE_ADD, FORMMODE_EDIT} from "../../../lang/masterDetail.js"
//#endregion
export default {
  //#region Khai báo
  components: {
    CashDialog,
    DropdownDuplicateAndDelete,
    CashPopup,
    ComboboxFilter,
    CashFilter
  },
  data() {
    return {
      cashs: [], // Mảng ReceiptPayment
      show: false, // Giá trị hiển thị dialog
      selectedCash: {}, // data 1 ReceiptPayment khi dblClick hoặc click btn Sửa
      recordId: null, // Lưu giá trị của EmployeeId để truyền qua Popup
      status: null, // Trạng thái nút là Thêm mới hay Sửa
      isBusy: false, // Trạng thái của icon Loading
      valuePopup: false, // Giá trị hiển thị Popup
      totalRecord: 0, // Tổng số bản ghi Empployee
      pageSize: 20, // Bao nhiêu ReceiptPayment / trang
      filter: "", // Giá trị truyền vào input để lọc
      pageIndex: 1, // Trang hiện tại
      totalPages: 1, // Tổng số trang
      valueSelect: true, // Hiển thị combobox filter phân trang
      msgSelect: " bản ghi trên 1 trang", // message default
      msgSelected: "20 bản ghi trên 1 trang", // message hiển thị khi phân trang.

      //#region data cho CashDialog
      totalMoney: 0,
      formMode: "",
      message: '',
      startDate: "",
      endDate: "",
      //#endregion

      checkedAll: false,
      checked: false,
      listChecked: [],
    };
  },
  //#endregion
  
  created() {
    window.addEventListener('click', (e) => {
      if (!this.$el.contains(e.target)){
        this.recordId = null;  // Xoa class onColor sau khi click ra ngoài phạm vi content
      }
    })
    /**
     * Lọc dữ liệu hiển thì mặc định 20 bản ghi/ trang
     * CreatedBy: NXCHIEN 17/05/2021
     */
    this.filterData();
  },

  //#region METHODS
  methods: {
    onBtnCheckedClick(){
      this.checkedAll = !this.checkedAll;
      for (let i = 0; i < this.cashs.length; i++) {
        document.getElementsByClassName('check-box')[i + 1].checked = this.checkedAll;
      }
    },
    onRowTableClick(id){
      this.recordId = id;
    },
    onBtnDeleteCondition(){
      this.startDate = '';
      this.endDate = '';
      this.filterData();
    },
    //#region BtnAddClick
    /**
     * Gán selectedCash khi click BtnAdd.
     * CreatedBY: NXCHIEN 30/05/2021
     */
    onBtnAddAssignSelectedCash(response, formMode){
      this.status = formMode;
      // Hiển thị dialog
      this.show = true;
      // Gán tất cả các ô data của dialog rỗng
      this.selectedCash = {};
      // Gán code Max cho ô Mã ReceiptPayment và 1 số thuộc tính khác.
      this.selectedCash.receiptPaymentCode = response.data.data;
      this.selectedCash.accountingDate = this.getCurrentDate();
      this.selectedCash.description = STR_DESCRIPTION_DETAIL;
      this.selectedCash.organizationUnitName = "";
      this.selectedCash.organizationUnitAddress = "";
      this.selectedCash.employeeId = "";
      this.selectedCash.fullName = "";
      this.selectedCash.receiver = "";
      this.selectedCash.refDate = this.getCurrentDate();
      this.selectedCash.refAttach = '';
      this.selectedCash.receiptPaymentDetail = JSON.stringify(arrDetailAdd);
    },
    // Lấy ra ngày hiện tại
    getCurrentDate(){
      let currentDate = new Date();
      return moment(currentDate).format("YYYY-MM-DD");
    },
    /**
     * Click thêm mới 1 ReceiptPayment
     * CreatedBy: NXCHIEN 17/05/2021
     */
    onBtnAddClick() {
      this.filterData();
      this.axios
        .get("/ReceiptPayments/ReceiptPaymentCode")
        .then((response) => {
          this.onBtnAddAssignSelectedCash(response, FORMMODE_ADD);
        })
        .catch(() => {});
    },
    //#endregion

    //#region Close CashDialog
    /* 
    Đóng CashDialog mà không load lại dữ liệu (được gọi từ CashDialog)
    CreatedBy: NXCHIEN 17/05/2021  
    */
    hideCashDialogNotLoad() {
      this.show = false;
      this.$refs.focusInputSearch.focus();
    },

    /* 
    Đóng CashDialog có load lại dữ liệu (được gọi từ CashDialog)
    CreatedBy: NXCHIEN 17/05/2021 
    */
    hideCashDialog() {
      this.show = false;
      this.filterData();
      this.$refs.focusInputSearch.focus();
    },
    //#endregion

    //#region Đóng mở CashPopup
    /* 
    Hiển thị CashPopup được gọi từ (dropdownDuplicateAndDelete)
    CreatedBy: NXCHIEN 17/05/2021  
    */
    showCashPopup(receiptPaymentId, receiptPaymentCode) {

      this.valuePopup = true;
      this.formMode = STR_CASHLIST;
      this.message = STR_DELETE_RP +`${receiptPaymentCode}` + STR_CONTINUE;
      // Lưu giá trị Id khi click vào nút sửa trên bảng.
      this.recordId = receiptPaymentId;
    },

    /* 
    Đóng popup có load lại dữ liệu
    CreatedBy: NXCHIEN 17/05/2021  
    */
    hideCashPopup() {
      this.valuePopup = false;
      this.filterData();
      this.$refs.focusInputSearch.focus();
    },

    /* 
    Đóng popup mà không load lại dữ liệu
    CreatedBy: NXCHIEN 17/05/2021  
    */
    hideCashPopupNotLoad() {
      this.valuePopup = false;
      this.$refs.focusInputSearch.focus();
    },
    //#endregion

    //#region Nhân bản và dbClick và nút Sửa
    /**
     * Hàm dùng chung khi click nhân bản và dblClick 1 dòng trong bảng
     * CreatedBy: NXCHIEN 17/05/2021
     */
    onShowDialogAndAssignSelectedCash(response, formMode){
      this.status = formMode;
      this.show = true;
      //Fill employee vào dialog
      this.selectedCash = response.data.data;      
    },

    /* 
    dblClick vào 1 dòng trong table
    - Lấy ra 1 ReceiptPayment được chọn
    CreatedBy: NXCHIEN 17/05/2021  
    */
    onRowTableDblClick(eId) {
      this.axios
        .get("/ReceiptPayments/" + eId)
        .then((response) => {
          this.onShowDialogAndAssignSelectedCash(response, FORMMODE_EDIT);
        })
        .catch(() => {});
    },

    duplicateClick(value) {
      this.axios
        .get("/ReceiptPayments/ReceiptPaymentCopy?id=" + value)
        .then((response) => {
          this.onShowDialogAndAssignSelectedCash(response, FORMMODE_ADD);
        })
        .catch(() => {});
    },
    /**
      * Click nút Sửa trong table
      * CreatedBy: NXCHIEN 06/06/2021
      */
    onBtnEditClick(receiptPaymentId) {
      this.onRowTableDblClick(receiptPaymentId);
    },
    //#endregion

    /**
      * Load lại dữ liệu khi click vào nút refresh
      * CreatedBy: NXCHIEN 06/06/2021
      */
    onBtnRefreshClick() {
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() =>{
        this.totalPages = 1;
        this.pageIndex = 1;
        this.startDate = '';
        this.endDate = '';
        this.filterData();
      }, 500);
    },

    /**
     * Lọc theo ngày hạch toán 
     * CreatedBY: NXCHIEN 09/06/2021
     */
    onBtnCashFilterClick(startDate, endDate){
      this.startDate = startDate;
      this.endDate = endDate;     
      this.filterDataByDateNotNull();
    },

    //#region Lọc dữ liệu: ChangeInput + Phân trang và Export excel
    /**
      * Lọc data bằng các tham số truyền vào
      * CreatedBy: NXCHIEN 06/06/2021
      */
    filterData() {
      this.isBusy = true;
      this.axios
        .get(
          `/ReceiptPayments/Filter?pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&filter=${this.filter}`
        )
        .then((response) => {
          this.assignValueCashArr(response);
        })
        .catch(() => {})
        .then(() => {
          this.isBusy = false;
        });
    },
    // Lọc dữ liệu từ ngày
    filterDataByDateNotNull() {
      this.isBusy = true;
      this.axios
        .get(
          `/ReceiptPayments/FilteringDate?pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&startDate=${this.startDate}&endDate=${this.endDate}&filter=${this.filter}`
        )
        .then((response) => {
          this.assignValueCashArr(response);
        })
        .catch(() => {})
        .then(() => {
          this.isBusy = false;
        });
    },
    assignValueCashArr(response){
      // Gán mảng ReceiptPayment ban đầu = data từ server trả về
      this.cashs = response.data.data.data;
      // tổng số bản ghi = tổng số bản ghi từ server trả về
      this.totalRecord = response.data.data.totalRecord;
      // tổng số trang.
      this.totalPages = response.data.data.totalPages;
      if (response.data.data.totalRecord == undefined) {
        this.totalRecord = 0;
      }
      if (response.data.data.totalMoney != undefined) {
        this.totalMoney = response.data.data.totalMoney;
      }else{
        this.totalMoney = 0;
      }
    },

    /**
      * Kiểm tra giá trị input thay đổi thì lọc mảng ReceiptPayment bằng cách gọi tới API
      * CreatedBy: NXCHIEN 06/06/2021
      */
    onChangeInputEmployeeFilter(e) {
      let val = e.target.value;
      // Lấy chuỗi cần lọc rồi gán vào biến filter
      this.filter = val;
      // Gán trang về 1
      this.pageIndex = 1;
      // Lọc data
      this.clearTimeOutFn();
  },

    /**
      * Kiểm tra click thay đổi phân trang, Lọc lại mảng ReceiptPayment khi click.
      * CreatedBy: NXCHIEN 06/06/2021
      */
    onPageChange(page) {
      // Thay đổi trang khi click button phần phân trang
      this.pageIndex = page;

      this.clearTimeOutFn();
    },

    /**
      * Export data ra file excel. 
      * CreatedBy: NXCHIEN 06/06/2021
      */
    onBtnExportClick() {  
      // Mở 1 cửa số mới gọi API để tải về.
      window.open(
        `https://localhost:44314/api/v1/ReceiptPayments/ExportingExcel`,
        "_blank"
      );
    },
    //#endregion

    //#region Lấy dữ liệu và đóng mở form phần lọc bản ghi / trang
    /**
     * Lấy dữ liệu từ comboboxFilter để phân trang
     * CreatedBy: NXCHIEN 17/05/2021
     */
    getValueFromComboboxFilter(value) {
      // gán message thành message cần để hiển thị  "20 bản ghi trên 1 trang..."
      this.msgSelected = "";
      this.msgSelected = value + this.msgSelect;
      // số ReceiptPayment trên/ trang
      this.pageSize = value;
      // trang số 1
      this.pageIndex = 1;
      // Lọc ReceiptPayment
      this.clearTimeOutFn();
      this.valueSelect = true;
    },
    clearTimeOutFn(){
      clearTimeout(this.timeOut);
      // Gọi hàm lọc có delay 0.5s để không gửi quá nhiều request lên server
      if (this.startDate == '') {
        this.timeOut = setTimeout(() =>{
          this.filterData();
        }, 500)
      }else{
        this.timeOut = setTimeout(() =>{
          this.filterDataByDateNotNull();
        }, 500)
      }
    },

    showSelected() {
      this.valueSelect = !this.valueSelect;
    },
    //#endregion
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
        "Ctrl + 2": this.onBtnAddClick,
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
  
  .pr-4 {
    padding-right: 4px !important;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
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
    justify-content: space-between;
  }
  .item .item-right {
    display: flex;
    align-items: center;
    width: 310px;
    justify-content: space-between;
  }
  .content-table .content-table-height {
    height: calc(100% - 70px);
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
    height: 51px;
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
    border: 1px solid transparent;
    padding: 6px 0px 6px 6px;
    box-sizing: border-box;
    margin-left: 10px;
    
  }
  .hover:hover{
    border-radius: 2px;
    border: 1px solid #0075c0;
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
    border: 1px solid transparent;
  }
  .margin:hover{
    font-weight: 800;
    border: 1px solid #bbb;
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
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
  }
  .editclass{
    min-width: 120px; 
    border-left: 1px dotted #c7c7c7; 
    text-align: center;
  }
  .btn-utilities {
    display: flex;
    margin-right: 12px;
    align-items: center;
  }
  .check--all{
    padding: 13px 12px;
  }
  .dateFilter{
    display: inline-block;
    position: relative;
    margin-right: 10px;
    color: #0075c0;
    padding-left: 20px;
    font-size: 12px;
    width: 33%;
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
  }
  .dateFilter:hover{
    text-decoration: underline;
    cursor: pointer;
  }
  .btn--edit{
    color: #0075c0; 
    font-weight: 600;
  }
  .row-color{
    background-color: #f3f8f8;
  }
  .row-color td{
    background-color: #f3f8f8;
  }
</style>
