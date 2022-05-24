<template>
    <div id="event-card">
        <div class="event-info-container"> 
            <h2 class="event-info">{{event.lgs}}</h2>
            <h2 class="event-info">{{month}}/{{day}}/{{year}}</h2>
        </div>
        <div class="match-info">
            <div class="deck" v-if="deck.hasOwnProperty('cards')">
                <magic-deck :deck="deck" :iteration="iteration"></magic-deck>
            </div>
            <div class="matches" v-if="matches.length > 0">
                <match-card v-for="(match, index) in matches" :match="match" :number="index" :key="match.matchId"></match-card>
            </div>
        </div>
        
    </div>
</template>

<script>
import MagicDeck from './MagicDeck.vue'
import MatchCard from './MatchCard.vue';
import DeckService from '../services/DeckService.js';
import MatchService from '../services/MatchService';
export default{
    props: ['event', 'iteration'],
    components: {
    MagicDeck,
    MatchCard
    },
    data() {
        return {
            matches: [],
            deck: {},
        }
    },
    computed: {
        day(){
            return this.event.eventDate.slice(8, 10).replace('0', '');
        },
        month(){
            return this.event.eventDate.slice(5,7).replace('0', '');
        },
        year(){
            return this.event.eventDate.slice(0,4)
        }
    },
    created() {
        DeckService.GetDeckByEventAndPlayer(this.event.eventId, 1)
        .then(response => {
            this.deck = response.data;
        })
        MatchService.GetMatchesByEvent(this.event.eventId)
        .then(response => {
            this.matches = response.data;
        })
    },
}
</script>

<style scoped>
    #event-card{
        margin: 10px;
        padding: 5px;
        display: flex;
        flex-direction: column;
        border: solid 2px black;
        border-radius: 15px;
        box-shadow: 0 5px 25px 0 rgba(0,0,0,.25);
    }
    .event-info{
        margin: 10px;
        padding: 10px;
    }
    .event-info-container{
        display: flex;
        justify-content: space-between;
    }
    .deck{
        align-self: start;
    }
    .matches{
        display: flex;
        justify-content: space-around;
        width: 100%;
    }
    .match-info{
        display: flex;
        justify-content: space-between;
    }
</style>