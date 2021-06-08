<template>
    <div class="filter__box">
        <div class="row-input">
            <span class="text">Lý do thu, chi</span><br/>
            <input type="text" style="width: 414px;">
        </div>
        <div class="row-input">
            <span class="text">Trạng thái ghi sổ</span><br/>
            <input type="text" style="width: 414px;">
        </div>
        <div class="btn-footer">
            <div class="reason">
                <span class="text">Thời gian</span><br/>
                <!-- <Autocomplete/> -->
                <model-select
                v-model="options.text"
                  :options="options"
                  style="margin-top: 4px; border: 1px solid #babec5; height: 32px;"
                >
                </model-select>  
            </div>
            <div class="reason">
                <span class="text">Từ ngày</span><br/>
                <date-pick                 
                  :displayFormat="STR_DISPLAY_FORMAT"
                  :inputAttributes="{
                    class: 'style-input-date-lib',
                    placeholder: STR_DISPLAY_FORMAT,
                    style: 'margin-top: 4px; width: 133px;',
                  }"
                  :weekdays="localeDatePicker.weekdays"
                  :months="localeDatePicker.months"
                ></date-pick>
            </div>
            <div class="reason">
                <span class="text">Đến ngày</span><br/>
                <date-pick                 
                  :displayFormat="STR_DISPLAY_FORMAT"
                  :inputAttributes="{
                    class: 'style-input-date-lib',
                    placeholder: STR_DISPLAY_FORMAT,
                    style: 'margin-top: 4px; width: 134px;',
                  }"
                  :weekdays="localeDatePicker.weekdays"
                  :months="localeDatePicker.months"
                ></date-pick>
            </div>
        </div>
        <div class="btn-footer" style="margin-top: 20px;">
            <button class="btn-padding reset">Đặt lại</button>
            <button class="btn-padding success">Lọc</button>
        </div>
    </div>
</template>
<script>
import DatePick from "vue-date-pick";

import { ModelSelect } from "vue-search-select";
export default {
    components:{
        DatePick,
        ModelSelect
    },
    data() {
        return {
            departments: [],
            STR_DISPLAY_FORMAT: "DD/MM/YYYY",
            localeDatePicker: {
                weekdays: ["T2", "T3", "T4", "T5", "T6", "T7", "CN"],
                months: [
                "Tháng 1",
                "Tháng 2",
                "Tháng 3",
                "Tháng 4",
                "Tháng 5",
                "Tháng 6",
                "Tháng 7",
                "Tháng 8",
                "Tháng 9",
                "Tháng 10",
                "Tháng 11",
                "Tháng 12",
                ],
            },
            options: [
                { value: '1', text: 'Tùy chọn' },
                { value: '2', text: 'Năm sau' },
                { value: '3', text: 'Quý sau' },
                { value: '4', text: 'Tháng sau' },
                { value: '5', text: 'Đầu năm đến hiện tại' }
            ],
        }
    },
    mounted() {
        this.axios.get("/Departments").then((res) => {
            res.data.data.forEach((item) => {
                this.departments.push({
                value: item.departmentId,
                text: item.departmentName,
                });
            });
        });
    },
}
</script>
<style scoped>
.filter__box{
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
}
.row-input{
    height: 70px;
}
.row-input input{
    margin-top: 4px;
}
.text{
    font-weight: 700;
    color: #111111;
    font-size: 12px;
}
.btn-footer{
    display: flex;
    align-items: center;
    justify-content: space-between;
}
.reason{
    width: 32%;
}
.reason input{
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
</style>