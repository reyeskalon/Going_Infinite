<template>
    <div class="match" :class="match.outcome">
        <p class="match-header" :class="match.outcome">Match {{number + 1}} vs {{match.opponent}}</p>
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
    .match.win{
        border-color: rgb(34,130,34,1);
    }
    .match.loss{
        border-color: rgb(202,33,43,1);
    }
    .match.draw{
        border-color: gray;
    }
    .match-header.win{
        background-color: rgb(34,130,34,.6);
    }
    .match-header.loss{
        background-color: rgb(202,33,43,.6);
    }
    .match-header.draw{
        background-color: gray;
    }
    .match{
        width: 250px;
        height: 80px;
        margin: 20px;
        border: solid 2px black;
        border-radius: 15px;
        box-shadow: 0 0 25px 0 rgba(0,0,0,.25);
    }
    .games{ 
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .match-header{
        height: 30px;
        display: flex;
        justify-content: center;
        align-items: center;
        border-radius: 12px 12px 0px 0px;
        color: white;
    }
    p{
        margin: 0px;
    }
</style>