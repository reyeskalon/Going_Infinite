<template>
    <div>
        <p class="match" :class="match.outcome">Match {{number + 1}} vs {{match.opponent}}</p>
        <div class="games" v-if="games.length > 0"> 
            <p>Games: </p>
            <game-card v-for="(game, index) in games" :game="game" :number="index" :key="game.gameId"></game-card>
        </div>
    </div>
</template>

<script>
import GameCard from './GameCard.vue'
import GameService from '../services/GameService'
export default{
    props: ['match', 'number'],
    components: {
        GameCard
    },
    data() {
        return {
            games: [],
        }
    },
    created(){
        GameService.GetGamesInMatch(this.match.matchId)
        .then(response => {
            this.games = response.data;
        })
    }
}
</script>

<style scoped>
    .win{
        background-color: rgb(34,130,34,.75);
    }
    .loss{
        background-color: rgb(202,33,43,.75);
    }
    .draw{
        background-color: gray;
    }
    .match{
        margin: 20px;
        padding: 15px;
        border: solid 2px black;
        border-radius: 15px;
    }
    .games{ 
        display: flex;
        justify-content: center;
        align-items: center;
    }
    p{
        margin: 0px;
    }
</style>