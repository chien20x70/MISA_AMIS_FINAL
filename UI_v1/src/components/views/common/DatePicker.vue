<template>
  <date-pick
    v-model="content"
    @input="handleInput"
    :displayFormat="displayFormat"
    :inputAttributes="{
      class: 'style-input-date-lib',
      placeholder: '__/__/____',
      style: 'margin-top: 4px; width: 167px;',
    }"
    :weekdays="localeDatePicker.weekdays"
    :months="localeDatePicker.months"
  ></date-pick>
</template>
<script>
import DatePick from "vue-date-pick";
import "vue-date-pick/dist/vueDatePick.css";
export default {
  name: "DatePicker",
  props: ['value', 'accountingDate', 'refDate','dateOfIN', 'dateOfBirth'],
  components: {
    DatePick,
  },
  methods: {
    handleInput (value) {
      this.content = value;
      if(this.accountingDate == 'accountingDate'){
        this.$emit('sendAccountingDate', this.content);
      } 
      if (this.refDate == 'refDate') {
        this.$emit('sendRefDate', this.content);
      }
      if (this.dateOfIN == 'dateOfIN') {
        this.$emit('sendDateOfIN', this.content);
      }
      if (this.dateOfBirth == 'dateOfBirth') {
        this.$emit('sendDateOfBirth', this.content);
      }
    }
  },
  mounted() {
    this.content = this.value;
  },
  data() {
    return {
      content: null,
      displayFormat: "DD/MM/YYYY",
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
    };
  },
};
</script>
<style scoped>
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