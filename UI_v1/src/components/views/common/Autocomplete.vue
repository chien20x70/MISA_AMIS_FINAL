<template>
  <div>
    <div class="department-box" style="margin-top: 4px">
      <div class="selected-option">
        <input type="text" ref="focusDepartment" class="input-select" />
        <div class="icon-selected">
          <div class="icon icon-30 arrow-dropdown" @click="onBtnDropdownClick"></div>
        </div>
      </div>
    </div>
    <div class="select-custom" :class="{ invisible: toggleAutocomplete }">
      <div class="header-select">
        <div class="text">Mã đơn vị</div>
        <div class="text" style="margin-left: 79px">Tên đơn vị</div>
      </div>
      <div class="department-content" ref="positionDepartment">
        <div class="item">
          <div></div>
          <div style="margin-left: 100px"></div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      toggleAutocomplete: true,
    }
  },
    methods: {
      onBtnDropdownClick(){
        this.toggleAutocomplete = !this.toggleAutocomplete;
      }
    },
}
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
.select-custom{
  position: absolute;
  overflow-y: auto;
  min-width: 200px;
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
.cashbox__input--size .select-custom{
  top: 43px;
  z-index: 10;
  width: 28%;
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
.invisible{
  display: none;
}
</style>