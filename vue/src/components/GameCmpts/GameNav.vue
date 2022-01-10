<template>
    <nav class="game-nav">
        <button class="game-nav--execute-trade btn" v-bind:disabled="isSubmitting" @click="ActiveScreen(1)">Execute Trade</button> <!--Buy AND Sell requirementS-->
        <button class="btn" @click="ActiveScreen(2)"> View Portfolio </button> <!--See Stock requirment-->
        <button class="btn"  @click="ActiveScreen(3)"> View Leaderboard </button> <!--leaderboard requirement-->
        <button class="btn" @click="ActiveScreen(4)"> View Chart </button>
    </nav>  
</template>

<script>
import GameSQLAPIService from '../../services/GameSQLAPIService.js';
import moment from 'moment';

export default {
    name: 'game-nav',
    data(){
        return{
            isSubmitting: false,
            game: {}
        }
    },
    created(){
        GameSQLAPIService.GetGameDetails(this.$store.state.currentGame).then(response => {
        this.game = response.data;
     
        if(response.data.start_Date > this.TestDate()){
            this.isSubmitting = true;
        } 
      });
    },
    methods: {
        ActiveScreen(num){
            this.$store.commit("SET_ACTIVE_SCREEN", num);
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
.btn{
    color: black;
}
.game-nav {
    color: black;
    display: flex;
    flex-wrap: nowrap;
    flex-direction: row;
    justify-content: space-evenly;
    padding: 1em;
    background-color: #3E5CBD;
    border-bottom: white 2px solid;
}

.game-nav > button {
    border: 0;
    box-shadow: 0 0.4em black;
    border-radius: .5em;
    background-image: linear-gradient(180deg, lightblue 95%, black 100%);
    transition: font-size .125s linear 0s;
}

.game-nav button:hover {
    color: green;
    font-size: 1.075em;
}

.game-nav > button:active {
    background-color: white;
    transform: translateY(0.1em);
    box-shadow: 0 0.3em #148;
}

</style>