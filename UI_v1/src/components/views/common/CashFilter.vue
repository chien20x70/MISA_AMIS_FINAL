<template>
  <div>
    <button
      class="btn-btn filter"
      style="border: 2px solid; margin-left: 10px"
      @click="onBtnFilterClick"
    >
      <div class="btn-btn-text" style="display: flex">
        <span class="pr-4">Lọc</span>
        <div class="icon icon-16 arrow-up--black"></div>
      </div>
    </button>
    <div class="filter__box" :class="{ visible: toggleFilter }">
      <div class="row-input tooltip tooltip--position30-30">
        <span class="text">Lý do thu, chi</span><br />
        <input type="text" class="" style="width: 414px; cursor: not-allowed; background: rgb(224 224 224);" placeholder="Tất cả" readonly/>
        <span class="tooltip__text">Chức năng đang phát triển</span>
      </div>
      <div class="row-input tooltip tooltip--position30-30">
        <span class="text">Trạng thái ghi sổ</span><br />
        <input type="text" style="width: 414px; cursor: not-allowed; background: rgb(224 224 224);" placeholder="Tất cả" readonly/>
        <span class="tooltip__text">Chức năng đang phát triển</span>
      </div>
      <div class="btn-footer">
        <div class="reason">
          <span class="text">Thời gian</span><br />
          <model-select           
            v-model="options.text"
            :options="options"
            :isDisabled="true"
            style="margin-top: 4px; border: 1px solid #babec5; height: 32px"
          >
          </model-select>
        </div>
        <div class="reason--time">
          <span class="text">Từ ngày</span><br />
          <DatePicker v-if="toggleFilter == true" v-model="startDate" :type="'startDate'" @sendStartDate="getStartDate"/>
        </div>
        <div class="reason--time" style="padding-right: 0;">
          <span class="text">Đến ngày</span><br />
          <DatePicker v-if="toggleFilter == true" v-model="endDate" :type="'endDate'" @sendEndDate="getEndDate"/>
        </div>
      </div>
      <div class="btn-footer" style="margin-top: 20px">
        <button class="btn-padding reset">Đặt lại</button>
        <button class="btn-padding success" @click="onBtnCashFilterClick">Lọc</button>
      </div>
    </div>
  </div>
</template>
<script>
import DatePicker from './DatePicker.vue'
import { ModelSelect } from "vue-search-select";
export default {
  components: {
    ModelSelect,
    DatePicker
  },
  data() {
    return {
      firstDateOfYear: '2021/01/01',      // Ngày đầu tiên của năm
      startDate: null,                    // Ngày bắt đầu để lọc
      endDate: null,                      // Ngày kết thúc
      toggleFilter: false,                // Đóng mở CashFilter
      options: [
        { value: "1", text: "Tùy chọn" },
        { value: "2", text: "Năm sau" },
        { value: "3", text: "Quý sau" },
        { value: "4", text: "Tháng sau" },
        { value: "5", text: "Đầu năm đến hiện tại" },
      ],
    };
  },
  methods: {
    /**
     * Đóng mở CashFilter binđing ngày bđ và ngày kết thúc
     * CreatedBy: NXCHIEN 09/06/2021
     */
    onBtnFilterClick() {
      this.toggleFilter = !this.toggleFilter;
      this.endDate = this.getCurrentDate();
      this.startDate = this.getFisrtDateOfYear();
    },

    //#region Lấy ngày lần đầu
    /**
     * Lấy ngày đầu tiên của năm
     */
    getFisrtDateOfYear(){
      let currentDate = new Date(this.firstDateOfYear);
      var day = currentDate.getDate();
      var month = currentDate.getMonth() + 1;
      var year = currentDate.getFullYear();
      day = day < 10 ? "0" + day : day;
      month = month < 10 ? "0" + month : month;
      return `${year}-${month}-${day}`;
    },
    /**
     * Lấy ngày hiện tại
     */
    getCurrentDate(){
      let currentDate = new Date();
      var day = currentDate.getDate();
      var month = currentDate.getMonth() + 1;
      var year = currentDate.getFullYear();
      day = day < 10 ? "0" + day : day;
      month = month < 10 ? "0" + month : month;
      return `${year}-${month}-${day}`;
    },
    //#endregion

    //#region  Cập nhật ngày từ DatePicker khi chọn
    getStartDate(value){
      this.startDate = value;
    },
    getEndDate(value){
      this.endDate = value;
    },
    //#endregion

    // Truyền giá trị cần lọc sang CashList để lọc
    onBtnCashFilterClick(){
      this.toggleFilter = false;
      this.$emit("onBtnCashFilterClick", this.startDate, this.endDate);
    },

    
  
  },
  created() {
    window.addEventListener("click", (e) => {
      if (!this.$el.contains(e.target) && e.target.className != 'vdpClearInput') {
        this.toggleFilter = false;
      }
    });
  },
  mounted() {
    this.options.text = this.options[4].value;
  },
};
</script>
<style scoped>
.filter__box {
  position: absolute;
  top: 180px;
  left: 75px;
  height: 285px;
  width: 454px;
  background-color: #fff;
  padding: 16px 20px 20px;
  z-index: 4;
  border: 1px solid #babec5;
  box-shadow: 0 10px 10px rgba(0, 0, 0, 0.2);
  border-radius: 4px;
  display: none;
}
.row-input {
  height: 70px;
}
.row-input input {
  margin-top: 4px;
}
.text {
  font-weight: 700;
  color: #111111;
  font-size: 12px;
}
.btn-footer {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: calc(100% + 3px);
}
.reason {
  width: calc(42% + 2px);
  padding-right: 10px;
}
.reason input {
  width: 100%;
  margin-top: 4px;
}
.reason--time {
  width: 130px;
  padding-right: 10px;
}
.reason--time input {
  width: 100%;
  margin-top: 4px;
}
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
.visible {
  display: block;
}
</style>