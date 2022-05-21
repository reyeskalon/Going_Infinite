<template>
  <div>
    <div id="top">
      <h1>Going Infinite</h1>
      <input id="search" v-model="queryString" placeholder="Search">
    </div>
    <div id="cards">
      <magic-card class="card" v-for="card in filteredCards" :key="card.id" :card="card"></magic-card>
    </div>
  </div>
</template>

<script>
import MagicCard from '../components/MagicCard.vue'
export default {
  name: 'HomePage',
  components: {
    MagicCard
  },
  data() {
    return {
      queryString : "",
      cards : require("../NewCappenaCards.json"),
    }
  },
  computed: {
    filteredCards(){
      return this.cards.filter(card => {
         const search = this.queryString.toLowerCase()
         return card.name.toLowerCase().includes(search) || card.oracle_text.toLowerCase().includes(search) || card.type_line.toLowerCase().includes(search)
      })
    }
  },
  methods: {

  }
}
</script>

<style scoped>
  #cards{
    display: flex;
    flex-wrap: wrap;
    justify-content: space-around;
    background-color: #F1EEE9;
  }
  .card{
    margin: 5px;
    margin-bottom: 20px;
  }
  #top{
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  #search
  {
    margin-bottom: 40px;
  }
</style>
