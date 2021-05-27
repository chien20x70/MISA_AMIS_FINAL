import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import EmployeeList from './components/views/employee/EmployeeList.vue'
import '../src/assets/font/fontawesome-5.15.1/css/all.min.css'
// import { ValidationProvider, extend, ValidationObserver } from 'vee-validate';
import './axios-libs/axios.js'
import "./components/styles/main.css";
import "./components/styles/input.css";
import "./components/styles/table.css";
import "./components/styles/dropdown.css";
import "./components/styles/button.css";
import "./components/styles/icon.css";
import VueHotkey from 'v-hotkey'
import Antd from 'ant-design-vue';
import 'ant-design-vue/dist/antd.css';
import 'vue-search-select/dist/VueSearchSelect.css'
Vue.use(VueHotkey)
Vue.use(Antd);

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
  router
}).$mount('#app')
