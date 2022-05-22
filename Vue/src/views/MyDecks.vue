<template>
    <div id="decks">
        <magic-deck v-for="(deck, index) in decks" :key="deck.id" :deck="deck" :iteration="index"></magic-deck>
    </div>
</template>

<script>
import MagicDeck from '../components/MagicDeck.vue'
import DeckService from '../services/DeckService.js'
export default{
  components: { MagicDeck },
  data(){
      return{
          decks : {},
      }
  },
  created() {
        DeckService.GetDecksByPlayer(1)
        .then(response => {
            this.decks = response.data;
        })
  }
}
</script>

<style scoped>
    #decks{
        display: flex;
        justify-content: space-around;
        margin: 20px;
    }
</style>