<template>
  <div>

      <header>
         <!-- <h2>Balance: ${{Balance}} </h2> -->
      </header>

  </div>
</template>

<script>
import PortfolioSQLAPIService from '../../services/PortfolioSQLAPIService.js'

export default {
    name: 'GameBalance',
    data() {
        return {
            Balance: this.GetBalance()
        }
    },
    methods: {
    GetBalance(){
        PortfolioSQLAPIService.GetGameBalance(this.$store.state.user.username, this.$store.state.currentGame)
        .then( (response) => {
            this.Balance = response.data;
        })
        .catch( (error) => {
            alert(" cannot get balance " + error);
        });
    }
    },
    created(){
        this.Balance=this.GetBalance();
    }
}
</script>

<style scoped>
    
</style>