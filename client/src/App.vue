<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class=" text-light nav-color">
    <!-- <div class="d-flex justify-content-between footer-img ">
      <img
        src="https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8N3x8Y29mZmVlfGVufDB8fDB8fHww"
        alt="coffee image">
      <img
        src="https://images.unsplash.com/photo-1478145046317-39f10e56b5e9?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mzh8fGZvb2R8ZW58MHx8MHx8fDA%3D"
        alt="breakfast image">
      <img
        src="https://images.unsplash.com/photo-1511920170033-f8396924c348?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8Y29mZmVlfGVufDB8fDB8fHww"
        alt="coffee image">
      <img
        src="https://images.unsplash.com/photo-1501959915551-4e8d30928317?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDh8fGZvb2R8ZW58MHx8MHx8fDA%3D"
        alt="oatmeal image">
      <img
        src="https://images.unsplash.com/photo-1604152135912-04a022e23696?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OTR8fGZvb2R8ZW58MHx8MHx8fDA%3D"
        alt="soup image">
    </div> -->
    <p class="m-5 fs-5"><i class="mdi mdi-copyright"></i> 2023 by Laura Alspaugh.</p>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 50px;
}

.nav-color {
  background-color: #7F8C8D
}

.footer-img {
  width: 150px;
  height: 150px;
  position: center;
  object-fit: cover;

  border-radius: 5px;
  border: solid 2px white;
}
</style>
