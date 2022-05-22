<template>
    <div>

    </div>
</template>

<script>
import FNMService from '@/services/FNMService';
export default {
    data() {
        return {
            events: [],
        }
    },
    created() {
        FNMService.GetEventsByPlayer(1)
        .then(response => {
            this.events = response.data;
        })
  },
}
</script>

<style scoped>

</style>