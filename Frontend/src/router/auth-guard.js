import store from '../store'
import axios from "axios";

export default function (to, from, next) {
  if (store.getters.user){
    next();
    return;
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
      next();
      return;
    }).catch(error => {
      if (error.response) {
        alert(error.response.data);
      } else {
        alert(error.message)
      };
      next('/register');
      alert('No access to the resource. Register or log in a system.')
    });
  }
  else{
    next('/register');
    alert('No access to the resource. Register or log in a system.')
  }
}
