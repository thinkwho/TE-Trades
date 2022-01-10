<template>
  <div> 
    <div class="leaderboard-border">
      <ol>
        <span class="row" v-for="player in players" v-bind:key="player.username">
          <li>
            <div>{{player.username}}</div>
            <div class="money">${{player.balance}}</div>
          </li>
        </span>
          
      </ol>
    </div>
  </div>
</template>

<script>
import GameSQLAPIService from '../../services/GameSQLAPIService';
export default {
    name: "game-leaderboard",
    data() {
      return {
        players: [],
        game: {}
      }
    },
    methods: {
     
    },
    created(){
      GameSQLAPIService.getLeaderboard(this.$store.state.currentGame, "yuppie").then(response => {
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
      });
      GameSQLAPIService.GetGameDetails(this.$store.state.currentGame).then(response => {
        this.game = response.data;
      });
    }
}
</script>

<style>

*,
::before,
::after {
  box-sizing: border-box;
  font-family: 'fira sans', Arial;
}

.leaderboard-border {
  /*border-style: solid;
  border-color: green;*/

  width: 100%;

  display: table;
}
.money{
  color: darkgreen;
}
.row {
  display: flex;
  flex-direction: row;
}


</style>