import axios from 'axios';

export default{
    GetCardById(cardId){
        return axios.get('/Card/' + cardId)
    }
}