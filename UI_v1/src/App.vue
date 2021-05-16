<template>
  <div id="app">
    <NavBar/>
    <div class="main">
      <Header />
      <Content/>
    </div>
  </div>
</template>

<script>
import NavBar from './components/layouts/NavBar.vue'
import Header from './components/layouts/Header.vue'
import Content from './components/layouts/Content.vue'

export default {
  name: 'App',
  components: {
    NavBar,
    Header,
    Content
  }, 
  
}
</script>

<style>
@import "./components/styles/main.css";
@import "./components/styles/input.css";
@import "./components/styles/table.css";
@import "./components/styles/dropdown.css";
@import "./components/styles/button.css";
@import "./components/styles/icon.css";
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
  display: flex;
  flex-direction: column;
}
::-webkit-scrollbar {
  width: 10px;
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
