<template>
    <div>
        <event-card class="event-card" v-for="(event, index) in events" :event="event" :key="index" :iteration="index"></event-card>
    </div>
</template>

<script>
import FNMService from '../services/FNMService.js';
import EventCard from '../components/EventCard.vue';
export default {
    components: {
        EventCard
    },
    data() {
        return {
            events: [],
            matches: [],
            games: [],
        };
    },
    created() {
        FNMService.GetEventsByPlayer(1)
            .then(response => {
            this.events = response.data;
        });
    },
}
</script>

<style scoped>

</style>