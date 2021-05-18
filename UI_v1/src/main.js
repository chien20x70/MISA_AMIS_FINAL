import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import EmployeeList from './components/views/employee/EmployeeList.vue'
import '../src/assets/font/fontawesome-5.15.1/css/all.min.css'
import { ValidationProvider, extend, ValidationObserver } from 'vee-validate';
import './axios-libs/axios.js'
import "./components/styles/main.css";
import "./components/styles/input.css";
import "./components/styles/table.css";
import "./components/styles/dropdown.css";
import "./components/styles/button.css";
import "./components/styles/icon.css";
import VueHotkey from 'v-hotkey'
import vuetify from './plugins/vuetify'

Vue.use(VueHotkey)

/* 
Validate các trường để trống thì thông báo lỗi
CreatedBy: NXCHIEN 10/05/2021
*/
extend('required', {
  validate(value) {
    return {
      required: true,
      valid: ['', null, undefined].indexOf(value) === -1
    };
  },
  computesRequired: true,
  message: '{_field_} không thể để trống'
});

Vue.component('ValidationProvider', ValidationProvider);
Vue.component('ValidationObserver', ValidationObserver);
Vue.config.productionTip = false
Vue.use(VueRouter);

const routes = [
  { path: '/', component: EmployeeList },
]

const router = new VueRouter({
  routes
})

/* 
Lọc giá trị ngày tháng năm để hiển thị lên client
*/
Vue.filter('dateFormatDDMMYY', function (date) { 
  if(date == null) return ""
  if(date == "0001-01-01T00:00:00") return ""
  var newDate = new Date(date);
  var day = newDate.getDate();
  var month = newDate.getMonth() + 1;
  var year = newDate.getFullYear();
  day = day < 10 ? "0" + day : day;
  month = month < 10 ? "0" + month : month;
  return `${day}/${month}/${year}`;
})

new Vue({
  render: h => h(App),
  vuetify,
  router
}).$mount('#app')
