<template>
  <div id="register" class="register-display">
    <form class="form-register" @submit.prevent="register">
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <div class="register-form-component">
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
      
      <div class="register-form-component">
        <label for="password" class="sr-only">Password</label>
        <input
          type="password"
          id="password"
          class="form-control"
          placeholder="Password"
          v-model="user.password"
          required
        />
      </div>
      <div class="register-form-component">
        <label for="confirmPassword">Confirm Password</label>
        <input
          type="password"
          id="confirmPassword"
          class="form-control"
          placeholder="Confirm Password"
          v-model="user.confirmPassword"
          required
        />
      </div>
      <div class="register-form-component">
        <b-button variant="success" type="submit">
          Create Account
        </b-button>
      </div>
      <div class="register-form-component">
        <p>
          Best practice for creating a password includes such things as:
          the use of different types of characters like uppercase,
          lowercase and special characters,
          make your passowrd long,
          don’t share your password with others,
          change your password regularly.
        </p>
        <p>
          However, This is a fake stock trading game,
          and we do not have any of your personal information.
          So, make your password as simple as you like.
        </p>
        <p>
          Here are some examples: 12345, password, or simply copy your username.
        </p>
      </div>      
    </form>
  </div>
</template>

<script>
import authService from '../../services/AuthService';
export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              /*this.$router.push({
                path: '/',
                query: { registration: 'success' },
              });*/
              alert("Registration was successful!");
              this.$store.commit("HOME_MAIN_LOGIN");
              this.user = {
                username: '',
                password: '',
                confirmPassword: '',
                role: 'user',
              };
              this.$store.commit("TOGGLE_REGISTER");
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
    setMainLogin(){
      this.$store.commit("HOME_MAIN_LOGIN");
    },
  },
};
</script>

<style>
  .form-register{
    display: flex;
    flex-flow: column nowrap;
    align-items: center;
    overflow: hidden;
    backface-visibility: hidden;
  }
  .btn{
    border-radius: 12px;
    padding: 8px 20px;
  }
  .register-form-component{
    margin: 8px 8px 8px 8px;
  }
</style>
