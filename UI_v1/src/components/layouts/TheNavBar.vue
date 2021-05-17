<template>
  <div id="navbar" :class="{collapse : toggle}">
    <div class="logo-container">
      <div class="flex">
        <div class="mi mi-24 mi-logo-option"></div>
        <div class="logo-img"></div>
      </div>    
    </div>
    <div class="menu-item-list">
      <router-link
        class="menu-item"
        to="/"
        v-bind:class="{ active: selectedItem == 0 }"
        v-on:click="selectItem(0)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-dashboard"></div>
        </div>
        <div class="menu-item-title">Tổng quan</div>
      </router-link>
      <div
        class="menu-item"        
        v-bind:class="{ active: selectedItem == 1 }"
        v-on:click="selectItem(1)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-cash"></div>
        </div>
        <div class="menu-item-title">Tiền mặt</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 2 }"
        v-on:click="selectItem(2)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-bank"></div>
        </div>
        <div class="menu-item-title">Tiền gửi</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 3 }"
        v-on:click="selectItem(3)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-pu"></div>
        </div>
        <div class="menu-item-title">Mua hàng</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 4 }"
        v-on:click="selectItem(4)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-sale"></div>
        </div>
        <div class="menu-item-title">Bán hàng</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 5 }"
        v-on:click="selectItem(5)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-invoice"></div>
        </div>
        <div class="menu-item-title">Quản lí hoá đơn</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 6 }"
        v-on:click="selectItem(6)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-stock"></div>
        </div>
        <div class="menu-item-title">Kho</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 7 }"
        v-on:click="selectItem(7)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-tools"></div>
        </div>
        <div class="menu-item-title">Công cụ dụng cụ</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 8 }"
        v-on:click="selectItem(8)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-fixed-assets"></div>
        </div>
        <div class="menu-item-title">Tài sản cố định</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 9 }"
        v-on:click="selectItem(9)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-tax"></div>
        </div>
        <div class="menu-item-title">Thuế</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 10 }"
        v-on:click="selectItem(10)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-price"></div>
        </div>
        <div class="menu-item-title">Giá thành</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 11 }"
        v-on:click="selectItem(11)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-general"></div>
        </div>
        <div class="menu-item-title">Tổng hợp</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 12 }"
        v-on:click="selectItem(12)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-budget"></div>
        </div>
        <div class="menu-item-title">Ngân sách</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 13 }"
        v-on:click="selectItem(13)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-report"></div>
        </div>
        <div class="menu-item-title">Báo cáo</div>
      </div>
      <div
        class="menu-item"
        v-bind:class="{ active: selectedItem == 14 }"
        v-on:click="selectItem(14)"
      >
        <div class="menu-item-icon">
          <div class="mi mi-24 mi-sidebar-finance"></div>
        </div>
        <div class="menu-item-title">Báo cáo tài chính</div>
      </div>
    </div>
    <div class="toggle-navbar">
      <div class="position icon icon-24 three-stripes" @click="btnToggleClick"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MyNavbar",
  props:{
    
  },
  data: function () {
    return {
      selectedItem: 0,
      toggle: false,
    };
  },
  methods: {
    btnToggleClick(){
      this.toggle = !this.toggle;
    },
    selectItem: function(item){
      this.selectedItem = item;
    },
  },
};
</script>

<style scoped>
.flex{
  display: flex;
  align-items: center;
}
#navbar {
  background: #393a3d;
  height: 100vh;
  width: 178px;
  font-family: NotoSans-Regular;
  /* transition: 0.5s; */
}
.logo-container {
  width: 100%;
  height: 48px;
  display: flex;
  align-items: center;
  justify-content: center;
}
.logo-img {
  height: 32px;
  width: 96px;
  background-image: url("../../assets/Logo_Module_TiengViet_White.66947422.svg");
  background-size: cover;
  background-repeat: no-repeat;
  cursor: pointer;
}
.menu-item-list {
  padding-top: 14px;
}
.menu-item {
  width: 178px;
  height: 42px;
  display: flex;
  align-items: center;
  cursor: pointer;
  position: relative;
}
.menu-item::before {
  content: "";
  position: absolute;
  width: 6px;
  height: 100%;
  background: rgb(51, 191, 34);
  top: 0;
  left: 0;
  display: none;
}
.menu-item:hover {
  background: rgba(255, 255, 255, 0.2);
}
.menu-item.active {
  background: rgba(255, 255, 255, 0.2);
}
.menu-item.active::before {
  display: block; 
}
.menu-item .menu-item-icon {
  margin-right: 8px;
  margin-left: 16px;
}

.menu-item .menu-item-title {
  color: white;
  font-size: 13px;
  font-weight: 500;
  font-family: NotoSans-Regular;
  text-decoration: none;
}
.menu-item.active .menu-item-title {
  font-family: NotoSans-Regular;
  font-weight: 1000;
}
.mi-24 {
  width: 24px;
  height: 24px;
  min-width: 24px;
  min-height: 24px;
}
.mi-14 {
  width: 14px;
  height: 14px;
  min-width: 14px;
  min-height: 14px;
}

.mi-16 {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
}

.active .mi {
  background-position-y: -1585px;
}

.active .mi-sidebar-finance {
  background-position: -248px -1664px;
}

.active .mi-sidebar-budget {
  background-position: -423px -1659px;
}
.toggle-navbar{
  position: fixed;
  top: 0px;
  left: 187px;
  height: 48px;
  display: flex;
  align-items: center;
}

.position{

}

#navbar.collapse{
  width: 60px;
}
#navbar.collapse .menu-item-title .menu-item{
  width: 60px;
}
#navbar.collapse .menu-item-list .menu-item-title{
  display: none;
}
#navbar.collapse .logo-img{
  display: none;
}
#navbar.collapse .flex{
  display: none;
}
#navbar.collapse .toggle-navbar{
  left: 10px;
  top: 5px;
}
#navbar.collapse .three-stripes{
  margin-left: 7px;
  background-position: -312px -32px;
}
</style>