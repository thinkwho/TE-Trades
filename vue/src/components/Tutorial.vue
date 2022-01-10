<template>
    <div>
        <b-nav class="sub-header" tabs>
            <b-nav-item v-bind:active="checkActivePage(1)" v-on:click="setDisplayPage(1)">Introduction</b-nav-item>
            <b-nav-item v-bind:active="checkActivePage(2)" v-on:click="setDisplayPage(2)">Navigation</b-nav-item>
            <b-nav-item v-bind:active="checkActivePage(3)" v-on:click="setDisplayPage(3)">Games</b-nav-item>
            <b-nav-item v-bind:active="checkActivePage(4)" v-on:click="setDisplayPage(4)">Objective</b-nav-item>
        </b-nav>
        <div v-show="displayPage === 1">
            <b-alert show variant="light">
                    <p class="text-header">
                        Welcome to TE Trades!
                    </p>
                    <p class="text-body">
                        This is TE Trades! Here is the perfect place to learn how to trade stocks safely. 
                        We will show you the ins and outs of this website and how to play!
                    </p>
                </b-alert>
        </div>
        <div v-show="displayPage === 2">
            <b-alert show variant="light">
                    <p class="text-header">
                        View Games
                    </p>
                    <p class="text-body">
                        In this header tab we can see all the games that you are apart of! Click on the game you would like to join and you'll get to trading!
                    </p>
                    <p class="text-header">
                        Create Games
                    </p>
                    <p class="text-body">
                        This tab will display a form for you to fill out. Once all information is collected, you can click on create game! Be sure to select yourself so 
                        you'll be in the game too. Once you're done, you'll see your game in the View Games tab.
                    </p>
                    <p class="text-header">
                        Tutorial
                    </p>
                    <p class="text-body">
                        This is the tab you're in now. Use the lighter blue header to see what each section does.
                    </p>
                    <p class="text-header">
                        Log-Out
                    </p>
                    <p class="text-body">
                        You will be logged out and be sent back to the landing page!
                    </p>
                </b-alert>
        </div>
        <div v-show="displayPage === 3">
            <b-alert show variant="light">
                    <p class="text-header">
                        Game Navigation
                    </p>
                    <p class="text-body">
                        <ul>
                            <li><b>Exectue Trade: </b>This will allow you to search in Stock Symbols [example: AAPL for Apple Inc] When you click the "Search for Stock Symbol"
                             it will give the Current price, name, and symbol. You can now choose to buy or sell stock and the quantity. Once completed you can click "Execute Trade"</li>
                            <li><b>View Portfolio: </b>You can see the stock name and quantity of the stocks you purchased!</li>
                            <li><b>View Leaderboard: </b>Here you can see the total worth of you and other players. Total worth means your current balance plus the money you invested in stocks </li>
                            <li><b>View Chart: </b>Allows you the see a chart of whatever stock you'd like to see. Clicking on the top left of the chart will allow you to change what stock to see. 
                            Use this to look up stock symbols for executing trades!</li>
                        </ul>
                    </p>
                    <p class="text-header">
                        The Need-to-Knows
                    </p>
                    <p class="text-body">
                        <ul>
                            <li><b>This game uses real stocks!</b> So the worth of them will change in real time be sure to check while the stock market is open.</li>
                            <li><b>Fake money.</b> In this game, you will be handling with fake money. So don't worry about anything and take some risks and learn from your mistakes!</li>
                        </ul>
                    </p>
                </b-alert>
        </div>
        <div v-show="displayPage === 4">
            <b-alert show variant="light">
                    <p class="text-header">
                        What to Expect
                    </p>
                    <p class="text-body">
                        <ul>
                            <li>Games will last until the set end date. At midnight the game will automatically close and you won't be able to buy or sell stocks. All stocks in your 
                                portfolio will be sold and the player with the most money will win!
                            </li>
                            <li>Each player will start out with $100,000 and be able to jump right into buying and selling stocks!</li>
                            <li>Research stocks using the stock chart to optimize your profits!</li>
                        </ul>
                    </p>
                </b-alert>
        </div>
        <div>
            <img src="../../img/Stonks.gif" alt="Stock Picture" class="center"> 
        </div>
    </div>
</template>

<script>
export default {
    name: "tutorial",
    data(){
        return {
            displayPage: 1,
        }
    },
    methods:{
        setDisplayPage(num){
            this.displayPage = num;
        },
        checkActivePage(num){
            if (this.displayPage === num){
                return true
            }
            return false
        }
    }
}
</script>

<style scoped>
    *{
        color: black !important;
        font-family: "fira sans", Arial;
    }
    .sub-header{
        background-color: #4F75F0;
    }
    .text-header{
        font-size: 26px;
        top:0;
        left:0;
        margin:0;
        animation: zoomby 1s ease-in;
    }
    .text-body{
        font-size: 18px;
        margin-left: 16px;
        animation: fadein 1.4s ease-in;
    }
    .tutorial-container{
        display: flex;
        flex-direction: column;
        text-align: center;
        margin-top: 1rem;
    }
    ul, ol{
        text-align: left;
    }
    .t-header{
        font-size: 20px;
    }
    .center{
        display: block;
        margin-left: auto;
        margin-right: auto;
    }
    @keyframes zoomby{
        0%{
            opacity: .5;
            transform: scale(0.1);
        }

        80%{
            opacity: 1;
            transform: scale(1.02);
        }
        100%{
            opacity: 1;
            transform: scale(1);
        }
    }
    @keyframes fadein {
        from {
            opacity: 0;
        }
        to {
            opacity: 1;
        }
    }
</style>