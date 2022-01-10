<template>
    <div class="form-container" >
        <!--
            name
            set start time
            set end time
            invite player - we will need a list of players
        -->

        <b-form class="form-create-game" @submit.prevent="createGame"> <!--createGame func to make a game-->
            <h1 class="h3 mb-3 font-weight-normal">Create Game</h1>
            <div class="alert alert-danger" role="alert" v-if="createGameErrors"> <!--createGameErrors-->
                {{ createGameErrorsMsg }}
            </div>
            <div class="form-component">
                <label for="Name" class="sr-only">Game Name: </label>
                <b-form-input
                v-on:change="readyToSubmit"
                    type="text"
                    id="gameName"
                    class="form-control"
                    placeholder="NameOfGame"
                    v-model="game.Name"
                    required
                    autofocus
                />
            </div>
            
            <!--datetime-local is formatted YYYY-MM-DDThh:mm-->
            <div class="form-component">
                 <label for="startDate" class="sr-only">Start Date: </label>
                <b-form-datepicker
                
                    @input="readyToSubmit"
                    id="startDate"
                    class="form-control"
                    placeholder="startDate"
                    v-model="game.Start_Date"
                    required
                />
            </div>
            <div class="form-component">
                <label for="endDate"  class="sr-only">End Date: </label>
                <b-form-datepicker
                    
                   @input="readyToSubmit();" 
                    type="date"
                    id="endDate"
                    class="form-control"
                    placeholder="endDate"
                    v-model="game.End_Date"
                    required
                />
            </div>
            <div v-if="dateErrorCheck">
                <p class="date-error">End Date Must Be Later Than Start Date</p> 
            </div>
           <!-- <div v-if="startDateErrorCheck">
                <p class="start-date-error">Start Date Must Not Be Earlier Than The Current Date</p>
            </div> /-->
            <div class="form-component">
                <label for="invitePlayer" class="sr-only">Invite Players:</label><br>
                <b-form-select multiple v-model="game.Players" name="users" id="invitePlayer">
                    <b-form-select-option :value="user.userId" v-for="user in game.AllPlayers" v-on:click="readyToSubmit" v-bind:key="user.id">{{user.username}}</b-form-select-option>
                </b-form-select>
            </div>
            <div class="form-component">
                <b-button v-bind:disabled="isSubmitting" variant="success" type="submit">
                    Create Game
                </b-button>
            </div>
        </b-form>
    </div>
</template>

<script>
//import CViewGames from "../components/CViewGames";
import GameSQLAPIService from "../services/GameSQLAPIService";
import UserSQLAPIService from "../services/UserSQLAPIService.js";
export default {
    name: 'create-game',
    data() {
    return {
      isSubmitting: true,
      game: {
        Name: '', //change once we have database
        Organizer: this.$store.state.user.username,
        Start_Date: '', //can use Date.parse() form html to js... change with database
        End_Date: '',
        isCompleted: 0,
        Players: [],
        AllPlayers: [],
      },
      createGameErrors: false,
      createGameErrorsMsg: 'There were problems creating this game.',
    };
  },
    created(){
        UserSQLAPIService.getUsers().then(response =>{
            console.log(response.data);
            this.game.AllPlayers = response.data;
        });
    },
    methods: {
        createGame(){
            
            //this.game.Players.Push(this.$store.state.user.username);
            GameSQLAPIService.addNewGame(this.game).then(response => {
                console.log(response.status)
                this.$store.commit("USER_MAIN_VIEW_GAMES");
                if (response.status === 201) {
                    this.game = {};
                    
                }
            }).catch(error => {
                alert(error.response)
            });
            //just make this show game a game
            //do we need a new component? Seems like it would be game navigation.
            //
            //we dont have a game/player SQL database yet
        },
        readyToSubmit(){
            if(this.game.Name && this.game.Start_Date && this.game.End_Date && this.game.Players.length != 0 && !this.dateErrorCheck)  
            {
                this.isSubmitting = false;
            }
            else{
                this.isSubmitting = true;
            }
        },
        setViewGames(){
            this.$store.commit("USER_MAIN_VIEW_GAMES");
        }
    },
    computed: {
       dateErrorCheck(){
            return (this.game.Start_Date > this.game.End_Date)
        },
        
        
    }
}
</script>

<style scoped>
*{
    color: black;
}
.form-create-game{
    display: absolute;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background-color: #4F75F0;
    border: solid 4px #99AEF2;
    margin-top: 8vh;
    margin-left: 20vh;
    margin-right: 20vh;
    padding: 20px 20px 20px 20px;
    animation: zoomin .5s ease-in;
}
.form-component{
    align-content: flex-start;
}
.date-error{
    color: darkblue;
}
#invite-player{
    color: darkblue;
}
@keyframes zoomin{
    from{
        transform: scale(0);
    }
    to{
        transform: scale(1);
    }
}
@media screen and (max-width: 429px){
    .form-create-game{
        margin-left: 1vh;
        margin-right: 1vh;
    }
}
</style>