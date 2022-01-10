<template>
  <div class="entire-container">

    <div class="grid">
      <top-display class="header"/>
      <get-all-user-games class="body" v-if="$store.state.userMainComponent == 'view-games'" />
      
      <create-game class="body" v-if="$store.state.userMainComponent == 'create-game'" />
      <tutorial class="body" v-if="$store.state.userMainComponent == 'tutorial'" />
      
      <game class="body" v-if="$store.state.userMainComponent == 'game-view'" />
      <end-game class="body" v-if="$store.state.userMainComponent == 'end-game'" />
      <page-footer class="footer" />
    </div>

  </div>
</template>

<script>

import TopDisplay from '../components/TopDisplay.vue';
import PageFooter from '../components/PageFooter.vue';
import Tutorial from '../components/Tutorial';
import CreateGame from '../components/CreateGame.vue';
import GetAllUserGames from '../components/GetAllUserGames.vue';
import Game from '../components/Game.vue';
import GameSQLAPIService from '../services/GameSQLAPIService.js';
import EndGame from '../components/EndGame.vue'

export default {
  name: 'user',
  data() {
    return {
      
    }
  },
  components:{
    TopDisplay,
    PageFooter,
    CreateGame,
    Tutorial,
    GetAllUserGames,
    Game,
    EndGame
  },
  methods: {
  },
  created() {
      // GameSQLAPIService.getActiveGames(this.$store.state.user.username)
      //   .then((response) => {
      //     if (response.data.length != 0){
      //       this.activegames = response.data;
      //       console.log(this.activegames);
            
      //       if(response.data[0].end_Date){
      //         GameSQLAPIService.updateIsComplete(response.data[0].id)
      //         .then(response => {
      //           console.log("Updating game status to complete. " + response.status);
      //         }).catch(error =>{
      //             alert("error updating game!" + error)
      //         });
      //       }
      //     }
          
      //   })
      //   .catch((error) => {
      //           alert("Error retrieving games. " + error); //Error Retrieving Data! Error: Network Error
      //   });
  
      GameSQLAPIService.getActiveGames(this.$store.state.user.username)
        .then((response) => {
          console.log("here is the activegames", response.data)
          this.activegames = response.data;
        
      
        })
        .catch(() => {
                alert("Error retrieving games. "); //Error Retrieving Data! Error: Network Error
        });
    
    // this.activegames.foreach(game => {
    //     if(game.end_Date < Date()){
    //       GameSQLAPIService.updateIsComplete(this.game.game_Id)
    //         .then(response => {
    //           if (response.data != 200){
    //             alert("error updating game!")
    //           }
    //         }).catch(error =>{
    //             alert("error updating game!" + error)
    //         });
    //     }
    //   });
    

  }
}
</script>

<style>
.entire-container{
  background-color: #3E5CBD;
}
.body {
  grid-area: body;
  
  background-image: linear-gradient(150deg, #253670 0% , white 100%);
}

.header{
  grid-area: header;
  background-color: #253670;
}

.footer{
  grid-area: footer;
}

.grid{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    grid-template-rows: .1fr 2fr .2fr;
    height: 100vh;
    grid-template-areas: 
    "header header header"
    "body body body"
    "footer footer footer";
}
@media screen and (max-width: 1279px){
    #grid{
        grid-template-columns: 1fr 1fr;
        grid-template-rows: .1fr 2fr .2fr;
        grid-template-areas:
        "header header"
        "body body"
        "body body"
        "footer footer";
    }
}
@media screen and (max-width: 768px){
    #grid{
        grid-template-columns: 1fr;
        grid-template-rows: .1fr 2fr .2fr;
        grid-template-areas:
        "header"
        "body"
        "body"
        "footer";
    }
}
</style>