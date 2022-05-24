import axios from 'axios';

export default {
    GetDeckById(deckId){
        return axios.get('/Deck/' + deckId)
    },
    GetDeckByEventAndPlayer(eventId, playerId){
        return axios.get('/Deck/Event/' + eventId + '/' + playerId);
    },
    GetDecksByPlayer(playerId){
        return axios.get('/Deck/Player/' + playerId)
    },
    SubmitDeck(deck){
        return axios.post('/Deck', deck)
    },
}