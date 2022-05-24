<template>
    <div class="event-card">
        <div class="event-info-container"> 
            <h3 class="event-info">{{event.lgs}}</h3>
            <h3 class="event-info">{{month}} / {{day}} / {{year}}</h3>
        </div>
        <div class="match-info">
            <div class="deck" v-if="deck.hasOwnProperty('cards')">
                <magic-deck :deck="deck" :iteration="iteration"></magic-deck>
            </div>
            <div class="matches" v-if="matches.length > 0">
                <match-card class="match" v-for="(match, index) in matches" :match="match" :number="index" :key="match.matchId"></match-card>
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
    .event-card{
        margin: 40px;
        display: flex;
        flex-direction: column;
        border: solid 2px rgba(21, 19, 60, 1);
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
        background-color: rgba(21, 19, 60, .8);
        box-shadow: 0 5px 25px 0 rgba(0,0,0,.25);
        border-radius: 12px 12px 0px 0px;
        color: white;
    }
    .deck{
        align-self: start;
    }
    .matches{
        display: grid;
        grid-template-columns: 1fr 1fr;
        grid-template-rows: 1fr 1fr;
        column-gap: 50px;
        row-gap: 50px;
        width: 100%;
    }
    .match-info{
        display: flex;
        justify-content: space-between;
        padding: 20px;
    }
</style>