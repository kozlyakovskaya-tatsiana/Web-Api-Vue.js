<template>
  <div id="mainBody" class="container" >
    <h3 class="mt-3 row col-md-2 offset-md-5">Books</h3>
    <router-link  v-if="isAdmin" id="addbook" to="/addbook" class="row col-md-3 offset-md-1"><h5>Add book</h5></router-link>
      <div class="row col-md-5 mt-2">
        <label for="sorting">Choose sorting criteria</label>
      </div>
    <div class="row">
      <div class=" col-md-5  mb-3">
        <select class="form-control" id="sorting" v-model="sortCriteria">
          <option value="name">Name</option>
          <option value="yearAsc">Old books</option>
          <option value="yearDesc">New books</option>
        </select>
      </div>
      <div class="col-md-5 offset-1 mb-3">
        <input type="text" class="form-control" placeholder="Search book" v-model="searchBook">
      </div>
    </div>

    <section v-if="!isLoading" class="text-center mb-4">
      <div class="row">
        <div class="col-md-3 mb-1 " v-for="book in displayedBooks">
          <div class="card mt-2" style="width: 270px;min-height: 380px;">
            <div class="text-right"><span class="badge badge-secondary badge-danger">Hit</span></div>
            <div class="text-center">
              <img :src="book.ImageSrc" class="img-fluid">
            </div>
            <div class="card-body">
              <h5 class="card-title">{{book.Name}}</h5>
              <h6 class="cart-text">Author: {{book.Author}}</h6>
              <h6 class="cart-text">Year: {{book.Year}}</h6>
              <router-link v-bind:to="/bookinfo/ + book.Id"><button type="button" class="btn btn-primary btn-sm">Open</button></router-link>
            </div>
          </div>
        </div>
      </div>
    </section>
        <img v-else class="mt-5 mb-5 row col-md-2 offset-md-5" src="static/loading2.gif" width="200px" height="240px">
      <nav aria-label="Page navigation example" class="row col-md-5 offset-md-5">
        <ul class="pagination">
          <li class="page-item" v-if="page != 1" ><a class="page-link" @click.prevent="page--" href="#" > Previous </a></li>
          <li class="page-item" v-for="pageNumber in pages.slice(page-1, page+5)"><a class="page-link" @click.prevent="page = pageNumber" href="#"> {{pageNumber}} </a></li>
          <li class="page-item" v-if="page < pages.length" ><a class="page-link" @click.prevent="page++" href="#" > Next </a></li>
        </ul>
      </nav>
  </div>
</template>

<script>
  import axios from "axios";

  export default {
    data(){
      return{
        sortCriteria:'yearDesc',
        searchBook:'',
        page: 1,
        perPage: 4,
        pages: [],
      }
    },
    computed:{
      books(){
        let sortedBooks;
        switch (this.sortCriteria) {
          case 'name': sortedBooks= this.$store.getters.sortedByName; break;
          case 'yearAsc': sortedBooks= this.$store.getters.sortedByYearAsc;break;
          case 'yearDesc': sortedBooks = this.$store.getters.sortedByYearDesc;break;
          default: sortedBooks = this.$store.getters.allBooks;break;
        }
        if (this.searchBook==='')
          return sortedBooks ;
       return sortedBooks.filter(book=>{
          return book.Name.toLowerCase().startsWith(this.searchBook.toLowerCase());
        });
      },
      displayedBooks () {
        return this.paginateBooks(this.books)
      },
      isLoading(){
        return this.$store.getters.loading;
      },
      isAdmin(){
        return this.$store.getters.isAdmin;
      }
    },
    watch: {
      books () {
        this.setPages();
      }
    },
    methods:{
      setPages () {
        this.page=1;
        this.pages=[];
        let numberOfPages = Math.ceil(this.books.length / this.perPage);
        for (let index = 1; index <= numberOfPages; index++) {
          this.pages.push(index);
        }
      },
      paginateBooks(books){
        let page = this.page;
        let perPage = this.perPage;
        let from = (page * perPage) - perPage;
        let to = (page * perPage);
        return books.slice(from, to);
      }
    },
    created() {
      this.$store.commit('setLoading', true);
      axios.get('https://localhost:44397/api/Book', {
        headers: {
          'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
      }).
      then(resp=>{
        this.$store.commit('setLoading', false);
        this.$store.commit('setBooks',resp.data)
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
  .badge{
    width: 4em;
  }
  img{
    height: 160px;
    /**width: 120px;*/
  }
  #addbook{
    text-decoration: none;
  }
</style>

