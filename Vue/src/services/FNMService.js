import axios from 'axios';

export default{
    GetEventById(eventId){
        return axios.get('/FNM/' + eventId)
    },
    GetEventsByPlayer(playerId){
        return axios.get('/FNM/Player' + playerId)
    },
    AddNewEvent(event){
        return axios.post('/FNM', event)
    },
}