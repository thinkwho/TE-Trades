<template>
      <div>

            <!--
            <div class="logo">
                  <img class="image-logo" src="../../img/Yuppie.png" alt="Yuppie Tech Elevator Logo">
            </div>
            -->

            <b-nav tabs is-nav-bar fill align="center">
                  <!--<b-nav-item><b-button variant="primary" v-on:click="showGames()">View Games</b-button></b-nav-item>
                  <b-nav-item><b-button variant="primary" v-on:click="showCreateGame()">Create Games</b-button></b-nav-item>
                  <b-nav-item><b-button variant="primary" v-on:click="showTutorial()">Tutorial</b-button></b-nav-item>
                  <b-nav-item ><b-button variant="primary" v-on:click= logout>Log-Out</b-button></b-nav-item>    -->
                  <b-nav-item v-on:click="showGames()">View Games</b-nav-item>
                  <b-nav-item v-on:click="showCreateGame()">Create Games</b-nav-item>
                  <b-nav-item v-on:click="showTutorial()">Tutorial</b-nav-item>
                  <b-nav-item v-on:click= logout>Log-Out</b-nav-item>
            </b-nav>
            
      </div>

</template>

<script>
export default {
      name: "top-display",
      
      data() {
            return {
                  
            }
      },
      methods: {
            showGames() {
                 // isGameOver();
                  this.$store.commit("USER_MAIN_VIEW_GAMES");
            },
            showTutorial(){
                  this.$store.commit("USER_MAIN_TUTORIAL");
            },
            showCreateGame(){
                  this.$store.commit("USER_MAIN_CREATE_GAME");
            },
            logout(){
                  this.$store.commit("LOGOUT");
                  this.$router.push("/");
            },
            // isGameOver(){

            // }
      }
}
</script>

<style scoped>
      *{
            font-family: "fira sans";
            font-weight: 400;
            color: white !important; 
      }
      .user-header-container{
            display: flex;
            flex-direction: row;
            flex-wrap: nowrap;
            justify-content: flex-end;
      }

      .header-option{ 
            border-radius: 12px;
            background-color: #0d6efd;
            border-color: #0d6efd;
            height: 60px;
      }

</style>