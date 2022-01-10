import axios from 'axios';
const http = axios.create({
    baseURL: "https://localhost:44315"
    
})

export default{
    createUser(user){
        http.post('/users', user)
    },
    getUsers(){
       return http.get('allusers', {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
    }
}