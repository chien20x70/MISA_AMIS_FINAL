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
  width: calc(100% - 178px);
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


/**
  * CSS custom thư viện vue-date-pick.
  *
*/
.vdpClearInput{
  margin-top: 4px !important;
}
.vdpFloating.vdpPositionLeft {
  position: absolute;
  left: -120px !important;
}
.vdpHeader{
  padding: 0 1em 5em !important;
  margin: -1em -1em -3.5em !important;
}
.vdpHeadCell {
    padding: .3em .5em .3em !important;
}
.vdpPeriodControl {
    line-height: 10px;
}
.vdpCellContent {
    font-size: 15px !important;
}

.ui.search.selection.dropdown>input.search{
  height: 32px;
}

.ui.selection.dropdown{
  border-radius: 2px !important;
  line-height: 10px !important;
}
.ui.selection.dropdown>.dropdown.icon{
  top: 7px !important;
}
.ui.selection.active.dropdown, .ui.selection.active.dropdown .menu {
    border-color: none !important;
  
}
/**-------------------------end-lib */
</style>
