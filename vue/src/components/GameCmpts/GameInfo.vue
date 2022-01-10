<template>
    <div>
        <aside> 
            <h2>Game Info</h2>
            <p>Name: {{currentGame.name}} </p>
            <p>Organizer: {{currentGame.organizer}}</p>
            <p>Start Date: {{currentGame.start_Date.substring(0,10)}}</p>
            <p>End Date: {{currentGame.end_Date.substring(0,10)}}</p>
        </aside>
    </div>
</template>

<script>
import GameSQLAPIService from '../../services/GameSQLAPIService.js'
export default {
    name: 'game-info',
    data() {
        return {
            currentGame: {}
        }
    },
    created() {
        console.log(this.$store.state.currentGame)
        GameSQLAPIService.GetGameDetails(this.$store.state.currentGame)
            .then( (response) => {
                this.currentGame = response.data;
            })
            .catch( (error) => {
                alert("Cannot retrieve game details \n" + error);
            });
    }
}
</script>

<style scoped>
*{
    font-family: 'fira sans';
    color: white;
}
</style>