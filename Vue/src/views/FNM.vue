<template>
    <div id="FNM">
        <canvas id="events-chart" height="800" width="1400"></canvas>
        <event-card class="event-card" v-for="(event, index) in events" :event="event" :key="index" :iteration="index"></event-card>
    </div>
</template>

<script>
import FNMService from '../services/FNMService.js';
import MatchService from '@/services/MatchService.js';
import GameService from '@/services/GameService.js';
import EventCard from '../components/EventCard.vue';
import Chart from 'chart.js/auto';
export default {
    components: {
        EventCard
    },
    data() {
        return {
            events: [],
            matches: [],
            games: [],
            numOfEvents: 5,
            eventDates: [],
            matchWinPercentages: [],
            gameWinPercentages: [],
        };
    },
    methods: {
        async getData(){
            let eventDates = [];

            if(this.events.length < this.numOfEvents) {
                for(let i = 0 ; i < this.events.length; i++){
                    eventDates.push(this.events[i].eventDate.slice(0,10));
                    await this.eventMatchWinPercent(this.events[i].eventId);
                    await this.eventGameWinPercent(this.events[i].eventId);
                }
                this.eventDates = eventDates;
                return;
            }
            else { 
                for(let i = 0; i < this.numOfEvents; i++){
                    eventDates.push(this.events[i].eventDate.slice(0,10));
                    await this.eventMatchWinPercent(this.events[i].eventId);
                    await this.eventGameWinPercent(this.events[i].eventId);
                }
                this.eventDates = eventDates;
                return;
            }
        },
        async chartEvents() {
            await this.getData();
            const ctx = document.getElementById('events-chart');
            const myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: this.eventDates,
                    datasets: [{
                        label: 'Match Win %',
                        data: this.matchWinPercentages,
                        backgroundColor: [
                            'rgba(236, 153, 75, .2)'
                        ],
                        borderColor: [
                            'rgba(236, 153, 75, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    },
                    responsive: false,
                    maintainAspectRatio: false,
                }
            })
            myChart;
        },
        async eventMatchWinPercent(eventId){
            let matches = [];
            let winCount = 0;
            MatchService.GetMatchesByEvent(eventId)
            .then(response => {
                matches = response.data;
                for(let i = 0; i < matches.length; i++){
                    if(matches[i].outcome == 'win'){
                        winCount ++;
                    }
                }
                this.matchWinPercentages.push(winCount/matches.length);
                return;
            })
        },
        async eventGameWinPercent(eventId){
            let games = [];
            let winCount = 0;
            GameService.GetGamesByEvent(eventId)
            .then(response => {
                games = response.data;
                for(let i = 0; i < games.length; i++){
                    if(games[i].outcome == 'win'){
                        winCount ++;
                    }
                }
                this.gameWinPercentages.push(winCount/games.length);
                return;
            })           
        }
    },
    created() {
        FNMService.GetEventsByPlayer(1)
            .then(response => {
            this.events = response.data;
            this.chartEvents();
        });
    },
    mounted() {
        
    }
}
</script>

<style scoped>
    #FNM {
        display: flex;
        flex-direction: column;
        align-items: center;
        background-color: #F1EEE9
    }
    #events-chart {
        margin: 20px;
    }
</style>