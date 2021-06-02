<template>
  <div>
    <div class="department-box" :class="{'input-focus': toggleAutocomplete == false}">
      <div class="selected-option">
        <input
          type="text"
          ref="focusDepartment"
          class="input-select"
          @keydown.up="up"
          @keydown.down="down"
          @keydown.enter="enter"
          :value="showValueDepartment"
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
    <div class="select-custom" :class="{ invisible: toggleAutocomplete }">
      <div class="header-select">
        <div class="text">Mã đơn vị</div>
        <div class="text" style="margin-left: 79px">Tên đơn vị</div>
      </div>
      <div class="department-content" ref="positionDepartment" 
        v-for="(department, index) in departments"
        :key="index"
        :value="department.departmentId"
        @click="onBtnDepartmentClick(department ,index)"
        :class="{ color: currentIndex == index }"
      >
        <div class="scrollItem" >
          <div>{{ department.departmentCode }}</div>
          <div class="scrollName" style="margin-left: 100px">{{ department.departmentName }}</div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      departments: [],
      toggleAutocomplete: true,
      currentIndex: 0,
      saveValueDepartment: null,
    };
  },
  methods: {
    onBtnDropdownClick() {
      this.toggleAutocomplete = !this.toggleAutocomplete;
      if (!this.toggleAutocomplete) {
        this.$refs.focusDepartment.focus();
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
     * Click nút up cập nhật vị trí currentIndex và kiểm tra trạng thái hiển thị phòng ban
     * CreatedBY:NXCHIEN 19/05/2021
     */
    down() {
      if (this.toggleAutocomplete) {
        this.toggleAutocomplete = false;
      }
      if (this.currentIndex < this.departments.length - 1) this.currentIndex++;
      document.getElementsByClassName("scrollName")[4].scrollIntoView();
    },

    /**
     * Click enter cập nhật các giá trị để bind lên input chứa giá trị tên phòng ban/ đơn vị
     * CreatedBy: NXCHIEN 19/05/2021
     */
    enter() {
      this.saveValueDepartment = this.departments[this.currentIndex].departmentId;
      
      this.toggleAutocomplete = true;
    },
    onBtnDepartmentClick(department, index) {
      // Lưu giá trị ID lấy được
      this.saveValueDepartment = department.departmentId;
      
      this.toggleAutocomplete = true;

      this.currentIndex = index;
    },
  },
  computed:{
    showValueDepartment: {
      get() {
        for (let index = 0; index < this.departments.length; index++) {
          if (
            this.saveValueDepartment == this.departments[index].departmentId
          ) {
            return this.departments[index].departmentName;
          } 
        }
        return "";
      },
      set(value) {
        this.saveValueDepartment = value;
      },
    },
  },

  mounted() {
    this.axios
      .get("/Departments")
      .then((res) => {
        this.departments = res.data.data;
      })
      .catch((res) => {
        console.log(res);
      });
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
.select-custom .scrollItem {
  height: 32px;
  width: 100%;
  padding: 0 14px 0 10px;
  text-align: left;
  line-height: 32px;
  display: flex;
  align-items: center;
}
.scrollItem:hover {
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
</style>