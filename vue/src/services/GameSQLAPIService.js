import axios from 'axios';
const http = axios.create({
    baseURL: "https://localhost:44315"
    
});

/*
if (this.$store.state.currentToken != null){
    http.defaults.headers.common['Authorization'] = `Bearer ${this.$store.state.currentToken}`;
}
*/
export default {
    
    //returns list of every game in DB
    getListOfGames() {
        return http.get('/game', {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
    },
    //getAllUserGames(user){
      //  return http.get(`/game/${user.username}/allgames`,{
    // getGameDetails() {
    //     return http.get('/games', {
    //returns list of all the users games (completed, incomplete, organizer)
    getAllUserGames(username){
        
        return http.get(`/game/${username}/allgames`,{
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
    },
    GetGameDetails(gameId) {
        return http.get(`/game/${gameId}`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
    },
    addNewGame(game) {
        return http.post('/game', game , {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
    },
    getLeaderboard(gameId, completedDate){
        console.log(completedDate)
        return http.get(`/leaderboard/${gameId}/${completedDate}` , {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        }); 
    },
    
    getActiveGames(username){
        return http.get(`/game/${username}/activegames` , {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
    },
    updateIsComplete(gameId){
        return http.put(`/game/${gameId}/IsComplete`, gameId, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        })
    }
}
