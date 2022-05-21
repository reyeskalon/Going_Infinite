<template>
    <div id="main">
        <div id="deck-bar">
            <p id="card-count">Cards: {{cardCount}}</p>
            <div id="mana-filter">
                <img class="mana" id="W" src="../assets/white-mana.png" @click="toggleColorFilter('W', this)">
                <img class="mana" id="U" src="../assets/blue-mana.png" @click="toggleColorFilter('U', this)">
                <img class="mana" id="B" src="../assets/black-mana.png" @click="toggleColorFilter('B', this)">
                <img class="mana" id="R" src="../assets/red-mana.png" @click="toggleColorFilter('R', this)">
                <img class="mana" id="G" src="../assets/green-mana.png" @click="toggleColorFilter('G', this)">
            </div>
            <input id="search" v-model="queryString" placeholder="Search">
            <button id="save-button" @click="(submitDeck())">Save Deck</button>
            <button id="save-button" @click="(getDeckById(1))">Get Deck</button>
        </div>
        <div id="cards">
            <button @click="showPrevious" v-if="startIndex > 0"><img class="btn-img" src="../assets/angle-double-left.png"></button>
            <magic-card class="card" v-for="card in cardsToShow" :card="card" :key="card.id" @click="addToDeck(card)"></magic-card>
            <button @click="showNext"><img class="btn-img" src="../assets/angle-double-right.png"></button>
        </div>
        <div id="split"></div>
        <div id="deck">
            <magic-card class="card" v-for="card in sortByCmc()" :card="card" :key="card.id" @click="removeFromDeck(card)"></magic-card>
        </div>
    </div>
</template>

<script>
import MagicCard from '../components/MagicCard.vue'
import DeckService from '../services/DeckService.js'
export default{
    components: {
        MagicCard
    },
    data() {
        return {
            queryString : "",
            cards : require("../NewCappenaCards.json"),
            startIndex: 0,
            endIndex: 6,
            builtDeck: [],
            filterColors: [],
            deck: {
                deckId: 0,
                playerId: 1,
                cards: {},
            },
            retrievedDeck: {},
        }
    },
    methods: {
        showPrevious(){
            if(this.startIndex > 0){
                this.startIndex -= 6;
                this.endIndex -= 6;
            }
        },
        showNext(){
            this.startIndex += 6;
            this.endIndex += 6;
        },
        addToDeck(card){
            let deck = this.builtDeck;
            let cardInDeck = false;
            let cardIndex = 0;
            for(let i = 0; i < deck.length; i++){
                if(card.id == deck[i].id){
                    cardInDeck = true;
                    cardIndex = i;
                }
            }
            if(cardInDeck){
                deck[cardIndex].count += 1;
            }
            else{
                let cardToAdd = JSON.parse(JSON.stringify(card));
                cardToAdd.count = 1;
                deck.push(cardToAdd);
            }
        },
        removeFromDeck(card){
            let index = this.builtDeck.indexOf(card);
            let deck = this.builtDeck;
            if(deck[index].count > 1){
                deck[index].count -= 1;
            }
            else{
                deck.splice(index, 1);
            }
        },
        sortByCmc(){
            return this.builtDeck.slice().sort(function(a, b) {
                return a.cmc - b.cmc;
            });
        },
        toggleColorFilter(color){
            if(this.filterColors.includes(color)){
                this.filterColors.splice(this.filterColors.indexOf(color),1)
            }
            else(
                this.filterColors.push(color)
            )
            this.startIndex = 0;
            this.endIndex = 6;
            let element = document.getElementById(color)
            if(element.classList.contains('selected')){
                element.classList.remove('selected')
            }
            else{
                element.classList.add('selected')
            }
        },
        submitDeck(){
            this.builtDeck.forEach(card => {
                this.deck.cards[card.id] = card.count;
            })
            DeckService.SubmitDeck(this.deck)
            .then(this.deck = {
                deckId: 0,
                playerId: 1,
                cards: [],
            })
        },
        getDeckById(id){
            DeckService.GetDeckById(id)
            .then(response => {
                this.retrievedDeck = response.data;
            })
        }
    },
    computed: {
        filteredCards(){
            let cards = this.cards
            if(this.filterColors.length > 0){
                cards = cards.filter(card => {
                    let inColor = false
                    this.filterColors.forEach(color => {if (card.color_identity.includes(color)) {
                        inColor = true
                    }})
                        return inColor
                })
            }
            return cards.filter(card => {
                const search = this.queryString.toLowerCase()
                return card.name.toLowerCase().includes(search) || card.oracle_text.toLowerCase().includes(search) || card.type_line.toLowerCase().includes(search)
            })
        },
        cardsToShow(){
            if (this.filteredCards.length <= 5){
                return this.filteredCards;
            }
            else{
                let returnCards = [];
                for(let i = this.startIndex; i < this.endIndex; i++){
                    returnCards.push(this.filteredCards[i]);
                }
                return returnCards;
            }
        },
        cardCount(){
            let sum = 0;
            this.builtDeck.forEach(card => {
                sum += card.count;
            })
            return sum;
        }
    },
}
</script>

<style scoped>
    .card{
        margin: 5px;
    }
    #cards{
        display: flex;
        flex-wrap: wrap;
        justify-content: space-around;
        align-items: center;
        padding: 10px;
    }
    #split{
        height: 10px;
        margin-left: 10px;
        margin-right: 10px;
        background-color: #15133C;
        border-radius: 5px;
    }
    .btn-img{
        height: 20px;
        width: 20px;
    }
    button{
        display: flex;
        padding: 1px;
        background-color:#F1EEE9;
        border: none;
    }
    #deck{
        display: flex;
        justify-content: space-around;
        flex-wrap: wrap;
        padding: 10px;
        background-color: #F1EEE9;
    }
    #deck-bar{
        display: flex;
        align-items: center;
        justify-content: center;
        background-color: #F1EEE9;
        height: 40px;
        margin: 15px;
        position: relative;
    }
    #save-button{
        display: flex;
        align-items: center;
        margin-left: 20px;
        height: 35px;
        border-radius: 5px;
        padding-left:5px;
        padding-right:5px;
        background-color: #15133C;
        color: white;
    }
    #save-button:hover{
        background-color: #EC994B;
    }
    #search{
        height: 30px;
        margin-left: 20px;
        border-radius: 5px;
    }
    .mana{
        height: 25px;
        margin: 10px;
        border: solid 2px black;
        border-radius: 50%;
    }
    .mana:hover{
        border-color: #EC994B;
    }
    #mana-filter{
        display: flex;
    }
    .selected{
        border-color: #EC994B;
    }
    #card-count{
        position: absolute;
        left: 20px;
        font-weight: 700;
    }
</style>