<template>
  <div>
      <h2>Balance: <span class="money">${{balance}}</span></h2>
      <h2>Execute Trade</h2>
      <form class="trade-form" v-on:submit.prevent.stop="executeTrade">
          <div>
                <button type="button" v-on:click="GetStockPrice(); getActiveStocks(); clearBuySellForm()"> Search for Stock Symbol: </button>
                <input type="text" placeholder="Search Stock" v-on:change="readyToExecuteTrade()" v-model="stockSymbol"/>
            </div>

            <div class="stock-info">
                <div v-for="s in stock" v-bind:key="s.id" >
                    <p>Current Price: ${{s.price}}</p>
                    <p>Name: {{s.name}}</p>
                    <p>Symbol: {{s.symbol}}</p>
                    
                </div>             
            </div>
            
            <div v-if="showBuySell">
                <p> Type: Buy / Sell </p>
                <select v-on:change="readyToExecuteTrade()" v-on:click="getActiveStockMatchIndex();" v-model="trade.trade_type">
                    <option value="0">Buy</option>
                    <option value="1">Sell</option>
                </select>
            </div>
            
            <div v-if="showBuySell">
                <label id="Quantity">Quantity</label>
                <input for="Quantity" type="number" min="0"  v-on:change="readyToExecuteTrade()" v-model="trade.quantity"/>
            </div>

            <div v-if="showBuySell">
                Total Trade Amount: {{trade.total_trade_amount}}
            </div>

            <button v-if="showBuySell" type="submit" v-bind:disabled="isSubmitting" >Execute Trade</button>

        </form>

    </div>
</template>

<script>
import PortfolioSQLAPIService from '../../services/PortfolioSQLAPIService';
import StockAPIService from '../../services/StockAPIService';
import TradeSQLAPIService from '../../services/TradeSQLAPIService';

export default {
    name: 'trade-search',
    data(){
        return {
            isSubmitting: true,
            stockSymbol: '',
            stock: '',
            trade: {
                portfolio_id: '',
                stock_symbol: '',
                stock_name: '',
                stock_price: '',
                trade_type: '',
                quantity: '',
                get total_trade_amount() {
                    return this.stock_price * this.quantity
                }
            },
            showBuySell: false,
            activeStocks: '',
            activeStockMatchIndex: -1,
            balance: this.getBalance()
            
        }
    },
    created(){
        PortfolioSQLAPIService.GetPortfolioId(this.$store.state.currentGame, this.$store.state.user.username)
        .then(response => {
            this.trade.portfolio_id = response.data;
        }).catch((error) => {
            alert("Error Retrieving Data! " + error);
        })
    },
    methods: {
        GetStockPrice(){
            StockAPIService.getStockQuote(this.stockSymbol.toUpperCase())
            .then((response) => {
                console.log(response.status)
                console.log(response.data)
                this.stock = response.data;
                this.trade.stock_symbol = this.stock[0].symbol
                this.trade.stock_name = this.stock[0].name
                this.trade.stock_price = this.stock[0].price
                this.showBuySell = true;
            })
            .catch(() => {
                alert("Please input a valid Stock Symbol. "); //Error Retrieving Data! Error: Network Error
            });
            
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

        getActiveStocks(){
            PortfolioSQLAPIService.GetPortfolioById(this.trade.portfolio_id)
            .then(response =>{
            this.activeStocks = response.data;
            })
            .catch((error) => {
                alert("Error Retrieving Data! " + error);
            })
        },

        getActiveStockMatchIndex(){
             
            for(let i = 0; i < this.activeStocks.length; i++){
                if(this.activeStocks[i].stockSymbol == this.trade.stock_symbol){
                    this.activeStockMatchIndex = i;
                    break
                }
                else{
                    this.activeStockMatchIndex = -1
                }
                
            }
        },

        executeTrade(){
            if(this.trade.trade_type == 0){
                TradeSQLAPIService.executeTradeBuy(
                    this.trade.portfolio_id,
                    this.trade.stock_symbol,
                    this.trade.stock_name,
                    this.trade.stock_price,
                    this.trade.trade_type,
                    this.trade.quantity,
                    this.trade.total_trade_amount
                ).then(response =>{
                    console.log(response.status)
                    console.log(response.data)

                    if(response.status === 200){
                        alert(`Purchase of ${this.trade.quantity} shares of ${this.trade.stock_symbol} Was Successful`)
                        this.$store.commit("SET_ACTIVE_SCREEN", 2);
                    }
                }).catch((error) =>{
                    alert("Error Purchasing Stock " + error)
                });
            }
            else if(this.trade.trade_type == 1)
            {
                TradeSQLAPIService.executeTradeSell(
                    this.trade.portfolio_id,
                    this.trade.stock_symbol,
                    this.trade.stock_name,
                    this.trade.stock_price,
                    this.trade.trade_type,
                    this.trade.quantity,
                    this.trade.total_trade_amount
                ).then(response =>{
                    console.log(response.status)
                    console.log(response.data)

                    if(response.status === 200){
                        alert(`Sale of ${this.trade.quantity} shares of ${this.trade.stock_symbol} Was Successful`)
                        this.$store.commit("SET_ACTIVE_SCREEN", 2);
                    }
                    
                }).catch((error) =>{
                    alert("Error Purchasing Stock " + error)
                });
            }
            this.balance = this.getBalance();
            this.showBuySell = false;
                
            this.trade = {
                portfolio_id: '',
                stock_symbol: '',
                stock_name: '',
                stock_price: '',
                trade_type: '',
                quantity: '',
            }
        },

        readyToExecuteTrade(){
            if(this.activeStockMatchIndex == -1 && this.trade.trade_type == "0"){
                this.isSubmitting = false
            }
            else if(this.activeStockMatchIndex == -1 && this.trade.trade_type == "1"){
                this.isSubmitting = true
            }
            else if(
                this.trade.portfolio_id && 
                this.trade.stock_symbol && 
                this.trade.stock_name && 
                this.trade.stock_price && 
                this.trade.trade_type && 
                this.trade.quantity > 0 && 
                ((this.trade.total_trade_amount < this.balance) || this.trade.trade_type == "1") &&
                (this.activeStocks[this.activeStockMatchIndex].quantity >= this.trade.quantity || this.trade.trade_type == "0")
            ){    
                this.isSubmitting = false;
            }    
            else {
                this.isSubmitting = true;
            }  
        },

        clearBuySellForm(){
            
            this.trade.trade_type = '';
            this.trade.quantity = '';
            this.activeStockMatchIndex = -1;
            this.isSubmitting = true;
        }

    },
    computed:{
        
    }
}

</script>

<style>
*,
::before,
::after {
    box-sizing: border-box;
}

.balance {
    padding: 1em;
    /*
    border-style: solid;
    border-width: 1px;
    border-color: black;*/
}

.trade-form {
    padding: 1em;
    /*
    border-style: solid;
    border-width: 1px;
    border-color: red;*/
}



</style>