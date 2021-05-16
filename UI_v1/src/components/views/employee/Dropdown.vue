<template>
  <div>
    <div class="dropdown">
      <button class="btn-btn hover padding" @click="btnDropdownClick">
        <div class="flex btn-btn-text">
          <div class="header-icon arrow-up--blue" :class="{'border': valueDrop}"></div>
        </div>
      </button>
      <div class="dropdown-content" :class="{ display: !valueDrop }">
        <div class="dropdown-row-1" @click="btnDuplicateClick">Nhân bản</div>
        <div class="dropdown-row-1" @click="btnDelete">Xóa</div>
        <div class="dropdown-row-1">Ngưng sử dụng</div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  components: {},
  data() {
    return {
      valueDrop: false,       // Giá trị hiển thị dropdown-content
    };
  },
  methods: {
    /* 
    Hiển thị dropdown
    CreatedBy: NXCHIEN 10/05/2021
    */
    btnDropdownClick() {
      this.valueDrop = !this.valueDrop;
    },

    /* 
    Hiển thị popup
    CreatedBy: NXCHIEN 10/05/2021
    */
    btnDelete() {
      this.$emit("showPopup");
      this.valueDrop = false;
    },

    btnDuplicateClick(){
      this.$emit("showDialog", "duplicate");
      this.valueDrop = false;
    }
  },
  created() {
    /* 
    Phát hiện click chuột thì đóng dropdown
    CreatedBy: NXCHIEN 10/05/2021
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
  position: absolute;
  height: 93px;
  width: 123px;
  right: -25px;
  top: 30px;
  background-color: white;
  z-index: 10;
  border: 1px solid #bbb;
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
  padding: 0 0 0 10px;
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
.header-icon {
  background: url("../../../assets/Sprites.5f05e81f.svg") no-repeat;
  cursor: pointer;
  min-width: 16px;
  min-height: 16px;
  height: 16px;
  width: 16px;
}
.arrow-up--blue {
  background-position: -897px -359px;
}

.dropdown-row-1 {
  height: 29px;
  width: 119px;
  text-align: left;
  padding: 5px 10px;
  box-sizing: border-box;
}
.dropdown-row-1:hover {
  color: #019160;
  background-color: rgb(224, 224, 224);
}
.border{
  border: 1px solid rgb(58, 58, 253);
}
</style>