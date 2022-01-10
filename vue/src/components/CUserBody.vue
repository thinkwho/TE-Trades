<!--THINK ABOUT REMOVAL OF ENTIRE COMPONENT-->
<template>
  <div>
      <div v-if="$store.state.userMainComponent == 'view-games'">
        <c-view-games />
      </div>
        
      <div v-if="MenuOption == 2">
        
      </div>

      <div v-if="MenuOption == 3">
        
      </div>

      <div v-if="MenuOption == 4">
        
      </div>

  </div>
</template>

<script>

import CViewGames from '../components/CViewGames.vue';


export default {
  name: "c-user-body",
  components: {
    CViewGames
  },
  data() {
    return {
   
    }
  }

}
</script>

<style>

/*some tabe for currentGames games here
some grid for currentGameDetails contain game contents: time left, start date, etc */
</style>