<template>
  <div>
    <div class="dropdown">
      <button class="btn-btn hover padding" @click="btnDropdownClick($event)" :class="{'border': valueDrop}">
        <div class="flex btn-btn-text">
          <div class="icon icon-16 arrow-up--blue"></div>
        </div>
      </button>
      <div class="dropdown-content" :class="{ 'display': !valueDrop}" :style="{top: top + 'px', left: left + 'px'}" >
        <div class="dropdown-row-1" @click="btnDuplicateClick">Nhân bản</div>
        <div class="dropdown-row-1" @click="btnDelete">Xóa</div>
        <!-- <div class="dropdown-row-1">Ngưng sử dụng</div> -->
      </div>
    </div>
  </div>
</template>
<script>
export default {
  //#region Khai báo
  data() {
    return {
      valueDrop: false,       // Giá trị hiển thị dropdown-content
      left: 0,                // Căn trái dropdown-content
      top: 0,                 // Căn phải dropdown-content
    };
  },
  //#endregion

  //#region METHODS
  methods: {
    /* 
    Hiển thị dropdown đồng thời hiển thị border
    CreatedBy: NXCHIEN 17/05/2021
    */
    btnDropdownClick(event) {
      this.valueDrop = !this.valueDrop;
      this.left = event.clientX - 80;
      this.top = event.clientY + 15; 
    },

    /* 
    Hiển thị popup
    CreatedBy: NXCHIEN 17/05/2021
    */
    btnDelete() {
      this.$emit("showPopup");
      this.valueDrop = false;
    },

    /**
     * Click button gọi đến sự kiện bên component cha EmployeeList
     * CreatedBy: NXCHIEN 17/05/2021
     */
    btnDuplicateClick(){
      // gọi sự kiện 
      this.$emit("showDialog");
      // bỏ bind border vào nút vừa click
      this.valueDrop = false;
    }
  },
  //#endregion

  created() {
    /* 
    Phát hiện click chuột thì đóng dropdown
    CreatedBy: NXCHIEN 17/05/2021
    */
    window.addEventListener("click", (e) => {
      if (!this.$el.contains(e.target)) {
        this.valueDrop = false;
      }
    });
  },
};
</script>
<style scoped>
.flex {
  display: flex !important;
}
.dropdown {
  position: relative;
}
.dropdown .dropdown-content {
  position: fixed;
  top: 0;
  left: 0;
  height: 63px;
  width: 123px;
  background-color: white;
  z-index: 100;
  border: 1px solid #bbb;
  border-radius: 2px;
  padding-left: 1px;
  padding-top: 1px;
}
.display {
  display: none;
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
.hover {
  border: none;
  padding: 6px 0px 6px 16px;
}
.padding {
  border: 1px solid transparent;
  margin-left: 10px;
  padding: 0;
  border-radius: 0%;
}
.padding:hover{
  border-radius: 2px;
  border: 1px solid #0075c0;
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
.arrow-up--blue {
  background-position: -895px -359px;
}
.dropdown-row-1 {
  height: 29px;
  width: 119px;
  text-align: left;
  padding: 5px 10px;
  box-sizing: border-box;
  font-size: 13px;
}
.dropdown-row-1:hover {
  color: #2ca01c;
  background-color: rgb(224, 224, 224);
}
.border{
  border: 1px solid rgb(58, 58, 253);
}
.reserve{
  top: auto;
  bottom: 100%;
}
</style>