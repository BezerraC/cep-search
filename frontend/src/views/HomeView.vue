<template>
  <CepSearch />

  <v-footer class="py-0 px-0 bottom-0 bg-teal d-flex flex-column">
      <div class="d-flex w-100 align-center px-4">
        {{ new Date().getFullYear() }} - 
        <a
          href="https://github.com/BezerraC"
          target="_blank"
          class="text-decoration-none text-white ms-2"
        >
          BezerraC
        </a>

        <v-spacer></v-spacer>

        <v-btn
          v-for="(icon, index) in icons"
          :key="icon"
          :icon="icon"
          :href="href[index]"
          target="_blank"
          class="mx-4"
          size="small"
          variant="plain"
        ></v-btn>
      </div>
    </v-footer>
</template>

<script>
import CepSearch from '@/components/CepSearch.vue'

export default {
  components: {
    CepSearch,
  },

  data: () => ({
    icons: ['mdi-github', 'mdi-linkedin', 'mdi-instagram'],
    href: ['https://github.com/BezerraC', 'https://www.linkedin.com/in/devcbezerra/', 'https://www.instagram.com/c.bezerra_/']
  }),
}
</script>

<style scoped>
.v-footer {
  position: fixed;
  bottom: 0;
  width: 100%;
}
</style>
