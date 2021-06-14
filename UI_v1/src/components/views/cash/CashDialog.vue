<template>
  <div class="cashbox">
    <div class="cashbox__header">
      <div class="icon icon-24 mi-recent-log"></div>
      <div class="cashbox__title">Phiếu chi {{valueRefCode}}</div>
      <div class="cashbox__input">
        <div class="cashbox__input--size">
          <Autocomplete/>
        </div>
      </div>
      <div class="cashbox__icon icon icon-24 mi-setting__detail"></div>
      <div class="cashbox__icon icon icon-24 mi-help"></div>
      <div
        class="cashbox__icon icon icon-24 mi-close tooltip tooltip--position close--hover"
        @click="onBtnCloseClick"
      >
      <span class="tooltip__text">ESC</span></div>
    </div>
    <div class="cashbox__content">
      <div class="content__information">
        <div class="basic__form">
          <div class="row__input">
            <div class="row__input__first">
              <div class="object">
                <span class="text">Đối tượng <p style="color: red; display: inline">*</p></span>             
                <div ref="RefObject" class="department-box" :class="{'input-focus': toggleObject == false, 'input-error': (messageObject != '')}">
                  <div class="selected-option">
                    <!-- <span tabindex="1" ></span> -->
                    <input
                      type="text"
                      ref="focusInputObject"
                      class="input-select"
                      @keydown.up="up('object')"
                      @keydown.down="down('object')"
                      @keydown.enter="enter('object')"
                      v-model="saveValueObject"
                      @focus="focusInputKey('object')"
                      @input="onChangeInputObject"
                      tabindex="2"
                    />
                    <div class="icon-selected">
                      <div
                        class="icon icon-30 arrow-dropdown"
                        :class="{ tranform: toggleObject == false }"
                        @click="onBtnDropdownClick('object')"
                      ></div>
                    </div>
                  </div>
                </div>
                <div class="select-custom" :class="{ invisibleObject: toggleObject }">
                  <div class="header-select">
                    <div class="text code--size text-hidden">Đối tượng</div>
                    <div class="text name--size text-hidden">Tên đối tượng</div>
                    <div class="text department--size text-hidden">Đơn vị</div>
                    <div class="text phone--size text-hidden">Số điện thoại</div>
                  </div>
                  <div class="department-content" ref="positionDepartment" 
                    v-for="(employee, index) in fakeObjects"
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
                <input maxlength="100" tabindex="3" ref="focusReceiver" type="text" class="input--size" v-model="cash.receiver" @input="onChangeInputReceiver" :class="{'input-error': messageReceiver != ''}"/><br/>
                <span class="span">{{messageReceiver}}</span>
              </div>
            </div>
            <div class="date__form">
              <span class="text">Ngày hạch toán <p style="color: red; display: inline">*</p></span><br />
              <DatePicker ref="accountingDate" v-model="cash.accountingDate" :type="'accountingDate'" @sendAccountingDate="getAccountingDate"/><br/>
              <span class="span">{{messageAccountingDate}}</span>
            </div>
          </div>
          <div class="row__input">
            <div class="address">
              <span class="text">Địa chỉ</span>
              <input tabindex="4" type="text" class="input--size" v-model="cash.organizationUnitAddress"/>
            </div>
            <div class="date__form">
              <span class="text">Ngày phiếu chi <p style="color: red; display: inline">*</p></span><br />
              <DatePicker ref="refDate" v-model="cash.refDate" :type="'refDate'" @sendRefDate="getRefDate"/>
              <span class="span">{{messageRefDate}}</span>
            </div>
          </div>
          <div class="row__input">
            <div class="address">
              <span class="text">Lý do chi</span>
              <input tabindex="5" type="text" class="input--size" v-model="cash.description"/>
            </div>
            <div class="date__form">
              <span class="text">Số chứng từ <p style="color: red; display: inline">*</p></span><br />
              <input tabindex="10" maxlength="50" ref="focusRefCode" type="text" class="input--size" 
                v-model="cash.receiptPaymentCode" 
                @input="onChangeRefCode" 
                @keyup.enter="focusDetail"
                :class="{ 'input-error': messageCode != ''}"><br/>
              <span style="color: red; font-size: 12px">{{messageCode}}</span>
            </div>
          </div>
          <div class="row__input">
            <div class="employee">
              <span class="text">Nhân viên <p style="color: red; display: inline">*</p></span>
              <div class="department-box" :class="{'input-focus': toggleEmployee == false,'input-error': (messageFullName != '')}">
                <div class="selected-option">
                  <input
                    type="text"
                    ref="focusInputEmployee"
                    class="input-select"
                    @keydown.up="up('employee')"
                    @keydown.down="down('employee')"
                    @keydown.enter="enter('employee')"
                    v-model="saveValueEmployeeName"
                    @focus="focusInputKey('employee')"
                    @input="onChangeInputEmployee"
                    tabindex="5"
                    maxlength="100"
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
              <div class="select-custom" :class="{ invisibleEmployee: toggleEmployee }">
                <div class="header-select">
                  <div class="text code--size text-hidden">Mã nhân viên</div>
                  <div class="text name--size text-hidden">Tên nhân viên</div>
                  <div class="text department--size text-hidden">Đơn vị</div>
                  <div class="text phone--size text-hidden">Số điện thoại</div>
                </div>
                <div class="department-content" ref="positionDepartment" 
                  v-for="(employee, index) in fakeEmployees"
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
              <input maxlength="10" tabindex="7" type="text" class="input--size" placeholder="Số lượng" v-model="cash.refAttach"/>
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
                <td class="first__th">{{index + 1}}</td>
                <td style="border-left: none">
                  <input ref="focusDescriptionDetail" type="text" style="width: 100%" v-model="list.descriptionDetail"/>
                </td>
                <td>
                  <input class="debtAccount--Detail" ref="focusDebt" type="text" 
                    @blur="onBlurInputListDetail(list.debtAccountDetail, index)"  
                    style="width: 100%"
                    @input="onInputChangeListDetail(list.debtAccountDetail, index)" 
                    v-model="list.debtAccountDetail" maxlength="20"/>
                </td>
                <td><input maxlength="20" type="text" style="width: 100%" v-model="list.creditAccountDetail"/></td>
                <td style="text-align: right">
                  <money style="width: 100%; text-align: right;" v-model="list.amountDetail" v-bind="money"/>
                </td>
                <td>
                  <Autocomplete v-model="list.organizationUnitCodeDetail" :code="index" @sendIdToCashDialog="getDataId" @updateData="updateData"/>
                </td>
                <td><input type="text" class="out-of-range" v-model="list.organizationUnitNameDetail" readonly disabled/></td>
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
          <button class="btn-add-row tooltip tooltip--100" @click="onBtnAddRowClick" v-hotkey="keymap">Thêm dòng <span class="tooltip__text">Ctrl + Insert</span></button>
          <button class="btn-add-row" @click="onBtnDeleteAllRow">Xóa hết dòng</button>
        </div>
        <div class="upload tooltip tooltip--position30-30">
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
          <span class="tooltip__text">Chức năng đang phát triển</span>
        </div>
      </div>
    </div>
    <div class="cashbox__footer">
      <button class="btn-common" @click="onBtnCloseClick">Hủy</button>
      <div class="flex">
        <button class="btn-common" @click="onBtnSaveClick">Cất</button>
        <button class="btn-common btn--success" @click="onBtnSaveClick">Cất và In</button>
      </div>
    </div>
    <span tabindex="30"></span>
    <CashPopup
        v-if="valuePopup"
        @hideCashPopupNotLoad="hideCashPopupNotLoad"
        :message="message"
        @hideCashPopupAndRemoveRow="hideCashPopupAndRemoveRow"
        :formMode="formMode"
        :changeData="changeData"
        @hideCashPopupAndHideDialog="hideCashPopupAndHideDialog"
        @hideCashPopupAndValidate="hideCashPopupAndValidate"
        @saveChangeData="onBtnSaveClick"
      />
  </div>
</template>
<script>
import $ from 'jquery'
import Autocomplete from "../common/Autocomplete.vue";
import {Money} from 'v-money'
import CashPopup from '../common/CashPopup.vue'
import DatePicker from '../common/DatePicker.vue'

import { MES_ADD_SUCCESS, MES_EDIT_SUCCESS, STR_DATA_CHANGE, MES_ERROR_SERVER, MES_REQUIRED_ATTRIBUTE, STR_EMPTY_RECEIPTPAYMENT_CODE, STR_EMPTY_EMPLOYEEID, STR_EMPTY_OBJECT, STR_EMPTY_RECEIVER, STR_EMPTY_ACCOUNTINGDATE, STR_EMPTY_REFDATE, REF_CODE, RECEIPTPAYMENT_CODE_EXIST, STR_EMPTY_DEBTACCOUNT
} from "../../../lang/validation.js";

import {STR_CASHDIALOG, DELETEALLROW, STR_REASONNAME, CHANGEDATA, EMPTYDATA, EXISTDATA, FORMMODE_EDIT, FORMMODE_ADD} from "../../../lang/masterDetail.js"

export default {
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
      //#region Dữ liệu thao tác với Object Đối tượng (Autocomplete)
      saveValueEmployeeName: null,  // Lưu tên nhân viên
      saveValueObject: null,        // Tên đối tượng
      currentIndex: 0,              // Vị trí khi click up down Object
      currentIndexE: 0,             // Vị trí khi click up down Employee
      toggleObject: true,           // Hiển thị đối tượng
      toggleEmployee: true,         // Hiển thị nhân viên
      employees: [],                // mảng nhân viên dùng chung cho cả đối tượng.
      //#endregion
      fakeObjects: [],
      fakeEmployees: [],
      //#region message lỗi Validate
      messageCode: '',
      messageFullName: '',
      messageObject: '',
      messageReceiver: '',
      messageAccountingDate: '',
      messageRefDate: '',
      //#endregion
      firstFocus: false,
      //#region Phát hiện sự thay đối dữ liệu khi click đóng form CashDialog
      changeData: '',           // Biến check changeData: trống, trùng, tồn tại
      detectChangeCash: {},     // Mảng sao chép Object Cash
      detectChangeDetail: '',   // Mảng sao chép listDetail
      //#endregion

      valueRefCode: null, // Giá trị RefCode bindding lên phiếu chi đầu trang
      formMode: '',       // Giá trị CashDialog hay CashList
      valuePopup: false,  // Giá trị hiển thị CashPopup
      message: '',        // Lưu trữ giá trị message lỗi bindding qua CashPopup
      rowIndex: null,     // Lưu độ dài mảng listDetail phần thêm và xóa dòng
      listDetail: [],     // Mảng chưa chi tiết Phiếu chi
      money: {            // Format tiền bằng v-money
        decimal: ',',
        thousands: '.',
        precision: 0,
        masked: false
      },
    };
  },
  created(){
    window.addEventListener("click", (e) => {
      if (e.target.className != 'icon icon-30 arrow-dropdown tranform' && e.target.className != 'icon icon-30 arrow-dropdown' && e.target.className != 'input-select') {
        this.toggleObject = true;
        this.toggleEmployee = true;
      }
    });
  },

  methods: {
    // Show thông báo
    showNotification(message) {
      this.$notification["success"]({
          message,
          duration: 2,
      });
    },

    // Nhập xong số chứng từ nhấn enter focus vào ô đầu của listDetail
    focusDetail(){
      this.$refs.focusDescriptionDetail[0].focus();
    },
    /**
     * Cập nhật dữ liệu Cash lấy từ Autocomplete dưới bảng detail
     * CreatedBY: NXCHIEN 09/06/2021
     */
    getDataId(code, index, name){
      this.listDetail[index].organizationUnitNameDetail = name;
      this.listDetail[index].organizationUnitCodeDetail = code;
    },
    updateData(value, index){
      this.listDetail[index].organizationUnitCodeDetail = value;
    },

    //#region Dữ liệu thao tác với Autocomplete Object và employee
    /**
     * Dropdown Click của Nhân viên và Đối tượng
     * CreateBy: NXCHIEN 09/06/2021
     */
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

    /**
     * focus vào Nhân viên hoặc Đối tượng thì xổ ra danh sách
     * CreateBy: NXCHIEN 09/06/2021
     */
    focusInputKey(value) {
      if (value === 'object') {
        this.toggleObject = false;
      }
      if (value === 'employee') {
        this.toggleEmployee = false;
      }     
    },

    /**
     * Click nút up cập nhật vị trí Index
     * CreatedBY:NXCHIEN 09/06/2021
     */
    up(value) {
      if (value === 'object') {
        if (this.currentIndex > 0) this.currentIndex--;
      }
      if (value === 'employee') {
        if (this.currentIndexE > 0) this.currentIndexE--;
      }
    },

    /**
     * Click nút down cập nhật vị trí currentIndex và kiểm tra trạng thái hiển thị phòng ban
     * CreatedBY:NXCHIEN 09/06/2021
     */
    down(value) {     
      if (value === 'object') {
        if (this.toggleObject) {
          this.toggleObject = false;
          this.currentIndex = 0;
        }
        if (this.currentIndex < this.fakeEmployees.length - 1) {
          this.currentIndex++;
        }   
      }
      if (value === 'employee') {
        if (this.toggleEmployee) {
          this.toggleEmployee = false;
          this.currentIndexE = 0;
        }
        if (this.currentIndexE < this.fakeEmployees.length - 1) this.currentIndexE++;
      }
    },

    /**
     * Click enter cập nhật các giá trị để bind lên Input
     * CreatedBy: NXCHIEN 19/05/2021
     */
    enter(value) {
      if (value === 'object') {
        this.saveValueObject = this.fakeObjects[this.currentIndex].fullName;
        this.cash.receiver = this.saveValueObject;
        this.cash.organizationUnitAddress = this.fakeObjects[this.currentIndex].address;
        this.cash.organizationUnitName = this.saveValueObject;
        this.toggleObject = true;
        this.messageObject = "";
      }
      if (value === 'employee') {
        this.saveValueEmployeeName = this.fakeEmployees[this.currentIndexE].fullName;
        this.cash.employeeId = this.fakeEmployees[this.currentIndexE].employeeId;
        this.cash.fullName = this.saveValueEmployeeName;
        this.toggleEmployee = true;
        this.messageFullName = "";
      }
    },
    
    /**
     * Click dòng trong danh sách nhân viên lấy ra nhân viên
     * CreatedBY: NXCHIEN 09/06/2021
     */
    onBtnEmployeeClick(employee, index, value) {
      if (value === 'object') {
        this.saveValueObject = employee.fullName;
        this.cash.receiver = this.saveValueObject;
        this.cash.organizationUnitAddress = employee.address;
        this.cash.organizationUnitName = this.saveValueObject;
        this.toggleObject = true;
        this.messageObject = "";
        this.currentIndex = index;
      }
      if (value === 'employee') {
        this.saveValueEmployeeName = employee.fullName;
        this.cash.employeeId = employee.employeeId;
        this.cash.fullName = this.saveValueEmployeeName;
        this.toggleEmployee = true;
        this.messageFullName = "";
        this.currentIndexE = index;
      }
    },
    //#endregion

    //#region Check OnchangeInput hiển thị lỗi khi nhập sai
    /**
     * Cập nhật giá trị refCode khi thay đổi input refCode
     * CreatedBY: NXCHIEN 06/06/2021
     */
    onChangeRefCode(e){
      let val = e.target.value;
      this.valueRefCode = val;
      if (val !== "") {
        this.messageCode = "";
      } else if (val == "") {
        this.messageCode = MES_REQUIRED_ATTRIBUTE;
      }
    },

    /**
     * InputObject rỗng thì hiển thị lỗi
     * CreateBy: NXCHIEN 09/06/2021
     */
    onChangeInputObject(e){
      let val = e.target.value;
      this.toggleObject = false;
      this.cash.organizationUnitName = val;
      this.currentIndex = 0;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        this.fakeObjects = this.employees.filter(item => {
        return item.fullName.toLowerCase().indexOf(this.saveValueObject.toLowerCase()) !== -1;
        })
      }, 300);
      if (val !== "") {
        this.messageObject = "";       
      } else if (val == "") {
        this.messageObject = MES_REQUIRED_ATTRIBUTE;
      }  
    },

    /**
     * InputEmployee rỗng thì hiển thị lỗi
     * CreateBy: NXCHIEN 09/06/2021
     */
    onChangeInputEmployee(e){
      let val = e.target.value;
      this.toggleEmployee = false;
      this.cash.fullName = val;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        this.fakeEmployees = this.employees.filter(item => {
        return item.fullName.toLowerCase().indexOf(this.saveValueEmployeeName.toLowerCase()) !== -1;
        })
      }, 300);     
      if (val !== "") {
        this.messageFullName = "";       
      } else if (val == "") {
        this.messageFullName = MES_REQUIRED_ATTRIBUTE;
      }
    },
    /**
     * InputReceiver rỗng thì hiển thị lỗi
     * CreateBy: NXCHIEN 09/06/2021
     */
    onChangeInputReceiver(e){
      let val = e.target.value;
      if (val !== "") {
        this.messageReceiver = "";
      } else if (val === "") {
        this.messageReceiver = MES_REQUIRED_ATTRIBUTE;
      }
    },
    //validate nhập Tài khoản nợ
    checkValueEmptyDebtAccount(value, index){
      if (!value.trim()) {
        document.getElementsByClassName("debtAccount--Detail")[index].classList.add('input-error');
      }else{
        document.getElementsByClassName("debtAccount--Detail")[index].classList.remove('input-error');
      }
    },
    onInputChangeListDetail(value, index){
      this.checkValueEmptyDebtAccount(value, index);
    },
    onBlurInputListDetail(value, index){
      this.checkValueEmptyDebtAccount(value, index);
    },
    //#endregion

    //#region Cập nhật DatePicker
    /**
     * Cập nhật dữ liệu ngày tháng khi lấy từ component DatePicker
     * CreatedBY: NXCHIEN 06/06/2021
     */
    getAccountingDate(value){
      this.cash.accountingDate = value;
      if (value == '') {
        this.messageAccountingDate = MES_REQUIRED_ATTRIBUTE;
      }else{
        this.messageAccountingDate = "";
      }
    },
    getRefDate(value){
      this.cash.refDate = value;
      if (value == '') {
        this.messageRefDate = MES_REQUIRED_ATTRIBUTE;
      }else{
        this.messageRefDate = "";
      }
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
    async onBtnDeleteRowClick(value){
      await this.listDetail.pop(this.listDetail[value]);
      this.rowIndex = this.listDetail.length;
      if(this.rowIndex != 0){
        this.$refs.focusDescriptionDetail[this.rowIndex - 1].select();
      }
    },
    // Thêm 1 dòng trong bảng listDetail
    async onBtnAddRowClick(){
      if (this.listDetail.length == 0) {
        let arrDetailAdd = [{"descriptionDetail": ""}];
        this.listDetail = arrDetailAdd;
        await this.listDetail;
        this.$refs.focusDescriptionDetail[0].focus();
        this.rowIndex = this.listDetail.length;
      }else{
        this.rowIndex += 1;
        await this.listDetail.push(JSON.parse(JSON.stringify(this.listDetail[this.rowIndex - 2])));
        this.$refs.focusDescriptionDetail[this.rowIndex - 1].select();
      }
    },

    //#endregion
    
    //#region Đóng Cashdialog
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
    //#endregion

    //so sánh 2 object by object
    compareObjectCash(obj1, obj2) {
      for (let key in obj2) {
        if (obj2[key] !== obj1[key]) {
          return true;
        }
      }
      return false;
    },

    //#region Validate Click Save
    /**
     * Kiểm tra empty các trường
     * CreatedBy: NXCHIEN 09/06/2021
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
      if (this.cash.accountingDate.trim() == "") {
        this.messageAccountingDate = MES_REQUIRED_ATTRIBUTE;
      }         
      if (this.cash.refDate.trim() == "") {
        this.messageRefDate = MES_REQUIRED_ATTRIBUTE;
      }         
      if (this.cash.employeeId == "") {
        this.messageFullName = MES_REQUIRED_ATTRIBUTE;
      }    
    },
    /**
     * Kiểm tra các trường bắt buộc nhập có check listDetail
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
      if (this.cash.accountingDate.trim() == "") {    
        this.message = STR_EMPTY_ACCOUNTINGDATE;
        this.changeData = EMPTYDATA;
        return true;
      }
      if (this.cash.refDate.trim() == "") {    
        this.message = STR_EMPTY_REFDATE;
        this.changeData = EMPTYDATA;
        return true;
      }
      if (this.cash.employeeId == "") {
        this.message = STR_EMPTY_EMPLOYEEID;
        this.changeData = EMPTYDATA;
        return true;
      }
      for (let i = 0; i < this.listDetail.length; i++) {
        if (this.listDetail[i].debtAccountDetail.trim() == '') {
          document.getElementsByClassName("debtAccount--Detail")[i].classList.add('input-error');
          this.message = STR_EMPTY_DEBTACCOUNT; 
          this.changeData = EMPTYDATA;
          return true;
        }
      }
      return false;
    },
    //#endregion
 
    //#region Click nút Cất và 'Cất và in'
    // chuyển mảng sang JSON
    convertListDetailtoJSON(){
      this.cash.receiptPaymentDetail = JSON.stringify(this.listDetail);
      this.cash.totalAmount = this.totalMoney;
      this.cash.reasonName = STR_REASONNAME;
    },
    onBtnSaveClick() {
      // Kiểm tra attribute empty
      this.checkEmptyAttribute();
      // Kiểm tra validate attribute
      this.valuePopup = this.isCheckValidate();
      if (!this.valuePopup)
        this.onFormModeAddSave();
        this.onFormModeEditSave();
    },
    // Check formMode Add
    onFormModeAddSave(){
      if (this.flag == FORMMODE_ADD) {
        this.convertListDetailtoJSON();
        this.axios.post("/ReceiptPayments", this.cash).then((res) => {            
          this.isCheckCodeResponse(res, MES_ADD_SUCCESS);
        })
        .catch((res) => {
            this.getResponseError(res);
        });
      }
    },
    // Check formMode Edit
    onFormModeEditSave(){
      if (this.flag == FORMMODE_EDIT) {
        this.convertListDetailtoJSON();
        this.axios.put("/ReceiptPayments/" + this.cash.receiptPaymentId, this.cash).then((res) => {
          this.isCheckCodeResponse(res, MES_EDIT_SUCCESS);
        })
        .catch((res) => {
          this.getResponseError(res);
        });
      }
    },
    // Check response trả về
    isCheckCodeResponse(res, message){
      if (res.data.code == 200) {
        this.showNotification(message);
        this.$emit("hideCashDialog");
      } else if (res.data.code == 400) { 
        this.message = res.data.data;
        this.valuePopup = true;
        this.changeData = EXISTDATA;
      }
    },
    // Lấy responseError từ server
    getResponseError(res){
      if(res != undefined){
        if(res.data.code == 500){
          this.message = MES_ERROR_SERVER;
          this.valuePopup = true;
        }
      }
    },
    
    //#endregion

    /**
     * ĐÓng CashPopup và kiểm tra message lỗi để focus
     * CreatedBy: NXCHIEN 10/06/2021
     */
    hideCashPopupAndValidate(){
      this.valuePopup = false;
      this.checkRefCodeEmpty();
      this.checkObjectEmpty();
      this.checkReceiverEmpty();
      this.checkAccountingDateEmpty();
      this.checkRefDateEmpty();
      this.checkEmployeeEmpty();
      if (!this.messageCode && !this.messageObject && !this.messageReceiver && 
      !this.messageAccountingDate && !this.messageRefDate && !this.messageFullName) {
        for (let i = 0; i < this.listDetail.length; i++) {
          if (this.listDetail[i].debtAccountDetail == '') {
            this.$refs.focusDebt[i].focus();
            break;
          }
        }
      }

      if(this.message.includes(REF_CODE)){
        this.messageCode = RECEIPTPAYMENT_CODE_EXIST;
        this.$refs.focusRefCode.focus();
      }
    },
    //#region Validate Check Empty Attribute
    checkRefCodeEmpty(){
      if (this.messageCode != '') {
        this.messageObject = '';
        this.messageReceiver = '';
        this.messageAccountingDate = '';
        this.messageRefDate= '';
        this.messageFullName = '';     
        this.$refs.focusRefCode.focus();
      }
    },
    checkObjectEmpty(){
      if(this.messageObject != ''){
        this.messageReceiver = '';
        this.messageAccountingDate = '';
        this.messageRefDate= '';
        this.messageFullName = '';
        this.$refs.focusInputObject.focus();
      }
    },
    checkReceiverEmpty(){
      if(this.messageReceiver != ''){
        this.messageAccountingDate = '';
        this.messageRefDate= '';
        this.messageFullName = '';
        this.$refs.focusReceiver.focus();
      }
    }, 
    checkAccountingDateEmpty(){
      if(this.messageAccountingDate != ''){
        this.messageRefDate= '';
        this.messageFullName = '';
        this.$refs.accountingDate.$refs.reference.$refs.accountingDate.focus();        
      }
    },
    checkRefDateEmpty(){
      if(this.messageRefDate != ''){
        this.messageFullName = '';
        this.$refs.refDate.$refs.reference.$refs.refDate.focus();
      }
    },
    checkEmployeeEmpty(){
      if(this.messageFullName != ''){
        this.$refs.focusInputEmployee.focus();
        return;
      }
    }
    //#endregion
  
  },
  computed:{
    // Phím tắt
    keymap() {
      return {       
        "Ctrl + Insert": this.onBtnAddRowClick,
        "Ctrl + S": this.onBtnSaveClick,
        esc: this.onBtnCloseClick,
      };
    },
    /**
     * Tính tổng tiền trong listDetail
     * CreatedBy: NXCHIEN 06/06/2021
     */
    totalMoney(){
      var total = 0;
      for ( var i = 0, _len = this.listDetail.length; i < _len; i++ ) {
        total += this.listDetail[i].amountDetail;
      }
      if (!total) {
        return 0;
      }
      return total;    
    },
  },

  mounted() {
    clearTimeout(this.timeOut);
    this.timeOut = setTimeout(() => {
      $('.v-money').attr("maxlength", 15);        /// Add maxlength cho số tiền trong detail
    }, 100)

    this.$refs.focusInputObject.focus();
    this.toggleObject = true;

    this.listDetail = JSON.parse(this.cash.receiptPaymentDetail);     // Khởi tạo giá trị listDetail
    this.rowIndex = this.listDetail.length;                           // Khởi tạo rowIndex
    this.valueRefCode = this.cash.receiptPaymentCode;                 // Khởi tạo giá trị Phiếu chi góc trái trên màn hình
    // Giá trị autoComplete-----------
    this.saveValueObject = this.cash.organizationUnitName;
    this.saveValueEmployeeName = this.cash.fullName;
    //---------------------------------
    // Check thay đổi dữ liệu.
    this.detectChangeCash = {...this.cash};                           // Sao chép object để so sánh
    this.detectChangeDetail = this.cash.receiptPaymentDetail;         // sao chép mảng để so sánh
    //-----------------------------------
    this.axios.get(`/Employees`).then((response) => {                 // Khởi tạo mảng nhân viên và fakeData dùng cho autocomplete
      this.employees = response.data.data;
      this.fakeEmployees = [...this.employees]
      this.fakeObjects = [...this.employees]
    }).catch(() => {});
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
.row__input__first{
  width: 75%;
  display: flex;
}
.object {
  width: 44%;
}
.receive {
  padding: 0 0px 0 12px;
  width: 55%;
}
.date__form {
  width: 23%;
  padding: 0 0 12px 16px;
  border-left: 1px solid #d4d7dc;
}
.address {
  width: 75%;
}
.employee {
  width: 34%;
  padding-right: 14px;
}
.attach {
  width: 16%;
}
.receive .input--size {
  width: calc(97% + 5px);
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
  width: 170%;
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
.select-custom {
  position: absolute;
  overflow-y: auto;
  min-width: 200px;
  max-height: 160px;
  background-color: white;
  border: 1px solid #bbb;
}
.object .select-custom {
  width: calc(53% + 13px);
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
.invisibleObject {
  display: none;
}
.invisibleEmployee {
  display: none;
}
.color {
  background-color: #2ca01c;
  color: white;
}
.visible{
  display: flex;
}
.span{
  color: red; 
  font-size: 12px;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
}
.tranform {
  transform: rotate(180deg);
  transition: transform 0.15s linear;
}
.input-error{
  border: 1px solid red;
}
.out-of-range{
  width: 100%; 
  cursor: not-allowed; 
  background: rgb(224 224 224);
}
.out-of-range:focus{
  border: 1px solid #babec5;
}
.close--hover:hover{
  background-position: -146px -145px;
}
</style>