import Vue from 'vue'
import Router from 'vue-router'

import Main from "../components/Main";
import Registration from "../components/Auth/Registration";
import About from "../components/Info/About";
import Contact from "../components/Info/Contact";
import BookStore from "../components/Book/BookStore";
import MyBooks from "../components/User/MyBooks";
import NotFound from "../components/NotFound";
import AuthGuard from './auth-guard'
import AdminGuard from './admin-guard'
import AddBook from "../components/Book/AddBook";
import BookInfo from "../components/Book/BookInfo";
import BookEdit from "../components/Book/BookEdit";
import UsersInfo from "../components/User/UsersInfo";

Vue.use(Router);

export default new Router({
  routes: [
   {
      path:'/',
      component: Main,
      name:'main'
    },
    {
      path:'/register',
      component: Registration,
      name:'register'
    },
    {
      path:'/about',
      component: About,
      name:'about'
    },
    {
      path:'/contact',
      component: Contact,
      name:'contact'
    },
    {
      path:'/bookstore',
      component: BookStore,
      name:'bookstore',
      beforeEnter:AuthGuard
    },
    {
      path:'/mybooks',
      component: MyBooks,
      name:'mybooks',
      beforeEnter:AuthGuard
    },
    {
      path:'/addbook',
      component: AddBook,
      name:'addbook',
      beforeEnter:AdminGuard
    },
    {
      path: '/bookinfo/:id',
      component: BookInfo,
      name:'bookinfo',
      beforeEnter:AuthGuard
    },
    {
      path: '/bookedit/:id',
      component: BookEdit,
      name:'bookedit',
      beforeEnter:AdminGuard
    },
    {
      path: '/usersinfo',
      component: UsersInfo,
      name:'usersinfo',
      beforeEnter:AdminGuard
    },
    {
      path:'*',
      component: NotFound,
      name:'NotFound'
    }
  ],
  mode: 'history'
})
