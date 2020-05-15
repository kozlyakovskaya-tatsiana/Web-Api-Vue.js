<template>
  <div class="container">
    <h3 class="row col-md-3 offset-md-5 mt-2 mb-2">Users</h3>
    <div class="row col-md-5 offset-1  mt-2 mb-3">
      <input type="text" class="form-control" placeholder="Search user by name" v-model="searchUser">
    </div>
    <table class="table table-bordered">
      <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Second Name</th>
        <th scope="col">First Name</th>
        <th scope="col">Email</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="(user,index) of sortedUsers">
        <th scope="row">{{index+1}}</th>
        <td>{{user.SecondName}}</td>
        <td>{{user.FirstName}}</td>
        <td>{{user.Email}}</td>
      </tr>
      </tbody>
    </table>
  </div>

</template>

<script>
    import axios from "axios";

    export default {
      data(){
        return{
          users:[],
          searchUser:''
        }
      },
      computed:{
        sortedUsers(){
          if (!this.searchUser)
            return this.users.sort(this.sortFunc);

          return this.users.filter(u=>{
            return (u.SecondName.toLowerCase().startsWith(this.searchUser.toLowerCase()) ||
            u.FirstName.toLowerCase().startsWith(this.searchUser.toLowerCase()))
          })
        }
      },
      methods:{
        sortFunc (user1,user2) {
          if (user1.SecondName.toLowerCase()>user2.SecondName.toLowerCase())
            return 1;
          else if (user1.SecondName.toLowerCase()<user2.SecondName.toLowerCase())
            return -1;
          return 0;
        }
      },
      created() {
        axios.get('https://localhost:44397/api/User/users', {
          headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
          }
        }).then(resp => {
          this.users = resp.data;
        }).catch(error => {
          if (error.response) {
            // Request made and server responded
            alert(error.response.data);
          } else {
            alert(error.message)
          }
        });
      }

    }
</script>

<style scoped>

</style>
