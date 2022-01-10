import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
//import { description } from 'core-js/fn/symbol/match'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));

if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    toggleRegisterForm: false,
    isFirstTimeLogin: true,
    userMainComponent: 'tutorial',
    homeMainComponent : 'title-screen',
    gameMainComponent: 'portfolio',
    currentGame: 0,
    activeScreen: 2
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    HOME_MAIN_REGISTER(state){
      state.homeMainComponent = 'register';
    },
    HOME_MAIN_DESCRIPTION(state){
      state.homeMainComponent = 'description';
    },
    HOME_MAIN_HOW_TO_PLAY(state){
      state.homeMainComponent = "how-to-play";
    },
    HOME_MAIN_TITLE(state){
      state.homeMainComponent = "title-screen";
    },
    HOME_MAIN_LOGIN(state){
      state.homeMainComponent = 'page-login';
    },
    
    USER_MAIN_DESCRIPTION(state){
      state.userMainComponent = "how-to-play";
    },
    USER_MAIN_VIEW_GAMES(state){
      state.userMainComponent = "view-games";
    },
    USER_MAIN_CREATE_GAME(state){
      state.userMainComponent = "create-game";
    },
    USER_MAIN_TUTORIAL(state){
      state.userMainComponent = "tutorial";
    },
    USER_MAIN_VIEW_VIEW_GAME(state){
      state.userMainComponent = "game-view";
    },
    USER_MAIN_VIEW_END_GAME(state){
      state.userMainComponent = "end-game";
    },
    TOGGLE_REGISTER(state){
      state.toggleRegisterForm = !state.toggleRegisterForm;
    },
    SET_CURRENT_GAME(state, game){
      state.currentGame = game;
    },
    SET_ACTIVE_SCREEN(state, num){
      state.activeScreen = num;
    }
  }
})
