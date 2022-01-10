import axios from 'axios';
const http = axios.create({
  baseURL: "https://localhost:44315",
  
});

export default {
    
  /*
    executeTradeBuy(trade){
     return http.post('/executetradebuy', trade, {
        headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
    });
    }
  */
  
    executeTradeBuy(portfolio_id, stock_symbol, stock_name, stock_price, trade_type, quantity, total_trade_amount){
      return http.post(`/executetradebuy/${portfolio_id}/${stock_symbol}/${stock_name}/${stock_price}/${trade_type}/${quantity}/${total_trade_amount}`)
    },
    
    executeTradeSell(portfolio_id, stock_symbol, stock_name, stock_price, trade_type, quantity, total_trade_amount){
      return http.post(`/executetradesell/${portfolio_id}/${stock_symbol}/${stock_name}/${stock_price}/${trade_type}/${quantity}/${total_trade_amount}`);
    }

}