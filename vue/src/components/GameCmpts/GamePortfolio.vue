<template>
    <div>
        <h2>Balance: <span class="money">${{balance}}</span></h2>
        <div class = "game-portfolio">
            
            <div id="portfolio-of-player-owned-stocks" v-for="p in portfolios" v-bind:key ="p.stockSymbol"> <!-- @click="ViewPortfolio(id) !-->
                <p> Stock: {{p.stockSymbol}} {{p.stockName}} </p>
                <p> Shares Owned: {{p.quantity}} </p>
            </div>
            <p v-show="isPortfolioEmpty">Buy or sell stock to populate your portfolio!</p>
        </div>
    </div>
</template>

<script>
import PortfolioSQLAPIService from '../../services/PortfolioSQLAPIService.js';



export default {
    name: 'game-portfolio',
    data() {
        return {
            portfolioId: '',
            portfolios: '',
            balance: this.getBalance(),
            isPortfolioEmpty: true
        }
    },
    methods: {
        
        viewPortfolio(){
            PortfolioSQLAPIService.GetPortfolioById(this.portfolioId)
            .then(response => {
                console.log(response.status);
                console.log(response.data);
                if (response.data.length != 0){
                    this.isPortfolioEmpty = false;
                }
                this.portfolios = response.data;
            }).catch((error) => {
                alert("Error Retrieving Data! " + error);
            })
        },
         getBalance(){
            PortfolioSQLAPIService.GetGameBalance(this.$store.state.user.username, this.$store.state.currentGame)
            .then( (response) => {
               this.balance = response.data;
            })
            .catch( (error) => {
                alert(" warning " + error);
            });
        },       
    },
    computed:{
       
    },
    created(){
        PortfolioSQLAPIService.GetPortfolioId(this.$store.state.currentGame, this.$store.state.user.username)
        .then(response => {
            this.portfolioId = response.data;
            this.portfolios = this.viewPortfolio();
        }).catch((error) => {
                alert("Error Retrieving Data! " + error);
        })
    }
}
</script>

<style scoped>
*{
    color: white;
}
.money{
    color: darkgreen;
}
</style>