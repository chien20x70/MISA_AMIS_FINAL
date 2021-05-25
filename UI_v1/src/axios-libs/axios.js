import Vue from 'vue'

import axios from 'axios'
import VueAxios from 'vue-axios'

const axiosIns = axios.create({
  
  baseURL: 'https://localhost:44314/api/v1'
  
})

Vue.prototype.$http = axiosIns

Vue.use(VueAxios, axiosIns)

export default axiosIns