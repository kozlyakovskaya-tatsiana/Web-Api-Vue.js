import store from '../store'
import axios from "axios";

export default function (to, from, next) {
  if (store.getters.user){
    if(store.getters.isAdmin){
      next();
      return;
    }
  }
  if (localStorage.getItem('token')) {
    axios.get('https://localhost:44397/api/Account/UserInfo', {
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).then(resp => {
      store.commit('setUser', {
        userName: resp.data.UserName,
        userRole: resp.data.UserRole
      });
      if (store.getters.isAdmin){
        next();
      }
      else {
        alert('No access to the resource. Only for admin.')
         next('/')
      }

    }).catch(error => {
      if (error.response) {
        alert(error.response.data);
      } else {
        alert(error.message)
      };
      alert('No access to the resource. Only for admin.')
      next('/')
    });
  }
  else{
    alert('No access to the resource. Only for admin.')
    next('/')
  }
}
