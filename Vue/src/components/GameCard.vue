<template>
    <div class="game-card">
        <p class="game" :class="game.outcome"></p>
    </div>
</template>

<script>
export default{
    props: ['game', 'number'],
    components: {

    }
}
</script>

<style scoped>
    .win{
        background-color: rgb(34,130,34,1);
    }
    .loss{
        background-color: rgb(202,33,43,1);
    }
    .draw{
        background-color: gray;
    }
    .game{
        margin: 20px;
        padding-left: 15px;
        padding-top: 5px;
        padding-bottom: 5px;
        border-radius: 15px;
    }
    .game-card{ 
        
    }
</style>