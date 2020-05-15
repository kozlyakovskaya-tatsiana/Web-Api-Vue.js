<template>
 <div class="container">
    <div class="card mt-3 mb-3">
      <div class="row no-gutters">
        <div class="col-md-4">
          <img :src="book.ImageSrc" class="img-fluid">
        </div>
        <div class="col-md-8">
            <div class="card-body">
              <h4 class="card-title">{{book.Name}}</h4>
              <h6 class="cart-text">Author: {{book.Author}}</h6>
              <h6 class="cart-text">Year: {{book.Year}}</h6>
              <p class="card-text">{{book.Description}}</p>
          </div>
          <div class="text-right">
            <button v-if="isAdmin" type="button" class="btn btn-danger mr-2" v-on:click.prevent="onDelete" v-bind:disabled="isLoading">Delete</button>
            <router-link v-if="isAdmin" v-bind:to="'/bookedit/' + book.Id"><button type="button" class="btn btn-warning mr-2" v-bind:disabled="isLoading">Edit</button></router-link>
            <button v-if="!isAdmin" type="button" class="btn btn-success mr-2" v-bind:disabled="isLoading" v-on:click.prevent="addBookToUser">Add to my books</button>
            <div class="text-center">
              <img v-if="isLoading" src="/static/loading.gif" style="height: 160px; width: 210px;">
            </div>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script>
   import axios from "axios";

    export default {
      data(){
        return{
          book:''
        }
      },
      computed:{
        isLoading(){
          return this.$store.getters.loading;
        },
        isAdmin(){
          return this.$store.getters.isAdmin;
        }
      },
      methods:{
        onDelete(){
          if(!confirm('Do you really want to delete this book?'))
            return;
          this.$store.commit('setLoading', true);
          axios.delete('https://localhost:44397/api/Book/' + this.book.Id, {
            headers: {
              'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
          }).then(resp => {
            this.$store.commit('setLoading', false);
            alert('Deleting is successful');
            this.$router.push('/bookstore');
          }).catch(error => {
            this.$store.commit('setLoading', false);
            if (error.response) {
              console.log(error.response.data);
            } else {
              alert(error.message)
            }
          });
        },
        addBookToUser(){
          this.$store.commit('setLoading', true);
          const headers = {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json'
          };
          axios.post('https://localhost:44397/api/User', this.book.Id,{
            headers:headers
          }).
          then(resp => {
            this.$store.commit('setLoading', false);
            alert('Book is added to your booklist');
          }).catch(error => {
            this.$store.commit('setLoading', false);
            if (error.response) {
              alert(error.response.data)
            } else {
              alert(error.message)
            }
          });
        }
      },
      created() {
        this.$store.commit('setLoading', true);
        axios.get('https://localhost:44397/api/Book/' + this.$route.params.id, {
          headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
          }
        }).
        then(resp=>{
          this.book = resp.data;
          this.$store.commit('setLoading', false);
        }).
        catch(error=>{
          this.$store.commit('setLoading', false);
          if (error.response) {
            // Request made and server responded
            alert(error.response.data);
          }
          else{
            alert(error.message)
          }
        });
      }
    }
</script>

<style scoped>
  img{
   height: 450px;
  }
</style>
