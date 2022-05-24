<template>
    <div id="deck">
        <img id="deckArt" :src="deckArt">
        <div id="colors">
            <div :id="`white${iteration}`" class="color" v-if="whitePercent > 0.05"></div>
            <div :id="`blue${iteration}`" class="color" v-if="bluePercent > 0.05"></div>
            <div :id="`black${iteration}`" class="color" v-if="blackPercent > 0.05"></div>
            <div :id="`red${iteration}`" class="color" v-if="redPercent > 0.05"></div>
            <div :id="`green${iteration}`" class="color" v-if="greenPercent > 0.05"></div>
        </div>
    </div>
</template>

<script>
export default{
    props: {
        deck: {
            type: Object,
            default: function () {
                return {cards: []}
            }
        },
        iteration: {
            type: Number,
            default: 0
        }
    },
    data()  {
        return {
            whitePercent: 0,
            bluePercent: 0,
            blackPercent: 0,
            redPercent: 0,
            greenPercent: 0,
            whiteColor: "#FFFCD3",
            blueColor: "#A9DCEF",
            blackColor: "#C6BFB9",
            redColor: "#ECA285",
            greenColor: "#90CDA3",
            colorBreakdownWidth : 265,
            count : this.iteration,
        }
    },
    methods: {
        colorBreakdown(){
            const cards = this.deck.cards;
            const totalColors = this.totalColorSymbols;
            let whiteCount = 0;
            let blueCount = 0;
            let blackCount = 0;
            let redCount = 0;
            let greenCount = 0;
            for(let i = 0; i < cards.length; i++){
                for(let j = 0; j < cards[i].manaCost.length; j++){
                    let char = cards[i].manaCost[j];
                    if(char == '{' || char == '}'){
                        continue;
                    }
                    else if(char == 'W'){
                        whiteCount += 1;
                    }
                    else if(char == 'U'){
                        blueCount += 1;
                    }
                    else if(char == 'B'){
                        blackCount += 1;
                    }
                    else if(char == 'R'){
                        redCount += 1;
                    }
                    else if(char == 'G'){
                        greenCount += 1;
                    }
                }
            }
            this.whitePercent = Math.round((whiteCount / totalColors) * 100) / 100;
            this.bluePercent = Math.round((blueCount / totalColors) * 100) / 100;
            this.blackPercent = Math.round((blackCount / totalColors) * 100) / 100;
            this.redPercent = Math.round((redCount / totalColors) * 100) / 100;
            this.greenPercent = Math.round((greenCount / totalColors) * 100) / 100;
        },
        setColorBreakdown(){
            const width = this.colorBreakdownWidth;

            let whiteBar = document.getElementById(`white${this.iteration}`);
            let blueBar =  document.getElementById(`blue${this.iteration}`);
            let blackBar = document.getElementById(`black${this.iteration}`);
            let redBar = document.getElementById(`red${this.iteration}`);
            let greenBar = document.getElementById(`green${this.iteration}`);

            if(this.whitePercent > 0.05){
                whiteBar.style.width = `${this.whitePercent * width}px`;
                whiteBar.style.backgroundColor = this.whiteColor;
            }
            if(this.bluePercent > 0.05){
                blueBar.style.width = `${this.bluePercent * width}px`;
                blueBar.style.backgroundColor = this.blueColor;
            }
            if(this.blackPercent > 0.05){
                blackBar.style.width = `${this.blackPercent * width}px`;
                blackBar.style.backgroundColor = this.blackColor;
            }
            if(this.redPercent > 0.05){
                redBar.style.width = `${this.redPercent * width}px`;
                redBar.style.backgroundColor = this.redColor;
            }
            if(this.greenPercent > 0.05){
                greenBar.style.width = `${this.greenPercent * width}px`;
                greenBar.style.backgroundColor = this.greenColor;
            }  
        }
    },
    created(){
        this.colorBreakdown();
    },
    mounted(){
        this.setColorBreakdown();
    },
    computed: {
        totalColorSymbols(){
            const cards = this.deck.cards;
            const colors = ['W','U','B','R','G'];
            let count = 0.0;
            for(let i = 0; i < cards.length; i++){
                for(let j = 0; j < cards[i].manaCost.length; j++){
                    if(colors.includes(cards[i].manaCost[j])){
                        count += 1;    
                    }
                }
            }
            
            return count;
        },
        deckArt(){
            let randomIndex = Math.floor(Math.random() * this.deck.cards.length);
            return this.deck.cards[randomIndex].artCrop;
        }
    },
    watch: {

    }
}
</script>

<style scoped>
    #deck{
        display: flex;
        flex-direction: column;
        align-items: center;
        margin: 10px;
        
    }
    #deckArt{
        border-radius: 10px;
        width: 313px;
        height: 228px;
        border: solid 2px #15133C;
        box-shadow: 0 0 25px 0 rgba(0,0,0,.25);
    }
    #deckArt:hover{
        border-color: #EC994B;
    }
    #colors{
        display: flex;
        background-color: #15133C;
        transform: skew(-20deg);
        box-shadow: 0 5px 25px 0 rgba(0,0,0,.25);
        margin-top: 15px;
    }
    .color{
        height: 20px;
        margin: 2px;
    }
</style>