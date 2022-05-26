<template>
    <div id="FNM">
        <canvas id="events-chart" height="800" width="1400"></canvas>
        <event-card class="event-card" v-for="(event, index) in events" :event="event" :key="index" :iteration="index"></event-card>
    </div>
</template>

<script>
import FNMService from '../services/FNMService.js';
import MatchService from '@/services/MatchService.js';
// import GameService from '@/services/GameService.js';
import EventCard from '../components/EventCard.vue';
import Chart from 'chart.js/auto';
export default {
    components: {
        EventCard
    },
    data() {
        return {
            events: [],
            numOfEvents: 5,
        };
    },
    methods: {
        getData(){
            return new Promise((resolve) => {
                let eventDates = this.events.map( event => event.eventDate);
                let matchWinPercentages = this.events.map( event => {
                    let wonMatches = 0;
                    for(let i = 0; i < event.matches.length; i++){
                        if(event.matches[i].outcome == 'win'){
                            wonMatches++;
                        }
                    }
                    return wonMatches/ event.matches.length;
                });
                let returnObj = {
                    eventDates : eventDates,
                    matchWinPercentages : matchWinPercentages
                }
                resolve(returnObj);
            })
        },
        chartEvents() {
            this.getData()
            .then(response => {
                const ctx = document.getElementById('events-chart');
                const myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: response.eventDates,
                        datasets: [{
                            label: 'Match Win %',
                            data: response.matchWinPercentages,
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
        setEventProps(events){
            return new Promise((resolve) => {
                let promises = events.map( event => {
                    MatchService.GetMatchesByEvent(event.eventId)
                    .then(response => {
                        event.matches = response.data;
                        return event;
                    })
                })
                Promise.all(promises).then( results => resolve(results));
            })
            
        },
    },
    async created() {
        FNMService.GetEventsByPlayer(1)
        .then(response => {
            let events = response.data;
            let promises = events.map( event => {
                return MatchService.GetMatchesByEvent(event.eventId)
                .then(response => {
                    event.matches = response.data;
                    return event;
                })
            })
            Promise.all(promises).then( results => {
                this.events = results;
                this.chartEvents();
            });
        })
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