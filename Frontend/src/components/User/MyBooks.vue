<template>
  <div class="container">
      <div class="row col-md-3 offset-md-5 mt-2">
        <h3>My books</h3>
      </div>
      <h4 v-if="!books.length" class="row col-md-7 offset-md-3 mt-2 mb-2">You have no any books in your booklist yet</h4>
    <div class="row col-md-5 offset-1  mt-2 mb-3">
      <input type="text" class="form-control" placeholder="Search book" v-model="searchBook">
    </div>
      <div class="card mt-3 mb-3" v-for="book of filteredBooks">
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
              <button type="button" class="btn btn-danger mr-2"  v-on:click="deleteBook($event,book.Id)">Delete from my books</button>
            </div>
          </div>
        </div>
    </div>
    <div class="text-center">
      <img v-if="isLoading" src="/static/loading2.gif" class="mt-2 mb-3" style="height: 240px; width: 200px;">
    </div>
  </div>
</template>

<script>
  import axios from "axios";

  export default {
    data() {
      return {
        books: '',
        searchBook: ''
      }
    },
    computed: {
      isLoading() {
        return this.$store.getters.loading;
      },
      filteredBooks() {
        if (this.searchBook === '')
          return this.books;
        return this.books.filter(book => {
          return book.Name.toLowerCase().startsWith(this.searchBook.toLowerCase());
        });
      }
    },
      methods: {
        deleteBook(event, bookId) {
          event.preventDefault();
          if (!confirm('Do you really want to delete this book?'))
            return;
          event.target.disabled = true;
          axios.delete('https://localhost:44397/api/User/' + bookId, {
            headers: {
              'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
          }).then(resp => {
            alert('Deleting is successful');
            this.$store.commit('setLoading', true);
            axios.get('https://localhost:44397/api/User', {
              headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
              }
            }).then(resp => {
              event.target.disabled = false;
              this.books = resp.data;
              this.$store.commit('setLoading', false);
            }).catch(error => {
              event.target.disabled = false;
              this.$store.commit('setLoading', false);
              if (error.response) {
                // Request made and server responded
                alert(error.response.data);
              } else {
                alert(error.message)
              }
            });
          }).catch(error => {
            event.target.disabled = false;
            if (error.response) {
              // Request made and server responded
              alert(error.response.data);
            } else {
              alert(error.message)
            }
          });
        }
      },
      created() {
        this.$store.commit('setLoading', true);
        axios.get('https://localhost:44397/api/User', {
          headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
          }
        }).then(resp => {
          this.books = resp.data;
          this.$store.commit('setLoading', false);
        }).catch(error => {
          this.$store.commit('setLoading', false);
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
  img{
    height: 400px;
  }
</style>
