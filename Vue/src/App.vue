<template>
  <div id="nav">
    <router-link to="/" class="link">Home</router-link>
    <router-link to="/deck-builder" class="link">Deck Builder</router-link>
    <router-link to="/my-decks" class="link">My Decks</router-link>
    <router-link to="/fnm" class="link">FNM</router-link>
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
    background-color: rgba(21, 19, 60, .8);
    box-shadow: 0 5px 25px 0 rgba(0,0,0,.25);
  }
  .link{
    margin-left: 40px;
    text-decoration: none;
    color: white;
    height: 48px;
    border-top:solid 2px rgba(236, 153, 75, 0);
    display: flex;
    align-items: center;
  }
  .link:hover{
    color: #EC994B;
    border-top-color: rgba(236, 153, 75, 1);
  }
</style>