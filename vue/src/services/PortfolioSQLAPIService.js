import axios from 'axios';
const http = axios.create({
  baseURL: "https://localhost:44315",
});

export default {

  GetGameBalance(username, gameId) {
    return http.get(`portfolio/${username}/game/${gameId}/balance`,{
      headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
  });
  },

  GetPortfolioId(gameId, username){
    return http.get(`portfolio/${username}/${gameId}`, {
      headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    });
  },

  GetPortfolioById(portfolioId){                     //Get portfolio by portfolioId...
    return http.get(`portfolio/${portfolioId}`, {
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    });
  }
  
}