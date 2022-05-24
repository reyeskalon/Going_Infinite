import axios from 'axios';

export default{
    GetGameById(gameId){
        return axios.get('/Game/' + gameId);
    },
    GetGamesInMatch(matchId){
        return axios.get('/Game/Match/' + matchId);
    },
    GetGamesByPlayer(playerId){
        return axios.get('/Game/Player/' + playerId);
    },
    GetGamesByEvent(eventId){
        return axios.get('/Game/FNM/' + eventId);
    },
    AddNewGame(game){
        return axios.post('/Game', game);
    },
}