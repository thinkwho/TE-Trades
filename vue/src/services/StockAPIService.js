import axios from 'axios';
const http = axios.create({
    baseURL: "https://financialmodelingprep.com",
    
});

const key = "?apikey=e1abf071b1023e06176404e2e7c089ef"
//const key = "?apikey=ea8ccc2fe0b59471e03ff6a6d4ffdd5c"
export default {
    getCompanyQuote(...symbols){
      let symbolQuery = '';
        for (let symbol of symbols){
            symbolQuery += symbol + ','
        }
    return http.get(`/api/v3/quote/${symbolQuery}` + key)
   },

   getStockQuote(symbol){
    return http.get(`/api/v3/quote/${symbol}` + key)
   }

}
