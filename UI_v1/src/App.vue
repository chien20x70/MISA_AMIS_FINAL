<template>
  <div id="app">
    <TheNavBar/>
    <div class="main">
      <TheHeader />
      <TheContent/>
    </div>
  </div>
</template>

<script>
import TheNavBar from './components/layouts/TheNavBar.vue'
import TheHeader from './components/layouts/TheHeader.vue'
import TheContent from './components/layouts/TheContent.vue'

export default {
  name: 'App',
  components: {
    TheNavBar,
    TheHeader,
    TheContent
  }, 
  
}
</script>

<style>

#app{
  color: #2c3e50;
  display: flex;
  width: 100%;
  height: 100vh;
  box-sizing: border-box;
}
.main{
  flex: 1;
  height: 100%;
  width: calc(100% - 172px);
  display: flex;
  flex-direction: column;
}
::-webkit-scrollbar {
  width: 10px;
  height: 10px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1; 
}
 
/* Handle */
::-webkit-scrollbar-thumb {
  background: #888; 
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555; 
}
</style>
