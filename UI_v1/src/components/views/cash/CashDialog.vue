<template>
  <div class="cashbox">
    <div class="cashbox__header">
      <div class="icon icon-24 mi-recent-log"></div>
      <div class="cashbox__title">Phiếu chi {{valueRefCode}}</div>
      <div class="cashbox__input">
        <div class="cashbox__input--size">
          <!-- <Autocomplete/> -->
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
              <span class="text">Đối tượng <p style="color: red; display: inline">*</p></span>             
              <!-- <Autocomplete ref="object" :value="cash.organizationUnitName" :object="'object'" @sendNameToCashDialog="getNameData" @sendDataByInput="getDataInput" :changeData="changeData"/> -->
              <div class="department-box" :class="{'input-focus': toggleObject == false,'input-error': (messageObject != '')}">
                <div class="selected-option">
                  <input
                    type="text"
                    ref="focusInputObject"
                    class="input-select"
                    @keydown.up="up('object')"
                    @keydown.down="down('object')"
                    @keydown.enter="enter('object')"
                    v-model="showObject"
                    @focus="focusInputKey('object')"
                    @input="onChangeInputObject"
                  />
                  <div class="icon-selected">
                    <div
                      class="icon icon-30 arrow-dropdown"
                      :class="{ tranform: toggleObject == false }"
                      @click="onBtnDropdownClick('object')"
                    ></div>
                    <!-- tranform-again -->
                  </div>
                </div>
              </div>
              <div class="select-custom" :class="{ invisible: toggleObject }">
                <div class="header-select">
                  <div class="text code--size text-hidden">Đối tượng</div>
                  <div class="text name--size text-hidden">Tên đối tượng</div>
                  <div class="text department--size text-hidden">Đơn vị</div>
                  <div class="text phone--size text-hidden">Số điện thoại</div>
                </div>
                <div class="department-content" ref="positionDepartment" 
                  v-for="(employee, index) in employees"
                  :key="index"
                  @click="onBtnEmployeeClick(employee ,index, 'object')"
                  :class="{ color: currentIndex == index}"
                >
                  <div class="scrollItem" >
                    <div class="code--size text-hidden">{{ employee.employeeCode }}</div>
                    <div class="name--size text-hidden">{{ employee.fullName }}</div>
                    <div class="department--size text-hidden">{{ employee.departmentName }}</div>
                    <div class="phone--size text-hidden">{{ employee.phoneNumber }}</div>
                  </div>
                </div>
              </div>
              <span class="span">{{messageObject}}</span>
            </div>
            <div class="receive">
              <span class="text">Người nhận <p style="color: red; display: inline">*</p></span>
              <input ref="focusReceiver" type="text" class="input--size" v-model="cash.receiver" @input="onChangeInputReceiver" :class="{'input-error': messageReceiver != ''}"/>
              <span class="span">{{messageReceiver}}</span>
            </div>
            <div class="date__form">
              <span class="text">Ngày hạch toán <p style="color: red; display: inline">*</p></span><br />
              <DatePicker v-model="cash.accountingDate" :accountingDate="'accountingDate'" @sendAccountingDate="getAccountingDate"/>
            </div>
          </div>
          <div class="row__input">
            <div class="address">
              <span class="text">Địa chỉ</span>
              <input type="text" class="input--size" v-model="cash.organizationUnitAddress"/>
            </div>
            <div class="date__form">
              <span class="text">Ngày phiếu chi <p style="color: red; display: inline">*</p></span><br />
              <DatePicker v-model="cash.refDate" :refDate="'refDate'" @sendRefDate="getRefDate"/>
            </div>
          </div>
          <div class="row__input">
            <div class="address">
              <span class="text">Lý do chi</span>
              <input type="text" class="input--size" v-model="cash.description"/>
            </div>
            <div class="date__form">
              <span class="text">Số phiếu chi <p style="color: red; display: inline">*</p></span><br />
              <input maxlength="50" ref="focusRefCode" type="text" class="input--size" v-model="cash.receiptPaymentCode" @input="onChangeRefCode" :class="{ 'input-error': messageCode != ''}"><br/>
              <span style="color: red; font-size: 12px">{{messageCode}}</span>
            </div>
          </div>
          <div class="row__input">
            <div class="employee">
              <span class="text">Nhân viên <p style="color: red; display: inline">*</p></span>
              <!-- <Autocomplete ref="employee" v-model="cash.fullName" :employee="'employee'" @sendDataEmployee="getDataEmployee" :changeData="changeData"/> -->
              <div class="department-box" :class="{'input-focus': toggleEmployee == false,'input-error': (messageFullName != '')}">
                <div class="selected-option">
                  <input
                    type="text"
                    ref="focusInputEmployee"
                    class="input-select"
                    @keydown.up="up('employee')"
                    @keydown.down="down('employee')"
                    @keydown.enter="enter('employee')"
                    v-model="showEmployeeName"
                    @focus="focusInputKey('employee')"
                    @input="onChangeInputEmployee"
                  />
                  <div class="icon-selected">
                    <div
                      class="icon icon-30 arrow-dropdown"
                      :class="{ tranform: toggleEmployee == false }"
                      @click="onBtnDropdownClick('employee')"
                    ></div>
                  </div>
                </div>
              </div>
              <div class="select-custom" :class="{ invisible: toggleEmployee }">
                <div class="header-select">
                  <div class="text code--size text-hidden">Mã nhân viên</div>
                  <div class="text name--size text-hidden">Tên nhân viên</div>
                  <div class="text department--size text-hidden">Đơn vị</div>
                  <div class="text phone--size text-hidden">Số điện thoại</div>
                </div>
                <div class="department-content" ref="positionDepartment" 
                  v-for="(employee, index) in employees"
                  :key="index"
                  @click="onBtnEmployeeClick(employee ,index, 'employee')"
                  :class="{ color: currentIndexE == index}"
                >
                  <div class="scrollItem" >
                    <div class="code--size text-hidden">{{ employee.employeeCode }}</div>
                    <div class="name--size text-hidden">{{ employee.fullName }}</div>
                    <div class="department--size text-hidden">{{ employee.departmentName }}</div>
                    <div class="phone--size text-hidden">{{ employee.phoneNumber }}</div>
                  </div>
                </div>
              </div>
              <span class="span">{{messageFullName}}</span>
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
          <table border="0" width="100%">
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
              <tr v-for="(list, index) in listDetail" :key="index">
                <td class="first__th">{{index}}</td>
                <td style="border-left: none">
                  <input ref="focusDescriptionDetail" type="text" style="width: 100%" v-model="list.descriptionDetail"/>
                </td>
                <td><input type="text" style="width: 100%" v-model="list.debtAccountDetail"/></td>
                <td><input type="text" style="width: 100%" v-model="list.creditAccountDetail"/></td>
                <td style="text-align: right">
                  <money style="width: 100%; text-align: right;" v-model="list.amountDetail" v-bind="money"/>
                </td>
                <td>
                  <Autocomplete v-model="list.organizationUnitCodeDetail" :code="index" @sendIdToCashDialog="getDataId"/>
                </td>
                <td><input type="text" style="width: 100%; cursor: not-allowed; background: rgb(224 224 224);" v-model="list.organizationUnitNameDetail" readonly/></td>
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
        :changeData="changeData"
        @hideCashPopupAndHideDialog="hideCashPopupAndHideDialog"
        @hideCashPopupAndValidate="hideCashPopupAndValidate"
      />
  </div>
</template>
<script>
// import $ from 'jquery'
import Autocomplete from "../common/Autocomplete.vue";
import {VMoney, Money} from 'v-money'
import CashPopup from '../common/CashPopup.vue'
import DatePicker from '../common/DatePicker.vue'

import { MES_ADD_SUCCESS, MES_EDIT_SUCCESS, STR_DATA_CHANGE, MES_ERROR_SERVER, MES_REQUIRED_ATTRIBUTE, STR_EMPTY_RECEIPTPAYMENT_CODE, STR_EMPTY_EMPLOYEEID, STR_EMPTY_OBJECT, STR_EMPTY_RECEIVER
} from "../../../lang/validation.js";

import {STR_CASHDIALOG, DELETEALLROW, STR_REASONNAME, CHANGEDATA, EMPTYDATA, EXISTDATA, FORMMODE_EDIT, FORMMODE_ADD} from "../../../lang/masterDetail.js"

export default {
  directives: {money: VMoney},
  components: {
    Autocomplete,
    Money, 
    CashPopup,
    DatePicker
  },
  props:{
    cash: {type: Object, default: null},
    flag: {type: String, default: ''},
  },
  data() {
    return {
      saveValueEmployeeName: null,
      saveValueObject: null,
      modeObject: true,
      modeEmployee: true,
      currentIndex: 0,
      currentIndexE: 0,
      toggleObject: true,
      toggleEmployee: true,
      employees: [],
      messageCode: '',
      messageFullName: '',
      messageObject: '',
      messageReceiver: '',
      changeData: '',
      detectChangeCash: {},
      detectChangeDetail: '',
      valueRefCode: null,
      formMode: '',
      valuePopup: false,
      message: '',
      rowIndex: null,
      listDetail: [],
      money: {
          decimal: ',',
          thousands: '.',
          precision: 0,
          masked: false /* doesn't work with directive */
        },
    };
  },
  
  methods: {
    getDataId(code, index, name){
      this.listDetail[index].organizationUnitNameDetail = name;
      this.listDetail[index].organizationUnitCodeDetail = code;
    },
    onBtnDropdownClick(value) {
      if (value === 'object') {
        this.toggleObject = !this.toggleObject;
        if (!this.toggleObject) {
          this.$refs.focusInputObject.focus();
        }
      }
      if (value === 'employee') {
        this.toggleEmployee = !this.toggleEmployee;
        if (!this.toggleEmployee) {
          this.$refs.focusInputEmployee.focus();
        }
      }
    },

    focusInputKey(value) {
      if (value === 'object') {
        this.toggleObject = false;
      }
      if (value === 'employee') {
        this.toggleEmployee = false;
      }     
    },

    onChangeInputObject(e){
      let val = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        if (val !== "") {
        this.messageObject = "";       
      } else if (val == "") {
        this.messageObject = MES_REQUIRED_ATTRIBUTE;
      }
      }, 200);
    },
    onChangeInputEmployee(e){
      let val = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        if (val !== "") {
        this.messageFullName = "";       
      } else if (val == "") {
        this.messageFullName = MES_REQUIRED_ATTRIBUTE;
      }
      }, 200);
    },

    up(value) {
      if (value === 'object') {
        if (this.currentIndex > 0) this.currentIndex--;
      }
      if (value === 'employee') {
        if (this.currentIndexE > 0) this.currentIndexE--;
      }
    },

    /**
     * Click nút up cập nhật vị trí currentIndex và kiểm tra trạng thái hiển thị phòng ban
     * CreatedBY:NXCHIEN 19/05/2021
     */
    down(value) {
      if (value === 'object') {
        if (this.toggleObject) {
          this.toggleObject = false;
        }
        if (this.currentIndex < this.employees.length - 1) this.currentIndex++;
      }
      if (value === 'employee') {
        if (this.toggleEmployee) {
          this.toggleEmployee = false;
        }
        if (this.currentIndexE < this.employees.length - 1) this.currentIndexE++;
      }
    },

    /**
     * Click enter cập nhật các giá trị để bind lên input chứa giá trị tên phòng ban/ đơn vị
     * CreatedBy: NXCHIEN 19/05/2021
     */
    enter(value) {
      if (value === 'object') {
        this.saveValueObject = this.employees[this.currentIndex].fullName;
        this.cash.receiver = this.saveValueObject;
        this.cash.organizationUnitAddress = this.employees[this.currentIndex].address;
        this.cash.organizationUnitName = this.saveValueObject;
        this.toggleObject = true;
        this.messageObject = "";
        this.modeObject = false;
      }
      if (value === 'employee') {
        this.saveValueEmployeeName = this.employees[this.currentIndex].fullName;
        this.cash.employeeId = this.employees[this.currentIndex].employeeId;
        this.cash.fullName = this.saveValueEmployeeName;
        this.toggleEmployee = true;
        this.messageFullName = "";
        this.modeEmployee = false;
      }
      
    },
    
    onBtnEmployeeClick(employee, index, value) {
      if (value === 'object') {
        this.saveValueObject = employee.fullName;
        this.cash.receiver = this.saveValueObject;
        this.cash.organizationUnitAddress = employee.address;
        this.cash.organizationUnitName = this.saveValueObject;
        this.toggleObject = true;
        this.messageObject = "";
        this.currentIndex = index;
        this.modeObject = false;
      }
      if (value === 'employee') {
        this.saveValueEmployeeName = employee.fullName;
        this.cash.employeeId = employee.employeeId;
        this.cash.fullName = this.saveValueEmployeeName;
        this.toggleEmployee = true;
        this.messageFullName = "";
        this.currentIndexE = index;
        this.modeEmployee = false;
      }
    },

    onChangeInputReceiver(e){
      let val = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        if (val !== "") {
          this.messageReceiver = "";
        } else if (val === "") {
          this.messageReceiver = MES_REQUIRED_ATTRIBUTE;
        }
      }, 200);
    },

    hideCashPopupAndValidate(){
      this.valuePopup = false;
      if (this.messageCode != '') {
        this.messageObject = '';
        this.messageReceiver = '';
        this.messageFullName = '';     
        this.$refs.focusRefCode.focus();
      }else if(this.messageObject != ''){
        this.messageReceiver = '';
        this.messageFullName = '';
        this.$refs.focusInputObject.focus();
      }else if(this.messageReceiver != ''){
        this.messageFullName = '';
        this.$refs.focusReceiver.focus();
      }else if(this.messageFullName != ''){
        this.$refs.focusInputEmployee.focus();
      }
    },
    //#region Cập nhật DatePicker
    /**
     * Cập nhật dữ liệu ngày tháng khi lấy từ component DatePicker
     * CreatedBY: NXCHIEN 06/06/2021
     */
    getAccountingDate(value){
      this.cash.accountingDate = value;
    },
    getRefDate(value){
      this.cash.refDate = value;
    },
    //#endregion
    
    //#region Thao tác các dòng trong listDetail với CashPopup
    /**
     * Thao tác Thêm dòng, Xóa hết dòng listDetail với cashPopup
     * CreatedBy: NXCHIEN 06/06/2021
     */
    hideCashPopupNotLoad(){
      this.valuePopup = false;
    },
    // Xóa hết dòng và call đến CashPopup
    onBtnDeleteAllRow(){
      this.valuePopup = true;
      this.message = DELETEALLROW;
      this.formMode = STR_CASHDIALOG;
    },
    // Xóa hết dòng và focus vào ô detail.
    hideCashPopupAndRemoveRow(){
      this.valuePopup = false;
      let arrDetailAdd = [{"descriptionDetail": ""}];
      this.listDetail = arrDetailAdd;
      this.$refs.focusDescriptionDetail[0].focus();
      this.rowIndex = this.listDetail.length;
    },
    // Xóa 1 dòng trong bảng listDetail
    onBtnDeleteRowClick(value){
      this.listDetail.pop(this.listDetail[value]);
      this.rowIndex = this.listDetail.length;
      if(this.rowIndex != 0){
        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() =>{
          this.$refs.focusDescriptionDetail[this.rowIndex - 1].select();
        }, 100)
      }
      
      //TODO: lỗi xóa hết dòng tổng tiền thành NaN ----- Thêm Validate Ngày ko được bỏ trống  ---- Lỗi lên xuống autocomplete
    },
    // Thêm 1 dòng trong bảng listDetail
    onBtnAddRowClick(){
      if (this.listDetail.length == 0) {
        let arrDetailAdd = [{"descriptionDetail": ""}];
        this.listDetail = arrDetailAdd;
        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() =>{
          this.$refs.focusDescriptionDetail[0].focus();
        }, 100)
        this.rowIndex = this.listDetail.length;
        // 
      }else{
        this.rowIndex += 1;
        this.listDetail.push(JSON.parse(JSON.stringify(this.listDetail[this.rowIndex - 2])));
        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() =>{
          this.$refs.focusDescriptionDetail[this.rowIndex - 1].select();
        }, 100)
      }
      
    },
    //#endregion

    // Hiển thị thông báo.
    showNotification(message) {
      this.$notification["success"]({
        message,
        duration: 2,
      });
    },
    
    //#region Đóng cashdialog
    /**
     * Đóng CashDialog và ko load lại data
     * CreatedBy: NXCHIEN 06/06/2021
     */
    onBtnCloseClick() {
      let str = JSON.stringify(this.listDetail);
      if (this.compareObjectCash(this.detectChangeCash, this.cash) || this.cash.receiptPaymentDetail !== str) {
        this.message = STR_DATA_CHANGE;
        this.valuePopup = true;
        this.changeData = CHANGEDATA;
      } else {
        this.$emit("hideCashDialogNotLoad");
      }
    },
    hideCashPopupAndHideDialog(){
      this.$emit("hideCashDialogNotLoad");
    },
    compareObjectCash(obj1, obj2) {
      for (let key in obj2) {
        if (obj2[key] !== obj1[key]) {
          return true;
        }
      }
      return false;
    },
    //#endregion

    /**
     * Lấy data lỗi trả về từ server
     * CreatedBY: NXCHIEN 30/05/2021
     */
    getResponseError(res){
      if(res != undefined){
        if(res.data.code == 500){
          this.message = MES_ERROR_SERVER;
          this.valuePopup = true;
        }
      }
    },

    convertListDetailtoJSON(){
      this.cash.receiptPaymentDetail = JSON.stringify(this.listDetail);
      this.cash.totalAmount = this.totalMoney;
      this.cash.reasonName = STR_REASONNAME;
    },

    /**
     * Kiểm tra empty các trường code, name, departmentId
     * CreatedBy:NXCHIEN 19/05/2021
     */
    checkEmptyAttribute() {
      if (this.cash.receiptPaymentCode.trim() == "") {
        this.messageCode = MES_REQUIRED_ATTRIBUTE;
      }
      if (this.cash.organizationUnitName.trim() == "") {
        this.messageObject = MES_REQUIRED_ATTRIBUTE;
      }
      if (this.cash.receiver.trim() == "") {
        this.messageReceiver = MES_REQUIRED_ATTRIBUTE;
      }         
      if (this.cash.employeeId == "") {
        this.messageFullName = MES_REQUIRED_ATTRIBUTE;
      }    
    },
    /**
     * Kiểm tra các trường bắt buộc nhập
     * CreatedBy: NXCHIEN 19/05/2021
     */
    isCheckValidate() {
      if (this.cash.receiptPaymentCode.trim() == "") {    
        this.message = STR_EMPTY_RECEIPTPAYMENT_CODE;
        this.changeData = EMPTYDATA;
        return true;
      }
      if (this.cash.organizationUnitName.trim() == "") {    
        this.message = STR_EMPTY_OBJECT;
        this.changeData = EMPTYDATA;
        return true;
      }
      if (this.cash.receiver.trim() == "") {    
        this.message = STR_EMPTY_RECEIVER;
        this.changeData = EMPTYDATA;
        return true;
      }
      if (this.cash.employeeId == "") {
        this.message = STR_EMPTY_EMPLOYEEID;
        this.changeData = EMPTYDATA;
        return true;
      }
      return false;
    },
    validAndSave() {
      // Kiểm tra attribute empty
      this.checkEmptyAttribute();
      // Kiểm tra validate attribute
      this.valuePopup = this.isCheckValidate();
      if (!this.valuePopup)
        if (this.flag == FORMMODE_ADD) {
          this.convertListDetailtoJSON();
          return this.axios.post("/ReceiptPayments", this.cash)
            .then((res) => {            
              if (res.data.code == 200) {
                // this.saveValueDepartment = null;
                return Promise.resolve();
              } else if (res.data.code == 400) {
                // Lấy ra message lỗi
                this.message = res.data.data;
                // show popup
                this.valuePopup = true;
                this.changeData = EXISTDATA;
                return Promise.reject();
              }
              return Promise.resolve();
            })
            .catch((res) => {
              this.getResponseError(res);
              return Promise.reject();
            });
        }// Kiểm tra nút Thêm hay Sửa       
        else if (this.flag == FORMMODE_EDIT) {
          this.convertListDetailtoJSON();
          return this.axios.put("/ReceiptPayments/" + this.cash.receiptPaymentId, this.cash)
            .then((res) => {
              if (res.data.code == 200) {
                return Promise.resolve();
              } else if (res.data.code == 400) {
                // Lấy ra message lỗi
                this.message = res.data.data;
                // show popup
                this.valuePopup = true;
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

    /**
     * Lưu thông tin ReceiptPayment
     * CreatedBy: NXCHIEN 06/06/2021
     */
    onBtnSaveClick() {
      this.validAndSave().then(() => {
        if (this.flag == FORMMODE_ADD) {
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
        if (this.flag == FORMMODE_ADD) {
          this.showNotification(MES_ADD_SUCCESS);
        } else {
          this.showNotification(MES_EDIT_SUCCESS);
        }
        this.$emit("saveAndAdd");
        
      });
    },

    /**
     * Cập nhật giá trị refCode khi thay đổi input refCode
     * CreatedBY: NXCHIEN 06/06/2021
     */
    onChangeRefCode(e){
      let val = e.target.value;
      this.valueRefCode = val;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        if (val !== "") {
          this.messageCode = "";
        } else if (val == "") {
          this.messageCode = MES_REQUIRED_ATTRIBUTE;
        }
      }, 200);
    },
  },
  computed:{
    /**
     * Tính tổng tiền trong listDetail
     * CreatedBy: NXCHIEN 06/06/2021
     */
    totalMoney(){
      var total = 0;
      for ( var i = 0, _len = this.listDetail.length; i < _len; i++ ) {
        total += this.listDetail[i].amountDetail;
      }
      if (total == 0) {
        return 0;
      }
      return total;    
    },
    showObject:{
      get(){
        if(this.modeObject){
          return this.cash.organizationUnitName;
        }
        return this.saveValueObject;
      },
      set(val) {
        this.value = val;
      },
    },
    showEmployeeName: {
      get(){
        if(this.modeEmployee){
          return this.cash.fullName;
        }
        return this.saveValueEmployeeName;
      },
      set(val) {
        this.value = val;
      },
    },
  },

  mounted() {
    this.listDetail = JSON.parse(this.cash.receiptPaymentDetail);
    this.rowIndex = this.listDetail.length;
    this.valueRefCode = this.cash.receiptPaymentCode;

    this.detectChangeCash = {...this.cash};
    this.detectChangeDetail = this.cash.receiptPaymentDetail;
    this.axios
      .get(
        `/Employees/Filter?pageSize=20&pageIndex=2&filter=`
      )
      .then((response) => {
        this.employees = response.data.data.data;
      })
      .catch(() => {});

      // $(document).click(function(e) {
      //   // if(!$(".object").contains(e.target)){
      //   //   this.toggleObject = true;
      //   //   // alert('clicked inside');
      //   // }
      //   if($('.object').className !== e.target.className){
      //     clearTimeout(this.timeOut);
      //     this.timeOut = setTimeout(() => {
      //       this.toggleObject = true;
      //     }, 200);
          
    //     }
    // });
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
  /* width: calc(95% + 20px); */
  width: calc(100vw - 56px);
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
.input-error{
  border: 1px solid red;
}
.department-box {
  height: 32px;
  display: flex;
  min-height: 32px;
  border: 1px solid #babec5;
  border-radius: 2px;
  background-color: #fff;
  outline: none;
  width: 100%;
  margin-top: 4px;
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
.selected-option {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 100%;
  border: none;
  outline: none;
}
.input-select {
  background-color: transparent;
  display: flex;
  padding: 5px 0 5px 10px;
  height: 32px;
  border: none;
  width: 84%;
}
.input-focus{
  border-color: #2ca01c;
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
/* .tranform {
  transform: rotate(180deg);
  transition: transform 0.15s linear;
} */
.select-custom {
  position: absolute;
  overflow-y: auto;
  min-width: 200px;
  max-height: 160px;
  background-color: white;
  border: 1px solid #bbb;
}
.object .select-custom {
  width: calc(55% + 13px);
  top: 65px;
}
.employee .select-custom {
  width: 40%;
  top: 275px;
  min-width: 200px;
  z-index: 4;
}
.cashbox__input--size .select-custom {
  top: 43px;
  z-index: 10;
  width: 28%;
}
.grid__height .department-box{
  margin-top: 0;
}
.grid__height .select-custom{
  /* top: 475px; */
  top: 280px;
  width: 60%;
  z-index: 1;
  left: 390px;
}
.reason .select-custom {
  top: 213px;
  width: 20%;
  z-index: 5;
}
.header-select {
  height: 32px;
  width: 100%;
  display: flex;
  align-items: center;
  background-color: #f4f5f8;
  padding: 0 14px 0 10px;
  position: sticky;
  top: 0;
}
.text{
  font-weight: 700;
  color: #111111;
  font-size: 12px;
}
.select-custom .scrollItem {
  height: 32px;
  width: 100%;
  padding: 0 14px 0 10px;
  text-align: left;
  line-height: 32px;
  display: flex;
  align-items: center;
  font-size: 12px;
}
.scrollItem:hover {
  color: #2ca01c;
  background-color: rgb(219, 219, 219);
}
.text-hidden{
  text-overflow: ellipsis;
  display: block;
  white-space: nowrap;
  overflow: hidden;
}
.header-select .code--size, .scrollItem .code--size{
  width: 20%;
}

.header-select .name--size, .scrollItem .name--size{
  width: 35%;
}
.header-select .department--size, .scrollItem .department--size{
  width: 25%;
}
.header-select .phone--size, .scrollItem .phone--size{
  width: 20%;
}
.invisible {
  display: none;
}
.color {
  background-color: #2ca01c;
  color: white;
}
.visible{
  display: flex;
}
.input-error{
  border: 1px solid red;
}
.span{
  color: red; 
  font-size: 12px;
}
.tranform {
  transform: rotate(180deg);
  transition: transform 0.15s linear;
}
.tranform-again {
  transform: rotate(0deg);
  transition: transform 0.15s linear;
}
</style>