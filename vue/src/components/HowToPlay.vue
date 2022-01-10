<template>
    <div class="container">
        <div class="header-display">How to Play Yuppie</div>
        <p class="body-text">
            First you will need to login. Only registered users can play Yuppie.
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
    name: 'how-to-play'
}
</script>

<style scoped>
    .container{
       flex-direction: column;
       display: flex;
    }
    div.header-display{
        text-align: left;
        font-size: 24px;
        animation: movedown 1s ease-out;
    }
    div.header-display:first-child{
        padding-top: 2vh;
    }
    p.body-text{
        text-align: left;
        font-size: 16px;
        padding-left: 1vh;
        padding-right: .5vh;
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