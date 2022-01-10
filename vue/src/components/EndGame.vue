<template>
    <div>
        <b-alert variant="success" show>
            <h1>END GAME!</h1>
                        <img src="../../img/Loading.gif" alt="Loading" v-if="isLoading" />
                        <p v-if="isLoading">Please wait</p>
            <ol>
                <span class="row" v-for="player in players" v-bind:key="player.username">
                <li>
                    <div>{{player.username}}</div>
                    <div>${{player.balance}}</div>
                </li>
                </span>
            </ol>
        </b-alert>
    </div>
</template>

<script>
import GameSQLAPIService from '../services/GameSQLAPIService.js';
//import GameLeaderboard from '../GameCmpts/GameLeaderboard.vue';

export default {
    name: 'end-game',
    components: {

    },
     data(){
        return {
            currentGame: {},
            players: [],
            isLoading: true
        }
    },
    methods: {
        ActiveScreen(num){
            this.$store.commit("SET_ACTIVE_SCREEN", num);
        },
        GetFinalLeaderboard(){

        }
    },
    created(){
        GameSQLAPIService.GetGameDetails(this.$store.state.currentGame).then(response =>{
           this.currentGame = response.data;
        }).catch((error) => {
            alert("Error Retrieving Data! " + error);
        });
        GameSQLAPIService.getLeaderboard(this.$store.state.currentGame, this.currentGame.end_Date).then(response => {
                response.data.forEach(player => {
                    let tempArr = player.split(',');
                    let p = {
                        username: tempArr[0],
                        balance: tempArr[1]
                    };
                    this.players.push(p);
                });
            }).catch((error) => {
                alert("Error Retrieving Data! " + error);
        }).then(() => {
            this.isLoading = false;
        });
    }
}
</script>

<style scoped>

</style>