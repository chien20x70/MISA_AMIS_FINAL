<template>
  <div class="cashbox">
    <div class="cashbox__header">
      <div class="icon icon-24 mi-recent-log"></div>
      <div class="cashbox__title">Phiếu chi PC000000</div>
      <div class="cashbox__input">
        <div class="cashbox__input--size">
          <Autocomplete/>
        </div>
      </div>
      <div class="cashbox__icon icon icon-24 mi-setting__detail"></div>
      <div class="cashbox__icon icon icon-24 mi-help"></div>
      <div
        class="cashbox__icon icon icon-24 mi-close"
        @click="onBtnCloseClick"
      ></div>
    </div>
    <div class="cashbox__content">
      <div class="content__information">
        <div class="basic__form">
          <div class="row__input">
            <div class="object">
              <span class="text">Đối tượng</span>             
              <Autocomplete :value="cash.organizationUnitName" @sendNameToCashDialog="getNameData"/>
            </div>
            <div class="receive">
              <span class="text">Người nhận</span>
              <input type="text" class="input--size" v-model="cash.receiver"/>
            </div>
            <div class="date__form">
              <span class="text">Ngày hạch toán</span><br />
              <date-pick
                v-model="cash.accountingDate"
                :displayFormat="STR_DISPLAY_FORMAT"
                :inputAttributes="{
                  class: 'style-input-date-lib',
                  placeholder: STR_DISPLAY_FORMAT,
                  style: 'margin-top: 4px; width: 168px;',
                }"
                :weekdays="localeDatePicker.weekdays"
                :months="localeDatePicker.months"
              ></date-pick>
            </div>
          </div>
          <div class="row__input">
            <div class="address">
              <span class="text">Địa chỉ</span>
              <input type="text" class="input--size" v-model="cash.organizationUnitAddress"/>
            </div>
            <div class="date__form">
              <span class="text">Ngày phiếu chi</span><br />
              <date-pick
                v-model="cash.refDate"
                :displayFormat="STR_DISPLAY_FORMAT"
                :inputAttributes="{
                  class: 'style-input-date-lib',
                  placeholder: STR_DISPLAY_FORMAT,
                  style: 'margin-top: 4px; width: 168px;',
                }"
                :weekdays="localeDatePicker.weekdays"
                :months="localeDatePicker.months"
              ></date-pick>
            </div>
          </div>
          <div class="row__input">
            <div class="address">
              <span class="text">Lý do chi</span>
              <input type="text" class="input--size" v-model="cash.description"/>
            </div>
            <div class="date__form">
              <span class="text">Số phiếu chi</span><br />
              <input type="text" class="input--size" v-model="cash.receiptPaymentCode">
            </div>
          </div>
          <div class="row__input">
            <div class="employee">
              <span class="text">Nhân viên</span>
              <Autocomplete v-model="cash.fullName" @sendDataEmployee="getDataEmployee"/>
            </div>
            <div class="attach">
              <span class="text">Kèm theo</span>
              <input type="text" class="input--size" placeholder="Số lượng" v-model="cash.refAttach"/>
            </div>
            <div class="invoice">chứng từ gốc</div>
          </div>
          <div class="row__reference">
            <div class="reference__title">Tham chiếu</div>
            <div class="reference__show__more">...</div>
          </div>
        </div>
        <div class="summary__info">
          <div class="summary__title">Tổng tiền</div>
          <div class="summary__number" >{{totalMoney | formatMoney}}</div>
        </div>
      </div>
      <div class="content__grid">
        <div class="detail">
          <div class="hover">Hạch toán</div>
        </div>
        <div class="grid__height">
          <table border="0">
            <thead>
              <tr>
                <th class="first__th">#</th>
                <th style="min-width: 315px; border-left: none">DIỄN GIẢI</th>
                <th style="min-width: 152px">TK NỢ</th>
                <th style="min-width: 146px">TK CÓ</th>
                <th style="min-width: 213px; text-align: right">SỐ TIỀN</th>
                <th style="min-width: 204px">ĐỐI TƯỢNG</th>
                <th style="min-width: 327px">TÊN ĐỐI TƯỢNG</th>
                <th style="min-width: 40px; z-index: 102"></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(list, index) in listDetailFilter" :key="index">
                <td class="first__th">{{index}}</td>
                <td style="border-left: none">
                  <input ref="focusDescriptionDetail" type="text" style="width: 100%" v-model="list.DescriptionDetail"/>
                </td>
                <td><input type="text" style="width: 100%" v-model="list.DebtAccount"/></td>
                <td><input type="text" style="width: 100%" v-model="list.CreditAccount"/></td>
                <td style="text-align: right">
                  <money style="width: 100%; text-align: right;" v-model="list.Amount" v-bind="money"/>
                </td>
                <td><Autocomplete v-model="list.OrganizationUnitCode" :code="index" @sendIdToCashDialog="getDataId"/></td>
                <td><input type="text" style="width: 100%; cursor: pointer;" v-model="list.OrganizationUnitName" readonly/></td>
                <td class="editclass">
                  <div class="icon icon-16 mi-delete" @click="onBtnDeleteRowClick(index)"></div>
                </td>
              </tr>
            </tbody>
            <tfoot>
              <tr>
                <th
                  style="width: 40px; position: sticky; left: 0px; z-index: 2"
                ></th>
                <th style="min-width: 187px; border-left: none"></th>
                <th style="min-width: 100px"></th>
                <th style="min-width: 100px"></th>
                <th style="min-width: 150px; text-align: right">{{totalMoney | formatMoney}}</th>
                <th style="min-width: 150px"></th>
                <th style="min-width: 250px"></th>
                <th style="min-width: 40px; z-index: 101"></th>
              </tr>
            </tfoot>
          </table>
        </div>
      </div>
      <div class="grid__item">
        <div class="item__flex">
          <button class="btn-add-row" @click="onBtnAddRowClick">Thêm dòng</button>
          <button class="btn-add-row" @click="onBtnDeleteAllRow">Xóa hết dòng</button>
        </div>
        <div class="upload">
          <div class="upload__flex">
            <div class="icon icon-18 mi-attach"></div>
            <div style="font-size: 12px; font-weight: 700; color: #111">
              Đính kèm
            </div>
            <div style="margin-left: 15px; color: #757575; font-size: 12px">
              Dung lượng tối đa 5MB
            </div>
          </div>
          <input
            type="text"
            class="attach__file"
            placeholder="Kéo/thả tệp vào đây hoặc bấm vào đây"
            readonly
          />
        </div>
      </div>
    </div>
    <div class="cashbox__footer">
      <button class="btn-common">Hủy</button>
      <div class="flex">
        <button class="btn-common" @click="onBtnSaveClick">Cất</button>
        <button class="btn-common btn--success">Cất và In</button>
      </div>
    </div>
    <CashPopup
        v-if="valuePopup"
        @hideCashPopupNotLoad="hideCashPopupNotLoad"
        :message="message"
        @hideCashPopupAndRemoveRow="hideCashPopupAndRemoveRow"
        :formMode="formMode"
      />
  </div>
</template>
<script>
import DatePick from "vue-date-pick";
import Autocomplete from "../common/Autocomplete.vue";
import {VMoney, Money} from 'v-money'
import CashPopup from '../common/CashPopup.vue'

import {
  MES_ADD_SUCCESS,
  MES_EDIT_SUCCESS,
} from "../../../lang/validation.js";

export default {
  directives: {money: VMoney},
  components: {
    DatePick,
    Autocomplete,
    Money, 
    CashPopup
  },
  props:{
    cash: {type: Object, default: null},
    flag: {type: String, default: ''},
  },
  data() {
    return {
      formMode: '',
      valuePopup: false,
      message: '',
      rowIndex: null,
      listDetail: [],
      check: null,
      employeeName: null,
      recordCode: null,
      money: {
          decimal: ',',
          thousands: '.',
          precision: 1,
          masked: false /* doesn't work with directive */
        },
      STR_DISPLAY_FORMAT: "DD/MM/YYYY",
      localeDatePicker: {
        weekdays: ["T2", "T3", "T4", "T5", "T6", "T7", "CN"],
        months: [
          "Tháng 1",
          "Tháng 2",
          "Tháng 3",
          "Tháng 4",
          "Tháng 5",
          "Tháng 6",
          "Tháng 7",
          "Tháng 8",
          "Tháng 9",
          "Tháng 10",
          "Tháng 11",
          "Tháng 12",
        ],
      },
    };
  },
  
  methods: {
    hideCashPopupNotLoad(){
      this.valuePopup = false;
    },
    hideCashPopupAndRemoveRow(){
      this.valuePopup = false;
      let arrDetailAdd = [{"DescriptionDetail": ""}];
      this.listDetail = arrDetailAdd;
      this.$refs.focusDescriptionDetail[0].focus();
    },
    onBtnDeleteAllRow(){
      this.valuePopup = true;
      this.message = "Bạn có thực sự muốn xóa tất cả dòng đã nhập không?";
      this.formMode = "CashDialog";
    },
    onBtnDeleteRowClick(value){
      this.listDetail.pop(this.listDetail[value]);
    },
    onBtnAddRowClick(){
      this.rowIndex += 1;
      this.listDetail.push(this.listDetail[this.rowIndex - 2]);
    },
    showNotification(message) {
      this.$notification["success"]({
        message,
        duration: 1,
      });
    },
    getDataId(valueCode, valueCheck, employeeName){
      this.recordCode = valueCode;
      this.check = valueCheck;
      this.employeeName = employeeName;
    },
    getNameData(valueName, valueAddress){
      this.cash.organizationUnitName = valueName;
      this.cash.organizationUnitAddress = valueAddress;
    },
    getDataEmployee(value){
      this.cash.employee = value;
    },
    onBtnCloseClick() {
      this.$emit("hideCashDialogNotLoad");
    },
    convertListDetailtoJSON(){
      this.cash.receiptPaymentDetail = JSON.stringify(this.listDetail);
      this.cash.totalAmount = this.totalMoney;
      this.cash.reasonName = "Chi khác";
    },

    validAndSave() {
        if (this.flag == "add") {
          this.convertListDetailtoJSON();
          return this.axios.post("/ReceiptPayments", this.cash)
            .then((res) => {            
              if (res.data.code == 200) {
                // this.saveValueDepartment = null;
                return Promise.resolve();
              } else if (res.data.code == 400) {
                // Lấy ra message lỗi
                this.message = res.data.data;
                console.log(this.message);
                // show popup
                // this.valuePopup = true;
                return Promise.reject();
              }
              return Promise.resolve();
            })
            .catch((res) => {
              this.getResponseError(res);
              return Promise.reject();
            });
        }// Kiểm tra nút Thêm hay Sửa       
        else if (this.flag == "edit") {
          this.convertListDetailtoJSON();
          return this.axios.put("/ReceiptPayments/" + this.cash.receiptPaymentId, this.cash)
            .then((res) => {
              if (res.data.code == 200) {
                // this.saveValueDepartment = null;
                return Promise.resolve();
              } else if (res.data.code == 400) {
                // Lấy ra message lỗi
                this.message = res.data.data;
                console.log(this.message);
                // show popup
                // this.valuePopup = true;
                return Promise.reject();
              }
              return Promise.resolve();
            })
            .catch((res) => {
              this.getResponseError(res);
              return Promise.reject();
            });
        }
      return Promise.reject();
    },

    /* 
    Click Save nhân viên
    CreatedBy: NXCHIEN 17/05/2021
    */
    onBtnSaveClick() {
      this.validAndSave().then(() => {
        if (this.flag == "add") {
          this.showNotification(MES_ADD_SUCCESS);
        } else {
          this.showNotification(MES_EDIT_SUCCESS);
        }
        this.$emit("hideCashDialog");
      });
    },

    /**
     * Click nút Cất và Thêm cho phép lưu dữ liệu và reset form để người dùng có thể thêm tiếp mà ko cần click nút Thêm mới.
     * CreatedBy: NXCHIEN 17/05/2021
     */
    onBtnSaveAndAddClick() {
      this.validAndSave().then(() => {
        if (this.flag == "add") {
          this.showNotification(MES_ADD_SUCCESS);
        } else {
          this.showNotification(MES_EDIT_SUCCESS);
        }
        this.$emit("saveAndAdd");
        
      });
    },

    onChangeArr(){
      for (var i = 0, _len = this.listDetail.length; i < _len; i++ ) {
        if(this.check != null){
          this.listDetail[this.check].OrganizationUnitName = this.employeeName; 
          this.listDetail[this.check].OrganizationUnitCode = this.recordCode; 
          return this.listDetail;
        }  
      }
      return this.listDetail;
    },
  },
  computed:{
    totalMoney(){
      var total = 0;
      for ( var i = 0, _len = this.listDetail.length; i < _len; i++ ) {
        total += this.listDetail[i].Amount;
      }
      return total;      
    },
    listDetailFilter(){    
      return this.onChangeArr();  
    } 
  },
  mounted() {
    this.listDetail = JSON.parse(this.cash.receiptPaymentDetail);
    this.rowIndex = this.listDetail.length;
  },
};
</script>

<style scoped>
.cashbox {
  display: flex;
  flex-direction: column;
  background-color: #fff;
  height: 100vh;
  width: 100%;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 104;
}
.cashbox__header {
  background-color: #f4f5f8;
  width: 100%;
  top: 0;
  padding: 9px 16px 16px;
  display: flex;
  align-items: center;
  height: 54px;
}
.cashbox__title {
  font-weight: 700;
  font-size: 24px;
  top: -7px;
  left: 20px;
  padding: 0 0 0 16px;
  color: #111;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.cashbox__input {
  display: flex;
  justify-content: left;
  align-items: center;
  padding-left: 50px;
  flex: 1;
  min-width: 600px;
}
.cashbox__icon + .cashbox__icon {
  margin-left: 16px;
}
.cashbox__input--size {
  width: 350px;
}
.cashbox__content {
  flex: 1;
  min-height: 0;
  min-width: 0;
  overflow: auto;
  position: relative;
  scroll-behavior: smooth;
}
.content__information {
  display: flex;
  /* align-items: center; */
  background-color: #f4f5f8;
  padding: 7px 30px 24px 30px;
}
.basic__form {
  width: 75%;
}
.summary__info {
  width: 25%;
  text-align: right;
}
.summary__title {
  font-size: 13px;
}
.summary__number {
  font-size: 36px;
  font-weight: 700;
}
.row__input {
  width: 100%;
  height: 70px;
  display: flex;
}
.object {
  width: 34%;
}
.receive {
  padding: 0 11px 0 12px;
  width: 44%;
  margin-right: 11px;
}
.date__form {
  width: 22%;
  padding: 0 0 12px 16px;
  border-left: 1px solid #d4d7dc;
}
.address {
  width: 79%;
}
.employee {
  width: 34%;
  padding-right: 14px;
}
.attach {
  width: 16%;
}
.text {
  font-weight: 700;
  color: #111111;
  font-size: 12px;
}
.receive .input--size {
  width: calc(100% + 5px);
  /* width: 100%; */
  margin-top: 4px;
}
.address .input--size {
  width: calc(97% + 9px);
  margin-top: 4px;
}
.attach .input--size {
  width: 100%;
  margin-top: 4px;
  text-align: right;
}
.date__form .input--size {
  width: 168px;
  margin-top: 4px;
}
.input--size::-webkit-input-placeholder {
  font-family: NotoSans-Regular-italic;
  font-size: 12px;
}
.invoice {
  padding: 31px 7px 0;
  font-size: 13px;
}
.row__reference {
  padding-top: 12px;
  width: 100%;
  height: 32px;
  display: flex;
}
.reference__title {
  margin-right: 16px;
  min-width: 75px;
  font-size: 13px;
}
.reference__show__more {
  cursor: pointer;
  color: #0075c0;
  font-weight: 700;
  white-space: nowrap;
  overflow: hidden;
  font-size: 12px;
}
.content__grid {
}
.detail {
  width: calc(100vw - 20px);
  padding: 18px 0 18px 30px;
  position: sticky;
  left: 0;
  display: flex;
  font-size: 13px;
  text-decoration: underline;
  color: #0075c0;
  font-weight: 700;
}
.hover:hover {
  color: #2ca01c;
  cursor: pointer;
}
.grid__height {
  margin-left: 30px;
  overflow-x: auto;
  width: calc(95% + 20px);
}

table tr {
  border-bottom: 1px solid #bbb;
}
table th,
td {
  border-bottom: 1px solid #c7c7c7;
  border-left: 1px solid #c7c7c7;
  box-shadow: inset 0px 0px #c7c7c7, 0 1px #c7c7c7;
}
tbody tr td {
  border-left: 1px dotted #bbb;
  background-color: #f3f8f8;
}
.editclass {
  min-width: 40px;
  border-left: 1px dotted #c7c7c7;
  text-align: center;
  line-height: 40px;
}
table tfoot th {
  bottom: 0px !important;
  height: 32px;
  font-size: 13px;
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
  justify-content: center;
}
.grid__item {
  padding: 0 30px 30px;
  width: 100%;
}
.item__flex {
  display: flex;
  align-items: center;
  padding: 10px 0 36px;
}
.btn-add-row {
  height: 24px;
  font-size: 12px;
  font-weight: 700;
  padding: 2px 20px;
  border-color: #8d9096;
  border-radius: 2.5px;
}
.btn-add-row:hover{
  background-color: #d2d3d6;
}
.btn-add-row + .btn-add-row {
  margin-left: 10px;
}
.upload__flex {
  display: flex;
  align-items: center;
}
.attach__file {
  margin-top: 4px;
  width: 30%;
  height: 50px;
  text-align: center;
  cursor: pointer;
}
.attach__file::-webkit-input-placeholder {
  font-family: NotoSans-Regular-italic;
  font-size: 12px;
}
.cashbox__footer {
  display: flex;
  align-items: center;
  padding: 8px 16px;
  width: 100%;
  background-color: #38393d;
  height: 50px;
  justify-content: space-between;
}
.flex {
  display: flex;
  align-items: center;
}
.btn-common {
  border: 1px solid #6b6c72;
  color: #fff;
  background-color: transparent;
  position: relative;
  height: 34px;
  padding: 6px 20px;
}
.btn-common + .btn-common {
  margin-left: 5px;
}
.btn--success {
  background-color: #35bf22;
  border: none;
}



/**css ------ department */
.department-box {
  height: 32px;
  display: flex;
  min-height: 32px;
  border: 1px solid #babec5;
  border-radius: 2px;
  background-color: #fff;
  outline: none;
  width: 100%;
}
.department-box:focus {
  border-color: #2ca01c;
}
.department-content {
  display: flex;
  align-items: center;
  background-color: white;
  cursor: pointer;
}
.department-content:hover {
  color: #35bf22;
  font-weight: 600;
  background-color: #f4f5f8;
}

.department-box:focus {
  border-color: #2ca01c;
}
.selected-option {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 100%;
}
.input-select {
  background-color: transparent;
  display: flex;
  padding: 5px 0 5px 10px;
  height: 32px;
  border: none;
  width: 90%;
}

.icon-selected {
  display: flex;
  align-items: center;
  justify-content: center;
}
.icon-selected:hover {
  background-color: #bbb;
}
.arrow-dropdown {
  background-position: -552px -352px;
}
.tranform {
  transform: rotate(180deg);
  transition: transform 0.15s linear;
}

.object .select-custom {
  position: absolute;
  overflow-y: auto;
  height: 160px;
  width: 24%;
  top: 65px;
  min-width: 200px;
  background-color: white;
  border: 1px solid #bbb;
}
.header-select {
  height: 32px;
  width: 100%;
  display: flex;
  align-items: center;
  background-color: #f4f5f8;
  padding: 0 14px 0 10px;
}
.select-custom .item {
  height: 32px;
  width: 100%;
  padding: 0 14px 0 10px;
  text-align: left;
  line-height: 32px;
  display: flex;
  align-items: center;
}
.item:hover {
  color: #2ca01c;
  background-color: rgb(219, 219, 219);
}
</style>