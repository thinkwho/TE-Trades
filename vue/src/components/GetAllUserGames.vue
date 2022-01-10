<template>
    <div class="list-of-games">
        <div id="game-detail-container" v-on:mouseenter="setCurrentGame(game.id)" v-on:click="viewGame(game.is_Completed)" v-for="game  in GetAllGamesData" v-bind:key="game.id">
            <h2>{{game.name}} </h2>
            <p>Id: {{game.id}}</p>
            <p>Start Date: {{game.start_Date.substring(0,10)}}</p>
            <p>End Date: {{game.end_Date.substring(0,10)}} </p>
            <p>Completed: {{(game.is_Completed) ? "Yes" : "No"}}</p>
        </div>
    </div>  
</template>

<script>
import GameSQLAPIService from '../services/GameSQLAPIService.js';
import moment from 'moment'
export default {
    name: "get-all-user-games",
    data() {
        return {
            GetAllGamesData: [],
            
        }
    },
    created() {

        this.TestDate()
        GameSQLAPIService.getAllUserGames(this.$store.state.user.username).then(response =>{
            console.log(response.status)
            if(response.status == 200){
                this.GetAllGamesData = response.data;
            }
        }).catch((error) =>{
            alert("Error retreving games! " + error)
        });

        GameSQLAPIService.getActiveGames(this.$store.state.user.username)
        .then((response) => {
          if (response.data.length != 0){
           
            console.log(response.data);
            for(let i = 0; i < response.data.length; i++){
                console.log("End date: "+response.data[i].end_Date);
              if(response.data[i].end_Date < this.TestDate()){
                GameSQLAPIService.updateIsComplete(response.data[i].id)
                .then(response => {
                    console.log("Updating game status to complete. " + response.status);
                }).catch(error =>{
                    alert("error updating game!" + error)
                });
              }
            }
          }
          
        })
        .catch((error) => {
                alert("Error retrieving games. " + error); //Error Retrieving Data! Error: Network Error
        });
    },
    methods:{
        viewGame(is_Completed){
            if(is_Completed){
                this.$store.commit("USER_MAIN_VIEW_END_GAME");
            }else{
                this.$store.commit("USER_MAIN_VIEW_VIEW_GAME");
            }
            
        },
        setCurrentGame(id){
            this.$store.commit("SET_CURRENT_GAME", id);
        },
        getDate: function(){
            return new Date().toDateString();
        },
        TestDate(){
            let today = moment().toISOString();
            
            today=today.substring(0,11);
            today += '00:00:00';
            console.log(today);
            return today;
        },
        moment: function() {
            return moment();
        }
    }
    
}
</script>

<style>

div.list-of-games{
    display: flex;
    justify-content: space-evenly;
    box-sizing: border-box; 
    flex-wrap: wrap;
    flex-direction: row;
    margin-top: 6vh;
    border: double;
    margin-right: 1vh;
    margin-left: 1vh;
}
div#game-detail-container {
    display: flex;
    justify-content: space-evenly;
    text-align: center;
    padding: 12px 12px 12px 12px;
    flex-direction: column;
    border-style: double;
    background-color: #475070;
    border-color: #3E5CBD;
    border-radius: 10px;
    box-shadow: 10px 8px 5px rgba(0, 0, 0, .4);
    color: white;
    width: 300px;
    max-height: 400px;
    margin-top: 2vh;
    margin-bottom: 2vh;
}

#game-detail-container:hover {
    background: linear-gradient(#99AEF2, #253670);
    cursor: pointer;
}

/* commented this out as the mobile view in User.vue for 768px looks nicer 
/* @media screen and (max-width: 500px) {
      body {
        margin: 1rem;
      }

      div.list-of-games {
        flex-wrap: wrap;
        flex-direction: column;
      } 

      div#game-detail-container {
        width: 20%;
      }


} */
</style>