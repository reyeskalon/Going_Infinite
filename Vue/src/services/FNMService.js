import axios from 'axios';

export default{
    GetEventsByPlayer(playerId){
        return axios.get('/FNM/' + playerId)
    }
}