import Vue from 'vue'
import Vuelidate from 'vuelidate'

import App from './App'
import router from './router'
import store from './store/index'
import axios from "axios";

Vue.use(Vuelidate);

Vue.config.productionTip = false;

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App),
  created() {
    if (localStorage.getItem('token')) {
      axios.get('https://localhost:44397/api/Account/UserInfo', {
        headers: {
          'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
      }).then(resp => {
        this.$store.commit('setUser', {
          userName: resp.data.UserName,
          userRole: resp.data.UserRole
        });
      }).catch(error => {
        if (error.response) {
          alert(error.response.data);
        } else {
          alert(error.message)
        }
      });
    }
  }
})
