<template>
  <div id="nav">
    <router-link to="/" class="link">Home</router-link>
    <router-link to="/deck-builder" class="link">Deck Builder</router-link>
    <router-link to="/my-decks" class="link">My Decks</router-link>
  </div>
  
  <router-view />
</template>

<script>
export default {

}
</script>

<style scoped>
  #nav{
    display: flex;
    align-items: center;
    height: 50px;
    background-color: #73777B;
    box-shadow: 0 5px 25px 0 rgba(0,0,0,.25);
  }
  .link{
    margin-left: 10px;
    margin-right: 10px;
    text-decoration: none;
    color: white;
  }
  .link:hover{
    color: #EC994B;
  }
</style>