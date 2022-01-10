<template>
    <div class="header">
        <div id="stock-ticker" v-for="stock in optionChain" v-bind:key="stock.symbol">  
            <div class="stock"> <span>{{ stock.symbol }}</span> <span>{{`$${stock.price}`}}</span> <span>{{stock.change}}</span> <span>{{stock.changesPercentage}}</span> </div>
      </div>
    </div>
</template>

<script>
import stockAPIService from '../services/StockAPIService.js';

export default {
    name: 'stocks',
    components: {
        
    },
    data() {
      return{
        optionChain: [],
        };  
    },
    created () {
        stockAPIService.getCompanyQuote('AAPL', 'FB', 'GOOG', 'NFLX').then(response =>{
            console.log(response.status);
            this.optionChain = response.data;
        }).catch((error) => {
            alert("Error Retreving Data! " + error);
        }); 
    } 
}    
</script>

<style scoped>
.header{
    display: flex;
    flex-flow: row nowrap;
    justify-items: center;
    justify-content: space-around;
    align-items: flex-start;
    border: solid;
}
.stock{
    border: 2px solid black;
    margin: 8px 8px 8px 8px;
    text-align: center;
    padding: 5px 5px 5px 5px;
    display: flex;
    flex-direction: column;
    font-size: 18px;
}
</style>