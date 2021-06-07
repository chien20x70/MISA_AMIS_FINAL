<template>
  <model-select
    v-model="content"
    :options="employees"
    style="margin-top: 4px; border: 1px solid #babec5; height: 32px;"
  >
  </model-select>
</template>
<script>
import { ModelSelect } from "vue-search-select";
export default {
  components: {
    ModelSelect,
  },
  data() {
    return {
      content: this.value,
      employees: [],
      // options: [
      //     { value: '1', text: 'Tùy chọn' },
      //     { value: '2', text: 'Năm sau' },
      //     { value: '3', text: 'Quý sau' },
      //     { value: '4', text: 'Tháng sau' },
      //     { value: '5', text: 'Đầu năm đến hiện tại' }
      // ],
      suppHead: [
        { label: "Đối tượng", width: "100" },
        { label: "Tên đối tượng", width: "250" },
        { label: "Mã số thuế", width: "120" },
        { label: "Địa chỉ", width: "200" },
        { label: "Điện thoại", width: "100" },
      ],
    };
  },
  mounted() {
    this.axios
      .get(`/Employees/Filter?pageSize=20&pageIndex=2&filter=`)
      .then((response) => {
        response.data.data.data.forEach((item) => {
          this.employees.push({
            value: item.employeeId,
            text: item.fullName,
          });
        });
      })
      .catch(() => {});
  },
  watch: {
    value: function() {
      this.content = this.value;
    },
  },
};
</script>
