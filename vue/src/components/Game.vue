<template>
    <div>   
        <div class="game-container">

            <game-nav class="game-nav"/>

            <game-info class="game-info"/>

            <!--<game-balance class="game-balance"/>-->

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
import GameNav from '../components/GameCmpts/GameNav.vue';
import GameInfo from '../components/GameCmpts/GameInfo.vue';
import GameTrade from '../components/GameCmpts/GameTrade.vue';
import GamePortfolio from '../components/GameCmpts/GamePortfolio.vue';
import GameLeaderboard from '../components/GameCmpts/GameLeaderboard.vue';
//import GameBalance from '../components/GameCmpts/GameBalance.vue';

import StockChart from '../components/StockCmpts/StockChart.vue';

export default {
    name: 'game-view',
    components: {  
    GameNav
    , GameTrade
    , GamePortfolio
    , GameLeaderboard
    , GameInfo
    , StockChart
    //, GameBalance
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
    color: white;
}

.game-container {
    
    background-image: linear-gradient(150deg, #253670 0% , white 100%);
    /*
    border-style: solid;
    border-color: black;*/

    display: grid;
    grid-template-rows: repeat(4, auto);
    grid-template-areas: 
        "info"
        "balance"
        "nav"
        "main";
    gap: 1em;
}

.game-nav {
    grid-area: nav;
}

.game-info {
    grid-area: info;
    text-align: center;
    padding: 1em;
    margin: .225em;

    border-radius: .5em;
    border-style: double;
    border-color: #253670;
    border-width: 8px;
    background-image: linear-gradient(#4F75F0, #99AEF2);
    box-shadow: 12px 12px 2px 1px rgba(64, 64, 146, 0.2);;
}

.game-balance {
    grid-area: balance;
   /* border-radius: .5em;
    border-style: dashed;
    border-color: lightgreen;
    border-width: 1px;*/
}

.game-main {
    grid-area: main;
    border-style: double;
    border-color: #253670;
    border-width: 8px;
    border-radius: .5em;
    padding: 18px 18px 18px 18px;
    background-image: linear-gradient(#4F75F0, #99AEF2);
    box-shadow: 12px 12px 2px 1px rgba(64, 64, 146, 0.2);;
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
            "info main main"
            "balance main main";
    }
}
</style>