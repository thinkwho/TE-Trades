<template>
    <div class="list-of-games">
        <div id="game-detail-container" v-for="game in GetAllGamesData" v-bind:key="game.id"
        @click="GoToGame(game)">

            <h2>{{game.name}}</h2>
            <p>Id: {{game.id}}</p>
            <p>Start Date: {{game.start_Date.substring(0,10)}}</p>
            <p>End Date: {{game.end_Date.substring(0,10)}} </p>
            <p>Winner: {{(game.winner == "") ? "Undecided" : game.winner}}</p>
            <p>Completed: {{(game.is_Completed) ? "Yes" : "No"}}</p>

        </div>
    </div>  
</template>

<script>
import GameSQLAPIService from '../services/GameSQLAPIService.js';
export default {
    name: "get-all-games",
    data() {
        return {
            GetAllGamesData: [],
        }
    },
    methods: {
        GoToGame(game) {
            this.$store.commit("SET_GAME", game);

            this.$router.push({ path: `/game/${game.id}`});
        }
    },

    created() {
        GameSQLAPIService.getListOfGames().then(response =>{
            if(response.status == 200){
                this.GetAllGamesData = response.data;
            }
        }).catch((error) =>{
            alert("Error retreving games! " + error)
        });
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
    margin-top: 10vh;
    margin-bottom: 5vh;
    border: double;
    margin-right: auto;
    margin-left: auto;
}
div#game-detail-container {
    display: flex;
    justify-content: space-evenly;
    text-align: center;
    padding: 12px 12px 12px 12px;
    flex-direction: column;
    border-style: double;
}

div#game-detail-container:hover {
    cursor: pointer;
}

@media screen and (max-width: 500px) {
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
}
</style>