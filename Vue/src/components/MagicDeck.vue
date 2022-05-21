<template>
    <div id="deck">
        <img id="deckArt" :src="deckArt">
        <div id="colors">
            <div id="white" class="color" v-if="whitePercent > 0.05"></div>
            <div id="blue" class="color" v-if="bluePercent > 0.05"></div>
            <div id="black" class="color" v-if="blackPercent > 0.05"></div>
            <div id="red" class="color" v-if="redPercent > 0.05"></div>
            <div id="green" class="color" v-if="greenPercent > 0.05"></div>
        </div>
        <p>Deck #{{deck.deckId}}</p>
    </div>
</template>

<script>
import CardService from '../services/CardService'
export default{
    props: ['deck'],
    data()  {
        return {
            currentDeck : {},
            deckArt: "",
            whitePercent: 0,
            bluePercent: 0,
            blackPercent: 0,
            redPercent: 0,
            greenPercent: 0,
        }
    },
    methods: {
        colorPercentage(color){
            const cards = this.currentDeck.cards;
            const totalColors = this.totalColorSymbols;
            let colorCount = 0.0;
            for(let i = 0; i < cards.length; i++){
                for(let j = 0; j < cards[i].manaCost.length; j++){
                    if(cards[i].manaCost[j] == color){
                        colorCount += 1;
                    }
                }
            }
            let percentage = colorCount / totalColors;
            return percentage;
        },
    },
    async created(){
        this.currentDeck = this.deck;
        let fullCards = [];
        let cards = this.deck.cards;
        for(const [key, value] of Object.entries(cards)){
            const data = await (await CardService.GetCardById(key)).data;
            data.count = value;
            fullCards.push(data);
        }
        this.currentDeck.cards = fullCards;
        let randomIndex = Math.floor(Math.random() * this.currentDeck.cards.length);
        this.deckArt = this.currentDeck.cards[randomIndex].artCrop;

        this.whitePercent = await this.colorPercentage('W');
        this.bluePercent = await this.colorPercentage('U');
        this.blackPercent = await this.colorPercentage('B');
        this.redPercent = await this.colorPercentage('R');
        this.greenPercent = await this.colorPercentage('G');

        let element = await document.getElementById("white");
        element.style.width = `${this.whitePercent * 260}px`;
        element = await document.getElementById("blue");
        element.style.width = `${this.whitePercent * 260}px`;
        element = await document.getElementById("black");
        element.style.width = `${this.whitePercent * 260}px`;
        element = await document.getElementById("red");
        element.style.width = `${this.whitePercent * 260}px`;
        element = await document.getElementById("greed");
        element.style.width = `${this.whitePercent * 260}px`;

    },
    computed: {
        totalColorSymbols(){
            const cards = this.currentDeck.cards;
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
        }
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
    #white{
        background-color: #FFFCD3;
    }
    #blue{
        background-color: #A9DCEF;
    }
    #black{
        background-color: #C6BFB9;
    }
    #red{
        background-color: #ECA285;
    }
    #green{
        background-color: #90CDA3;
    }
    .color{
        height: 20px;
        margin: 2px;
    }
</style>