<template>
  <div class="dialog">
    <div class="model"></div>
    <div class="dialog-box">
      <div class="dialog-header">
        <div class="title flex">
          <div class="title-dialog">Thông tin nhân viên</div>
          <div class="component">
            <input type="checkbox" style="padding: 0" />
            <div style="padding-left: 10px">Là khách hàng</div>
          </div>
          <div class="component">
            <input type="checkbox" style="padding: 0" />
            <div style="padding-left: 10px">Là nhà cung cấp</div>
          </div>
        </div>
      </div>
      <div class="item-close">
        <div class="icon icon-24 help" style="margin-right: 6px"></div>
        <div
          class="icon icon-24 close tooltip tooltip--position25-10"
          v-hotkey="keymap"
          @click="onBtnCloseClick"
        >
        <span class="tooltip__text">ESC</span>
        </div>
      </div>
      <div class="dialog-content">
        <div class="employee-inf">
          <div class="inf-left">
            <div class="row-1">
              <div class="code">
                <span class="text"
                  >Mã
                  <p style="color: red; display: inline">*</p></span
                >
                <input
                  maxlength="20"
                  :title="(messageCode != '') ? 'Mã nhân viên không được để trống!' : ''"
                  type="text"
                  ref="focusCode"
                  style="width: 151px; margin-top: 4px"
                  v-model="employee.employeeCode"
                  @input="onChangeInputCode"
                  :class="{ 'input-error': messageCode != ''}"                  
                />         
                <span style="color: red; font-size: 12px">{{messageCode}}</span>
              </div>
              <div class="name">
                <span class="text"
                  >Tên
                  <p style="color: red; display: inline">*</p></span
                >
                <input
                  :title="(messageName != '') ? 'Tên không được để trống!' : ''"
                  type="text"
                  ref="focusName"
                  style="width: 235px; margin-top: 4px"
                  v-model="employee.fullName"
                  @input="onChangeInputName"
                  :class="{ 'input-error': messageName != ''}"
                />
                <span style="color: red; font-size: 12px">{{messageName}}</span>
              </div>
            </div>
            <div class="row-1">
              <div class="position">
                <span class="text"
                  >Đơn vị
                  <p style="color: red; display: inline">*</p></span
                >
                <div
                  :title="(messageDepartment != '') ? 'Đơn vị không được để trống!' : '' "
                  class="department-box"
                  style="margin-top: 4px;"
                  :class="{ 'input-error': messageDepartment != ''}"
                >
                  <div class="selected-option">
                    <input
                      type="text"
                      ref="focusDepartment"
                      class="input-select"
                      v-model="saveValueDepartment"
                      @input="onChangeInputDepartment"
                      @keydown.up="up"
                      @keydown.down="down"
                      @keydown.enter="enter"
                      @focus="focusInputKey"
                    />
                    
                    <div class="icon-selected">
                      <div
                        class="icon icon-30 arrow-dropdown"
                        @click="onBtnDropdownClick"
                      ></div>
                    </div>
                  </div>
                </div>
                <span style="color: red; font-size: 12px">{{messageDepartment}}</span>            
                <div
                  class="select-custom"
                  :class="{ invisible: showDepartment }"
                >
                  <div class="header-select">
                    <div class="text">Mã đơn vị</div>
                    <div class="text" style="margin-left: 79px;">
                      Tên đơn vị
                    </div>
                  </div>
                  <div
                    class="department-content"
                    ref="positionDepartment"
                    v-for="(department, index) in fakeDepartments"
                    :key="index"
                    @click="onBtnDepartmentClick(department, index)"
                    :class="{ color: currentIndex == index }"
                  >
                    <div class="item">
                      <div>{{ department.departmentCode }}</div>
                      <div style="margin-left: 100px;">
                        {{ department.departmentName }}
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="row-1">
              <div class="position">
                <span class="text">Chức danh</span>
                <input
                  type="text"
                  style="width: 392px; margin-top: 4px"
                  v-model="employee.positionName"
                />
              </div>
            </div>
          </div>
          <div class="inf-left">
            <div class="row-1">
              <div class="dateofbirth">
                <span class="text">Ngày sinh</span>
                <DatePicker v-model="employee.dateOfBirth" :type="'dateOfBirth'" @sendDateOfBirth="getDateOfBirth"/>
              </div>
              <div class="gender">
                <span class="text">Giới tính</span>
                <div class="radio" style="width: 251px; margin-top: 13px">
                  <input
                    id="male"
                    type="radio"
                    name="gender"
                    class="input-radio"
                    value="1"
                    v-model="employee.gender"
                  />
                  <label for="male" style="margin-right: 20px">Nam</label>

                  <input
                    id="female"
                    type="radio"
                    name="gender"
                    class="input-radio"
                    value="0"
                    v-model="employee.gender"
                  />
                  <label for="" style="margin-right: 20px">Nữ</label>

                  <input
                    id="orther"
                    type="radio"
                    name="gender"
                    class="input-radio"
                    value="2"
                    v-model="employee.gender"
                  />
                  <label for="">Khác</label>
                </div>
              </div>
            </div>
            <div class="row-1">
              <div class="id">
                <span class="text">Số CMND</span>
                <input
                  type="text"
                  style="width: 245px; margin-top: 4px"
                  v-model="employee.identifyNumber"
                />
              </div>
              <div class="dateofbirth" style="padding-left: 5px">
                <span class="text">Ngày cấp</span>
                <DatePicker v-model="employee.dateOfIN" :type="'dateOfIN'" @sendDateOfIN="getDateOfIN"/>
              </div>
            </div>
            <div class="row-1">
              <div class="place">
                <span class="text">Nơi cấp</span>
                <input
                  type="text"
                  style="width: 418px; margin-top: 4px"
                  v-model="employee.placeOfIN"
                />
              </div>
            </div>
          </div>
        </div>
        <div class="select-tab">
          <div class="tab-inf">
            <div class="row-1">
              <div class="position" style="margin-top: 10px">
                <span class="text">Địa chỉ</span>
                <input
                  type="text"
                  style="width: 836px; margin-top: 4px"
                  v-model="employee.address"
                />
              </div>
            </div>
            <div class="row-1">
              <div class="phone" style="margin-top: 17px">
                <span class="text">ĐT di động</span>
                <input
                  maxlength="10"
                  ref="focusPhone"
                  type="text"
                  style="width: 197px; margin-top: 4px"
                  @input="onChangeInputPhone"
                  v-model="employee.phoneNumber"
                  :class="{'input-error': messagePhone != ''}"
                />
                <span style="color: red; font-size: 12px">{{messagePhone}}</span>          
              </div>
              <div class="phone" style="margin-top: 17px; margin-left: 5px">
                <span class="text">ĐT cố định</span>
                <input
                  type="text"
                  style="width: 197px; margin-top: 4px"
                  v-model="employee.telephoneNumber"
                />
              </div>
              <div class="name" style="margin-top: 17px; margin-left: 5px">
                <span class="text"
                  >Email
                  <p style="color: red; display: inline">*</p></span
                ><br />
                <input
                  ref="focusEmail"
                  type="text"
                  style="width: 203px; margin-top: 4px"
                  v-model="employee.email"
                  
                  @input="onChangeInputEmail"
                  :class="{'input-error': messageEmail != ''}"/>
                  <br />
                <span style="color: red; font-size: 12px">{{messageEmail}}</span>
              </div>
            </div>
            <div class="row-1">
              <div class="phone" style="margin-top: 17px">
                <span class="text">Tài khoản ngân hàng</span>
                <input
                  type="text"
                  style="width: 197px; margin-top: 4px"
                  v-model="employee.bankAccountNumber"
                />
              </div>
              <div class="phone" style="margin-top: 17px; margin-left: 5px">
                <span class="text">Tên ngân hàng</span>
                <input
                  type="text"
                  style="width: 197px; margin-top: 4px"
                  v-model="employee.bankName"
                />
              </div>
              <div class="name" style="margin-top: 17px; margin-left: 5px">
                <span class="text">Chi nhánh</span><br />
                <input
                  type="text"
                  style="width: 203px; margin-top: 4px"
                  v-model="employee.bankBranchName"
                /><br />
              </div>
            </div>
          </div>
        </div>
        <div class="footer">
          <div class="divide"></div>
          <div class="footer-btn">
            <button
              class="btn-No btn-yes add-line"
              style="width: 67px; height: 36px; boder-radius: 4px"
              @click="onBtnCloseClick"
            >
              Hủy
            </button>
            <div class="btn-right">
              <button
                class="btn-No btn-yes add-line tooltip tooltip--70 tooltip--position25-10"
                style="width: 67px; height: 36px; boder-radius: 4px"
                @click="onBtnSaveClick"
              >
                Cất
                <span class="tooltip__text">Ctrl + S</span>
              </button>
              <button
                class="btn-common btn--success btn-save-add"
                style="margin-left: 13px"
                @click="onBtnSaveAndAddClick"
              >
                Cất và Thêm
              </button>
            </div>
          </div>
        </div>
      </div>
      <Popup
        v-if="valuePopup"
        @hidePopupNotLoad="hidePopupNotLoad"
        :message="message"
        @onClickYesWhenDataChange="onBtnSaveClick"
        @hidePopupAndHideDialog="hidePopupAndHideDialog"
      />
    </div>
  </div>
</template>
<script>
//#region Import
import Popup from "../common/Popup.vue";
import DatePicker from "../common/DatePicker.vue"
import {
  STR_EMPTY_CODE,
  STR_EMPTY_NAME,
  STR_EMPTY_DEPARTMENT,
  STR_DATA_CHANGE,
  MES_ADD_SUCCESS,
  MES_EDIT_SUCCESS,
  STR_EMPTY_EMAIL,
  STR_ERROR_EMAIL,
  MES_REQUIRED_ATTRIBUTE,
  REGEX_EMAIL,
  MES_ERROR_SERVER,
  STR_ERROR_PHONE,
  REGEX_PHONE,
  EMPLOYEE_CODE_EXIST,
  EMPLOYEE_EMAIL_EXIST,
  EMPLOYEE_PHONE_EXIST,
  EMPLOYEE_CODE
} from "../../../lang/validation.js";

//#endregion
export default {
  //#region Khai báo
  components: {
    Popup,
    DatePicker
  },
  props: {
    state: { type: Boolean, selector: false }, // Trạng thái hiển thị Dialog
    employee: { type: Object, default: null }, // Đối tượng nhân viên được truyền từ EmployeeList sang
    flag: { type: String, selector: null }, // Cờ để check giá trị nút Thêm mới hay Sửa
  },
  data() {
    return {
      infor: true, // Giá trị hiển thị tab Liên hệ hay tài khoản ngân hàng
      departments: [], // Mảng phòng ban
      message: "", // message thông báo lỗi bind sang Popup
      valuePopup: false, // Giá trị để hiển thị Popup
      valueForcusInput: false, // Giá trị để focus vào ô input
      showDepartment: true, // Hiển thị comboboxDepartment
      saveValueDepartment: null, // Biến lưu lại giá trị DepartmentId
      dectectEmployee: {}, //TODO: phát hiện sự thay đổi giá trị employee khi click nút X form dialog
      currentIndex: 0, // Vị trí nút bấm up down
      messageEmail: "",   // thông báo lỗi nhập liệu
      messageCode: "",    // thông báo lỗi nhập liệu
      messageName: "",    // thông báo lỗi nhập liệu
      messageDepartment: "",    // thông báo lỗi nhập liệu
      messagePhone: "",
      dateCheck: false,
      fakeDepartments: [],
    };
  },
  //#endregion
  computed: {
  //   /**
  //    * Theo dõi giá trị departmentId
  //    * CreatedBy: NXCHIEN 17/05/2021
  //    */
  //   showValueDepartment: {
  //     get() {
  //       for (let index = 0; index < this.departments.length; index++) {
  //         if (
  //           this.saveValueDepartment == this.departments[index].departmentId
  //         ) {
  //           return this.departments[index].departmentName;
  //         } else if (
  //           this.employee.departmentId == this.departments[index].departmentId
  //         ) {
  //           return this.departments[index].departmentName;
  //         }
  //       }
  //       return "";
  //     },
  //     set(value) {
  //       this.employee.departmentId = value;
  //     },
  //   },
    keymap() {
      return {
        esc: this.onBtnCloseClick,
        "Ctrl + S": this.onBtnSaveClick,
      };
    },
  },
  //#region METHODS
  methods: {
    getDateOfIN(value){
      this.employee.dateOfIN = value;
    },
    getDateOfBirth(value){
      this.employee.dateOfBirth = value;
    },
    hidePopupNotLoad(){
      this.valuePopup = false;
      if(this.messageCode != "" || this.message.includes(EMPLOYEE_CODE)){
        this.$refs.focusCode.focus();
        this.messageDepartment = "";
        this.messageName = "";
        this.messageEmail = "";
        this.messagePhone = "";
        this.messageCode = EMPLOYEE_CODE_EXIST;
      }else if(this.messageName != ""){
        this.$refs.focusName.focus();
        this.messageDepartment = "";
        this.messageEmail = "";  
        this.messagePhone = "";    
      }else if(this.messageDepartment != ""){
        this.$refs.focusDepartment.focus();
        this.messageEmail = "";
        this.messagePhone = "";
      }else if(this.messagePhone != ""){
        this.$refs.focusPhone.focus();
      }else if(this.message.includes(EMPLOYEE_PHONE_EXIST)){
        this.messagePhone = EMPLOYEE_PHONE_EXIST;
        this.messageEmail = "";
        this.$refs.focusPhone.focus();
      }else if(this.message.includes(STR_ERROR_PHONE)){
        this.messagePhone = STR_ERROR_PHONE;
        this.messageEmail = "";
        this.$refs.focusPhone.focus();
      }else if(this.messageEmail != ""){
        this.$refs.focusEmail.focus();        
      }else if(this.messageEmail != "" || this.message.includes(EMPLOYEE_EMAIL_EXIST)){
        this.$refs.focusEmail.focus();
        this.messageEmail = EMPLOYEE_EMAIL_EXIST;
      }
    },
    // SHow thông báo thêm, sửa thành công
    showNotification(message) {
      this.$notification["success"]({
        message,
        duration: 1,
      });
    },
    focusInputKey() {
      this.showDepartment = false;
    },
    //#region Validate
    //Kiểm tra thay đổi Giá trị input
    onChangeInputName(e) {
      let val = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        if (val !== "") {
          this.messageName = "";
        } else if (val == "") {
          this.messageName = MES_REQUIRED_ATTRIBUTE;
        }
      }, 200);
    },
    //Kiểm tra thay đổi Giá trị input
    onChangeInputCode(e) {
      let val = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        if (val !== "") {
          this.messageCode = "";
        } else if (val == "") {
          this.messageCode = MES_REQUIRED_ATTRIBUTE;
        }
      }, 200);
    },
    //Kiểm tra thay đổi Giá trị input
    onChangeInputDepartment(e) {
      let val = e.target.value;
      this.showDepartment = false;
      this.employee.departmentName = val;
      this.currentIndex = 0;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        this.fakeDepartments = this.departments.filter(item => {
        return item.departmentName.toLowerCase().indexOf(this.saveValueDepartment.toLowerCase()) !== -1;
        })
      }, 300);
      if (val !== "") {
        this.messageDepartment = "";
      } else if (val == "") {
        this.messageDepartment = MES_REQUIRED_ATTRIBUTE;
      }
    },

    onChangeInputPhone(e){
      let val = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        if (REGEX_PHONE.test(val)) {
          this.messagePhone = "";
        } else if (val == "") {
          this.messagePhone = "";
          delete this.employee.phoneNumber;
        } else {
          this.messagePhone = STR_ERROR_PHONE;
        }
      }, 500);
    },
    /* 
    Xử lý thay đổi giá trị Input Email
    CreatedBy: NXCHIEN 17/05/2021
    */
    onChangeInputEmail(e) {
      let val = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        if (REGEX_EMAIL.test(val)) {
          this.messageEmail = "";
        } else if (val == "") {
          this.messageEmail = MES_REQUIRED_ATTRIBUTE;
        } else {
          this.messageEmail = STR_ERROR_EMAIL;
        }
      }, 500);
    },
    //#endregion
    /**
     * Click nút up cập nhật vị trí currentIndex
     * CreatedBy:NXCHIEN 19/05/2021
     */
    up() {
      if (this.currentIndex > 0) this.currentIndex--;
    },

    /**
     * Click nút up cập nhật vị trí currentIndex và kiểm tra trạng thái hiển thị phòng ban
     * CreatedBY:NXCHIEN 19/05/2021
     */
    down() {
      if (this.showDepartment) {
        this.showDepartment = false;
        this.currentIndex = 0;
      }
      if (this.currentIndex < this.departments.length - 1) this.currentIndex++;
    },

    /**
     * Click enter cập nhật các giá trị để bind lên input chứa giá trị tên phòng ban/ đơn vị
     * CreatedBy: NXCHIEN 19/05/2021
     */
    enter() {
      this.saveValueDepartment = this.fakeDepartments[
        this.currentIndex
      ].departmentName;
      this.employee.departmentId = this.fakeDepartments[
        this.currentIndex
      ].departmentId;
      this.showDepartment = true;
      this.messageDepartment = "";
    },

    /**
     * Kiểm tra các trường bắt buộc nhập
     * CreatedBy: NXCHIEN 19/05/2021
     */
    isCheckValidate() {
      if (this.employee.employeeCode.trim() == "") {       
        this.message = STR_EMPTY_CODE;
        return true;
      }
      if (this.employee.fullName.trim() == "") {
        this.message = STR_EMPTY_NAME;
        return true;
      }
      if (this.employee.departmentId == "") {
        this.message = STR_EMPTY_DEPARTMENT;
        return true;
      }
      if (this.messagePhone == STR_ERROR_PHONE) {
        this.message = STR_ERROR_PHONE;
        return true;
      }
      if (this.employee.email == "") {
        this.message = STR_EMPTY_EMAIL;
        return true;
      }
      if (this.messageEmail == STR_ERROR_EMAIL) {
        this.message = STR_ERROR_EMAIL;
        return true;
      }
      return false;
    },

    /**
     * Đóng dialog mà không load: gọi từ popup qua Dialog -> EmployeeList
     * CreatedBy:NXCHIEN 17/05/2021
     */
    hidePopupAndHideDialog() {
      this.$emit("hideDialogNotLoad");
    },

    /**
     * So sánh 2 object
     * CreatedBy: NXCHIEn 17/05/2021
     */
    compareObjectEmployee(obj1, obj2) {
      for (let key in obj2) {
        if (obj2[key] !== obj1[key]) {
          return true;
        }
      }
      return false;
    },
    /* 
    Click đóng Dialog
    CreatedBy: NXCHIEN 17/05/2021
    */
    onBtnCloseClick() {
      if (this.compareObjectEmployee(this.dectectEmployee, this.employee)) {
        this.message = STR_DATA_CHANGE;
        this.valuePopup = true;
      } else {
        this.$emit("hideDialogNotLoad");
        this.saveValueDepartment = null;
      }
    },

    /**
     * Kiểm tra empty các trường code, name, departmentId
     * CreatedBy:NXCHIEN 19/05/2021
     */
    checkEmptyAttribute() {
      if (this.employee.employeeCode == "") {
        this.messageCode = MES_REQUIRED_ATTRIBUTE;
      }
      if (this.employee.fullName == "") {
        this.messageName = MES_REQUIRED_ATTRIBUTE;
      }
      if (this.employee.departmentId == "") {
        this.messageDepartment = MES_REQUIRED_ATTRIBUTE;
      }
      if (this.employee.email == "") {
        this.messageEmail = MES_REQUIRED_ATTRIBUTE;
      }     
    },

    /**
     * Lấy data lỗi trả về từ server
     * CreatedBY: NXCHIEN 30/05/2021
     */
    getResponseError(res){
      if(res != undefined){
        if(res.data.code == 500){
          this.message = MES_ERROR_SERVER;
          // show popup
          this.valuePopup = true;
        }
      }
    },

    /**
     * Hàm dùng chung nút 'Cất' và Nút 'Cất và Thêm'
     * CreatedBY: NXCHIEN 17/05/2021
     */
    validAndSave() {
      // Kiểm tra attribute empty
      this.checkEmptyAttribute();
      // Kiểm tra validate attribute
      this.valuePopup = this.isCheckValidate();
      if (!this.valuePopup)
        if (this.flag == "add") {
          this.employee.gender = parseInt(this.employee.gender);
          return this.axios.post("/Employees", this.employee)
            .then((res) => {            
              if (res.data.code == 200) {
                this.saveValueDepartment = null;
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
        }// Kiểm tra nút Thêm hay Sửa       
        else if (this.flag == "edit") {
          this.employee.gender = parseInt(this.employee.gender);
          return this.axios.put("/Employees/" + this.employee.employeeId, this.employee)
            .then((res) => {
              if (res.data.code == 200) {
                this.saveValueDepartment = null;
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
        this.$emit("hideDialog");
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
      this.$refs.focusCode.focus();
      
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() =>{
        this.dectectEmployee = {};
        this.dectectEmployee = { ...this.employee };    
      }, 1000)
    },

    /**
     * Click nút dropdown để toggle Department phòng ban
     * CreatedBy: NXCHIEN 17/05/2021
     */
    onBtnDropdownClick() {
      this.showDepartment = !this.showDepartment;
      if (!this.showDepartment) {
        this.$refs.focusDepartment.focus();
      }
    },

    /**
     * Click chọn phòng ban để bind dữ liệu
     * CreatedBy: NXCHIEN 17/05/2021
     */
    onBtnDepartmentClick(department, index) {
      // Gán ID phòng ban của employee = id lấy được để theo dõi giá trị và bind lên form
      this.employee.departmentId = department.departmentId;
      this.saveValueDepartment = department.departmentName;
      this.employee.departmentName = department.departmentName;
      // Ẩn combobox Department phòng ban
      this.showDepartment = true;
      // Ẩn border error
      this.messageDepartment = "";
      this.currentIndex = index;
    },
  },
  //#endregion

  mounted() {
    /**
     * Lấy ra danh sách các phòng ban rồi bind vào ô Select Department
     * và focus vào ô Input
     *CreatedBy: NXCHIEN 17/05/2021
     */
    this.$refs.focusCode.focus();
    this.saveValueDepartment = this.employee.departmentName;
    this.axios
      .get("/Departments")
      .then((res) => {
        this.departments = res.data.data;
        this.fakeDepartments = [...this.departments];
      })
      .catch((res) => {
        console.log(res);
      });
    this.dectectEmployee = { ...this.employee };
  },
};
</script>
<style scoped>
.flex {
  display: flex !important;
}
.dialog-hide {
  display: none;
}
.dialog {
  border: 1px solid #bbb;
  /* animation: zoomIn 0.5s ; */
}

.dialog .model {
  width: 100%;
  background: rgba(0, 0, 0, 0.4);
  position: fixed;
  left: 0;
  top: 0;
  height: 100%;
  z-index: 103;
  transition: all 0.25s ease;
  opacity: 1;
}

.dialog .dialog-box {
  z-index: 104;
  border-radius: 4px;
  width: 900px;
  height: 625px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: white;
}

.dialog-box .dialog-header {
  padding: 20px 12px 20px 32px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 90%;
}
.dialog-header .title {
}
.title-dialog {
  font-size: 24px;
  color: #111111;
}
.component {
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0 20px;
}
.item-close {
  position: absolute;
  top: 0;
  right: 0;
  height: 70.4px;
  width: 78px;
  display: flex;
  padding: 12px;
}

.dialog-content {
  transition: all 0.23s ease 0.1s;
  flex-direction: column;
  display: flex;
  /* overflow: auto;
  overflow-y: visible; */
  padding: 0 32px 20px;
}
.employee-inf {
  width: 100%;
  height: 213px;
  display: flex;
  align-items: center;
}
.inf-left {
  padding-bottom: 12px;
  width: 50%;
  height: 100%;
  padding: 0 26px 0 0;
}
.row-1 {
  width: 100%;
  height: 70px;
  display: flex;
}
.code {
  width: 160px;
  height: 70px;
}
.name {
  width: 235px;
  height: 70px;
}
.phone {
  width: 197px;
  height: 70px;
}
.position {
  width: 392px;
  height: 70px;
}
.place {
  width: 418px;
  height: 70px;
}
.text {
  font-weight: 700;
  color: #111111;
  font-size: 12px;
}
.dateofbirth {
  width: 170px;
  height: 70px;
}
.gender {
  padding-left: 10px;
  width: 251px;
  height: 70px;
}
.radio {
  display: flex;
  align-items: center;
}
.input-radio {
  margin-right: 10px;
  border: none;
  outline: none;
  border: 1px solid #babec5;
}

.select-tab {
  height: 226px;
  width: 100%;
}
.tab {
  height: 32px;
  width: 100%;
  display: flex;
}
.tab-inf {
  height: 192px;
  width: 834px;
}
.inf {
  width: 64px;
  height: 100%;
  padding: 0 8px;
  font-size: 13px;
  color: #111111;
  border: none;
  border-top: 1px solid #c9ccd2;
  border-right: 1px solid #c9ccd2;
  border-left: 1px solid #c9ccd2;
  outline: none;
  background-color: white;
  font-family: GoogleSans-Regular;
}
.inf:hover {
  color: #019160;
}
.inf:focus {
  background-color: rgb(205, 240, 236);
}
.bank {
  padding: 0 8px;
  height: 100%;
  width: 148px;
  margin-left: 2px;
  border: none;
  border-top: 1px solid #c9ccd2;
  border-left: 1px solid #c9ccd2;
  border-right: 1px solid #c9ccd2;
  outline: none;
  background-color: white;
  font-family: GoogleSans-Regular;
}
.bank:hover {
  color: #019160;
}
.bank:focus {
  background-color: rgb(205, 240, 236);
}
.content-tblEmployee {
  height: 145px;
  overflow-y: auto;
}
.delete {
  background-position: -464px -312px;
}

.footer {
  height: 89px;
  width: 836px;
}
.divide {
  width: 100%;
  border-top: 1px solid #e0e0e0;
  margin: 32px 0 20px;
  position: relative;
}
.footer-btn {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.color {
  height: 36px;
  color: white;
  background-color: #35bf22;
  /* border-radius: 4px; */
  border: none;
  outline: none;
}
.color:hover {
  background-color: #3ff128;
}

.label-input {
  height: 21px;
  width: 21px;
  border-radius: 50%;
  border: 1px solid #3ff128;
  background-color: white;
}
.label-input:checked {
  /* content: ""; */
  height: 13px;
  width: 13px;
  border: none;
  background-color: #3ff128;
  position: absolute;
  border-radius: 50%;
  margin-left: 3px;
  margin-top: 3px;
}
.absolute {
  position: absolute;
  top: 91px;
  right: 22px;
  display: flex;
  width: 251px;
  height: 32px;
}
.department-box {
  width: 392px;
  height: 32px;
  display: flex;
  min-height: 32px;
  border: 1px solid #babec5;
  border-radius: 2px;
  background-color: #fff;
  outline: none;
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

.input-select {
  background-color: transparent;
  display: flex;
  padding: 5px 0 5px 10px;
  height: 32px;
  width: 360px;
  border: none;
  /* border: 1px solid #babec5; */
}
.selected-option {
  display: flex;
  align-items: center;
  justify-content: space-between;
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
.box-error {
  border-color: red;
}
.box-error:hover {
  border-color: red;
}
.select-custom {
  position: absolute;
  width: 392px;
  /* height: 194px; */
  top: 205px;
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
.invisible {
  display: none;
}
.color {
  background-color: #2ca01c;
  color: white;
}
.input-error {
  border: 1px solid red;
}
.input-overide {
  border: 1px solid #babec5;
}

/* @keyframes zoomIn {
    0% {
        transform: scale(0.5,0.5);
    }
    100% {
        transform: scale(1,1);
    }
} */
/* .zoomIn{
  animation-name: zoomIn;
  animation-duration: 0.5s;
} */
.style-input-date-lib {
  font-size: 13px;
  border: none;
  border-radius: 2px;
  height: 32px;
  color: #000000;
  padding: 6px 10px;
  border: 1px solid #babec5;
  font-family: NotoSans-Regular;
  outline: none;
}
input .search {
  width: 392px !important;
}
</style>
