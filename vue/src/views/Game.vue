<template>
    <div>
           
        <div class="game-container">
  
            <div class="game-nav--grid">
                <nav class="game-nav">
                    <!--<button class="game-nav--execute-trade" @click="ActiveScreen(1)"> Execute Trade </button> <Buy AND Sell requirementS-->
                    <button class="game-nav--execute-trade" @click="ActiveScreen(1)">Execute Trade</button> <!--Buy AND Sell requirementS-->
                    <button @click="ActiveScreen(2)"> View Portfolio </button> <!--See Stock requirment-->
                    <button @click="ActiveScreen(3)"> View Leaderboard </button> <!--leaderboard requirement-->
                    <button @click="ActiveScreen(4)"> View Chart </button>
                </nav>
            </div>
            
            <game-info class="game-info"/>
            <!--
            <aside class="game-info"> 
                <h2>Game Info</h2>
                <p>Name: {{currentGame.name}} </p>
                <p>Organizer: {{currentGame.organizer}}</p>
                <p>Start Date: {{currentGame.start_Date}}</p>
                <p>End Date: {{currentGame.end_Date}}</p>
            </aside>
            -->

            <div class="game-main">
                
                <div v-if="$store.state.activeScreen===1">
                    <game-trade />
                </div>
                
                <div v-if="$store.state.activeScreen === 2">
                    <game-portfolio />
                </div>

                <div v-if="$store.state.activeScreen===3">
                    <game-leaderboard />
                </div>

                <div v-if="$store.state.activeScreen===4">
                    <stock-chart />
                </div>
            </div>
        </div>
    </div>
</template>

<script> 
import GameSQLAPIService from '../services/GameSQLAPIService.js';

import GameBalance from '../components/GameCmpts/GameBalance.vue';
import GameInfo from '../components/GameCmpts/GameInfo.vue';
import GameTrade from '../components/GameCmpts/GameTrade.vue';
import GamePortfolio from '../components/GameCmpts/GamePortfolio.vue';
import GameLeaderboard from '../components/GameCmpts/GameLeaderboard.vue';

import StockChart from '../components/StockCmpts/StockChart.vue';

//import GameHeader from './GameHeader.vue';
//import Portfolio from './Portfolio.vue';
//import TradeStock from '../components/TradeStock.vue';

export default {
    components: {  GameTrade, GamePortfolio, GameLeaderboard, StockChart, GameBalance, GameInfo},
    name: 'game-view',
    data(){
        return {
            currentGame: {},
            //ActiveScreen: 4
        }
    },
    methods: {
        ActiveScreen(num){
            this.$store.commit("SET_ACTIVE_SCREEN", num);
        }
    },
    created() {
        GameSQLAPIService.GetGameDetails(this.$store.state.currentGame)
        .then( (response) => {
            this.currentGame = response.data;
        })
        .catch( (error) => {
            alert("cannot get game details \n" + error);
        });
    }
}

</script>

<style>

*,
::before,
::after {
    box-sizing: border-box;
}

:root {
    font-size: .825rem;
    font-family: "fira sans";
}

.testing {
    border-color: black;
    border-width: 1px;
    border-style: solid;
    margin: 1em;
}

.game-container {
    border-style: solid;
    border-color: black;

    display: grid;
    grid-template-rows: repeat(3, auto);
    grid-template-areas: 
        "info"
        "nav"
        "main";
    gap: 1em;
}

.game-nav--grid {
    grid-area: nav;
}

.game-nav {
    display: flex;
    justify-content: space-evenly;
    flex-wrap: nowrap;
    padding: 1em;
}

.game-nav > button {
    border-radius: .5em;
    background-image: linear-gradient(180deg, #d3d3d3 90%, black 100%);
    transition: font-size .125s linear 0s;
}

.game-nav button:hover {
    color: green;
    font-size: 1.075em;
}

.game-info {
    grid-area: info;
    text-align: center;
    padding: 1em;
    margin: .225em;

    border-radius: .5em;
    border-style: dashed;
    border-color: blue;
    border-width: 1px;

}

.game-main {
    grid-area: main;
    border-style: dashed;
    border-color: red;
    border-width: 1px;
    border-radius: .5em;
}

@media screen and (min-width: 429px){
    :root {
        font-size: 1rem;
    }

    .game-container {
        grid-template-rows: repeat(2, auto);
        grid-template-columns: repeat(3, auto);
        grid-template-areas: 
            "nav nav nav"
            "info main main";
    }

    

}


</style>