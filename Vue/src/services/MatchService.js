import axios from 'axios';

export default{
    GetMatchById(matchId){
        return axios.get('/Match/' + matchId);
    },
    GetMatchesByEvent(eventId){
        return axios.get('/Match/Event/' + eventId);
    },
    GetMatchesByPlayer(playerId){
        return axios.get('/Match/Player/' + playerId);
    },
    AddNewMatch(match){
        return axios.post('/Match', match);
    },
}