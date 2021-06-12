<template>
  <div>
    <div class="department-box" :class="{'input-focus': toggleAutocomplete == false}">
      <div class="selected-option">
        <input
          type="text"
          ref="Autocomplete"
          class="input-select"
          @keydown.up="up"
          @keydown.down="down"
          @keydown.enter="enter"
          v-model="saveValueEmployeeCode"
          @focus="focusInputKey"
          @input="handleInput"
        />
        <div class="icon-selected">
          <div
            class="icon icon-30 arrow-dropdown tranform-again"
            :class="{ tranform: toggleAutocomplete == false }"
            @click="onBtnDropdownClick"
          ></div>
        </div>
      </div>
    </div>
    <div class="select-custom" :class="{ invisible: toggleAutocomplete }">
      <div class="header-select">
        <div class="text code--size text-hidden">Đối tượng</div>
        <div class="text name--size text-hidden">Tên đối tượng</div>
        <div class="text department--size text-hidden">Đơn vị</div>
        <div class="text phone--size text-hidden">Số điện thoại</div>
      </div>
      <div class="department-content" ref="positionDepartment" 
        v-for="(employee, index) in fakeEmployees"
        :key="index"
        @click="onBtnEmployeeClick(employee ,index)"
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
  </div>
</template>
<script>
export default {
  props: ['value', 'code'],
  data() {
    return {
      employees: [],
      fakeEmployees: [],
      toggleAutocomplete: true,
      currentIndex: 0,
      saveValueEmployeeName: '',
      saveValueEmployeeCode: null,
    };
  },
  methods: {
    handleInput(e){
      let val = e.target.value;
      this.toggleAutocomplete = false;
      this.$emit('updateData', val, this.code);
      this.currentIndex = 0;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        this.fakeEmployees = this.employees.filter(item => {
        return item.fullName.toLowerCase().indexOf(this.saveValueEmployeeCode.toLowerCase()) !== -1;
        })
      }, 300);
    },
    
    onBtnDropdownClick() {
      this.toggleAutocomplete = !this.toggleAutocomplete;
      if (!this.toggleAutocomplete) {
        this.$refs.Autocomplete.focus();
      }
    },

    focusInputKey() {
      this.toggleAutocomplete = false;      
    },
    /**
     * Click nút up cập nhật vị trí currentIndex
     * CreatedBy:NXCHIEN 19/05/2021
     */
    up() {
      if (this.currentIndex > 0) this.currentIndex--;
    },

    /**
     * Click nút down cập nhật vị trí currentIndex
     * CreatedBY:NXCHIEN 19/05/2021
     */
    down() {
      if (this.toggleAutocomplete) {
        this.toggleAutocomplete = false;
        this.currentIndex = 0;
      }
      if (this.currentIndex < this.fakeEmployees.length - 1) this.currentIndex++;
    },

    /**
     * Click enter cập nhật các giá trị để bind lên input chứa giá trị tên phòng ban/ đơn vị
     * CreatedBy: NXCHIEN 19/05/2021
     */
    enter() {
      this.saveValueEmployeeName = this.fakeEmployees[this.currentIndex].fullName;
      this.saveValueEmployeeCode = this.fakeEmployees[this.currentIndex].employeeCode;
      this.toggleAutocomplete = true;
      if (this.code != undefined) {
        this.$emit("sendIdToCashDialog", this.saveValueEmployeeCode, this.code, this.saveValueEmployeeName);
      }
    },
    
    /**
     * Click chọn nhân viên trong dropdown
     * CreatedBY: NXCHIEN 09/06/2021
     */
    onBtnEmployeeClick(employee, index) { 
      this.saveValueEmployeeName = employee.fullName;
      this.saveValueEmployeeCode = employee.employeeCode;
      this.toggleAutocomplete = true;
      this.currentIndex = index;
      if (this.code != undefined) {
        this.$emit("sendIdToCashDialog", this.saveValueEmployeeCode, this.code, this.saveValueEmployeeName);       
      }
    },
  },

  created(){
    // Phát hiện click outside element
    window.addEventListener("click", (e) => {
      if (!this.$el.contains(e.target)) {
        this.toggleAutocomplete = true;
      }
    });
  },
  
  mounted() {
    this.saveValueEmployeeCode = this.value;
    // Lấy dữ liệu nhân viên
    this.axios.get(`/Employees`)
      .then((response) => {
        this.employees = response.data.data;
        this.fakeEmployees = [...this.employees];
      })
      .catch(() => {});
  },
};
</script>
<style scoped>
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
  width: 77%;
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
.tranform {
  transform: rotate(180deg);
  transition: transform 0.15s linear;
}
.tranform-again {
  transform: rotate(0deg);
  transition: transform 0.15s linear;
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
</style>