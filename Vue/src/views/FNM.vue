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
            playersEvents: [],
            numOfEvents: 5,
        };
    },
    methods: {
        async getData(){
            return new Promise((resolve) => {
                let eventDates = [];
                let matchWinPercentages = [];
                let gameWinPercentages = [];

                if(this.events.length < this.numOfEvents) {
                    for(let i = 0 ; i < this.events.length; i++){
                        eventDates.push(this.events[i].eventDate.slice(0,10));
                        this.getMatchAndGameWinPercentFromAnEvent(this.events[i].eventId)
                        .then(response => {
                            matchWinPercentages.push(response.data.matchWinPercent);
                            gameWinPercentages.push(response.data.gameWinPercent);
                        })
                    }
                    resolve({eventDates, matchWinPercentages, gameWinPercentages});
                }
                else { 
                    for(let i = 0; i < this.numOfEvents; i++){
                        eventDates.push(this.events[i].eventDate.slice(0,10));
                        this.getMatchAndGameWinPercentFromAnEvent(this.events[i].eventId)
                        .then(response => {
                            matchWinPercentages.push(response.data.matchWinPercent);
                            gameWinPercentages.push(response.data.gameWinPercent);
                        })
                    }
                    resolve({eventDates, matchWinPercentages, gameWinPercentages});
                }
            })
        },
        chartEvents() {
            this.getData()
            .then(response => {
                const ctx = document.getElementById('events-chart');
                const myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: response.data.eventDates,
                        datasets: [{
                            label: 'Match Win %',
                            data: response.data.matchWinPercentages,
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
                });
                myChart;
            })
        },
        getMatchAndGameWinPercentFromAnEvent(eventId){
            return new Promise((resolve) => {
                let matchWinPercent;
                let gameWinPercent;
                MatchService.GetMatchesByEvent(eventId)
                .then(response => {
                    let matches = response.data;
                    let wonMatches = 0;
                    for(let i = 0; i < matches.length; i++){
                        if(matches[i].outcome == 'win'){
                            wonMatches++;
                        }
                    }
                    matchWinPercent = wonMatches / matches.length;
                    GameService.GetGamesByEvent(eventId)
                    .then(response2 => {
                        let games = response2.data;
                        let wonGames = 0;
                        for(let i = 0; i < games.length; i++){
                            if(games[i].outcome == 'win'){
                                wonGames++;
                            }
                        }
                        gameWinPercent = wonGames / games.length;
                        resolve({matchWinPercent, gameWinPercent});
                    })
                })
            });
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