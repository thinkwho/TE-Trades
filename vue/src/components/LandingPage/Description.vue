<template>
    <div class="container">
        <div class="header-display">Introduction</div><!--NAME-->
            <p class="body-text">
                Learning how to trade in the stock market can be intimidating,
                especially when it's your own money.
                Wouldn’t it be great if there were a way to trade stocks without the risk of losing your shirt? 
            </p>
            <p class="body-text ">
                TE Trades is a Virtual Stock Market,
                a gamified environment where you can hone your stock market skills.
                Challenge your friends and family to see who is the best trader.
                Careful, you may learn something along the way.
            </p>
            <p class="body-text">
                First you will need to login. Only registered users can play TE Trades.
                Once logged in, users can create a game and invite other users to play.
                Each of the players start with 100,000 virtual dollars.
                The creator of the game will set an end date and time; a month, a week, a fortnight, a single day.
                It’s up to the creator.
                When the time runs out, all stocks are sold and the player with the highest dollar value wins.
            </p>
    </div>
</template>

<script>
export default {
    name: 'description'
}
</script>

<style scoped>
    /*flex the div*/
    .container{
       flex-direction: column;
       backface-visibility: hidden;
    }
    div.header-display{
        text-align: left;
        font-size: 32px;
        animation: movedown 1s ease-out;
    }
    div.header-display:first-child{
        padding-top: 5vh;
    }
    p.body-text{
        text-align: left;
        font-size: 18px;
        padding-left: 5vh;
        padding-right: 5vh;
        animation: fadein 1s ease-out;
    }
    
    @keyframes movedown {
        0% {
            opacity: 0;
            transform: translateY(-10px);
        }
        100% {
            opacity: 100;
            transform: translateY(0px);
        }
    }
    @keyframes fadein {
        0% {
            opacity: 0;
        }
        100% {
            opacity: 100;
        }
    }

</style>