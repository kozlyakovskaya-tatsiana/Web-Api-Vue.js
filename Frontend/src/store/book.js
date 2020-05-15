export default {
  state:{
    books:[]
  },
  getters:{
    allBooks(state){
      return state.books;
    },
    sortedByName(state){
      let sortFunc = function (book1,book2) {
        if (book1.Name.toLowerCase()>book2.Name.toLowerCase())
          return 1;
        else if (book1.Name.toLowerCase()<book2.Name.toLowerCase())
          return -1;
        return 0;
      };

      return state.books.sort(sortFunc);
    },
    sortedByYearAsc(state){
      let sortFunc = function (book1,book2) {
        return book1.Year-book2.Year;
      };

      return state.books.sort(sortFunc);
    },
    sortedByYearDesc(state){
      let sortFunc = function (book1,book2) {
        return book2.Year-book1.Year;
      };

      return state.books.sort(sortFunc);
    }
  },
  mutations:{
    setBooks(state, payload){
      state.books = payload;
    }
  },
  actions:{}
}
