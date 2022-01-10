<template>
    <div id="login" class="text-center">
      <div class="flex-container">
        <div>
          <b-button variant="primary" lg="4" v-if="!showLogin" v-on:click="toggleRegister">{{this.$store.state.toggleRegisterForm ? "Cancel" : "Register"}}</b-button> <!--HERE do we want a show, hide-->
          <b-button variant="success" lg="4" v-if="!showRegister" v-on:click="toggleShowLoginForm(); setUserMainComponentTutorial();" v-show="!this.$store.state.toggleRegisterForm" class="pb-2">Login</b-button>
        </div>
      </div>
      
      <register v-if="this.$store.state.toggleRegisterForm" />

      <form v-if="showLogin" class="form-signin" @submit.prevent="login">
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering, please sign in.</div>
      <div class="login-form-componenet">
        <label for="username" class="sr-only">Username: </label>
        <input
          type="text"
          id="username"
          class="form-control"
          placeholder="Username"
          v-model="user.username"
          required
          autofocus
        />
      </div>
      <div class="login-form-componenet">
        <label for="password" class="sr-only">Password: </label>
        <input
          type="password"
          id="password"
          class="form-control"
          placeholder="Password"
          v-model="user.password"
          required
        />
      </div>
      <b-button variant="success" lg="6" type="submit" class="pb-2 login-form-componenet btn">Sign In</b-button>
    </form>
  </div>
</template>

<script>
import authService from "../../services/AuthService";
import Register from '../../components/LandingPage/Register';

export default {
  name: "page-login", //scott changed to "c"
  components: {
    Register
  },
  data() {
    return {
      showLogin: false,
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push(`/user/${this.user.username}`);
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
    setMainRegister(){
      this.$store.commit("HOME_MAIN_REGISTER");
    },
    setMainDescription(){
      this.$store.commit("HOME_MAIN_TITLE");
      this.$store.commit("USER_MAIN_VIEW_GAMES");
    },
    toggleShowLoginForm(){
      this.showLogin = !this.showLogin;
    },
    toggleRegister(){
      this.$store.commit("TOGGLE_REGISTER");
    },
    setUserMainComponentTutorial(){
      this.$store.commit("USER_MAIN_TUTORIAL")
    }
  },
  created(){
    this.showLogin = false;
    this.showRegister = false;
  }
  
};
</script>
<style scoped>
  *{
    color: black;
  }
  .text-center{
    font-size: 18px;
    height: 100%;
  }
  .flex-container{
    display: flex;
    align-items: center;
    flex-direction: column;
    padding: 6px 6px 6px 6px;
    backface-visibility: hidden;
  }
  .form-signin{
    display: flex;
    align-content: center;
    flex-direction: column;
    align-items: center;
  }
  .login-form-componenet{
    margin: 8px 8px 8px 8px;
  }
  .btn{
    border-radius: 12px;
    padding: 8px 20px;
  }
  .login-header{
    font-size: 26px;
  }
</style>