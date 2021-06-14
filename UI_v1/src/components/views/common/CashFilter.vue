<template>
  <div>
    <button
      class="btn-btn filter"
      style="border: 2px solid; margin-left: 10px"
      @click="onBtnFilterClick"
    >
      <div class="btn-btn-text" style="display: flex">
        <span class="pr-4">Lọc</span>
        <div class="icon icon-16 arrow-up--black" style="margin-left: 3px;"></div>
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
          <DatePicker v-if="toggleFilter == true" v-model="endDate" :type="'endDate'" @sendEndDate="getEndDate" :startDate="startDate"/>
        </div>
      </div>
      <!-- <span class="errorDate" :class="{'visible-span': errorData}">&lt;Đến ngày&gt; phải lớn hơn hoặc bằng &lt;Từ ngày&gt;</span> -->
      <div class="btn-footer-filter">
        <button class="btn-padding reset">Đặt lại</button>
        <button class="btn-padding success" @click="onBtnCashFilterClick">Lọc</button>
      </div>
    </div>
  </div>
</template>
<script>
import moment from 'moment'
import DatePicker from './DatePicker.vue'
import { ModelSelect } from "vue-search-select";
export default {
  components: {
    ModelSelect,
    DatePicker
  },
  data() {
    return {
      errorData: false,
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
      let fisrtDateOfYear = new Date(this.firstDateOfYear);
      return moment(fisrtDateOfYear).format("YYYY-MM-DD");
    },
    /**
     * Lấy ngày hiện tại
     */
    getCurrentDate(){
      let currentDate = new Date();
      return moment(currentDate).format("YYYY-MM-DD");
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
      var startDate = new Date(this.startDate);
      var endDate = new Date(this.endDate);
      if (endDate < startDate) {
        this.errorData = true;
      }else{
        this.toggleFilter = false;
        this.errorData = false;
        this.$emit("onBtnCashFilterClick", this.startDate, this.endDate);
      }
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
.btn-footer-filter {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: calc(91% + 4px);
  position: absolute;
  bottom: 15px;
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
.errorDate{
  color: red; 
  font-size: 11px; 
  margin-left: 40px;
  display: none;
}
.visible-span{
  display: inline-block;
}
</style>