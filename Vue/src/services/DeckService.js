import axios from 'axios';

export default {
    GetDeckById(deckId){
        return axios.get('/Deck/' + deckId)
    },
    SubmitDeck(deck){
        return axios.post('/Deck/', deck)
    },
    GetDecksByPlayer(playerId){
        return axios.get('/Decks/' + playerId)
    },
}