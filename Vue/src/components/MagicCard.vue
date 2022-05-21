<template>
    <div id="card-container">
        <p id="counter" v-if="card.count > 1">{{card.count}}</p>
        <img id="card" :src="card.image">
    </div>
</template>

<script>
export default {
    props: ['card']
}
</script>

<style scoped>
    #card{
        width: 265px;
        height: 370px;
        border-radius: 17px;
    }
    #counter{
        font-size: 25px;
        color: white;
        position: absolute;
        left: 80%;
        top: 8%;
        text-shadow: 2px 2px black;
    }
    #card-container{
        position: relative;
        height: 370px;
        border-radius: 17px;
        box-shadow: 0 5px 25px 0 rgba(0,0,0,.25);
    }
</style>