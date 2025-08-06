<template>
  <div>
    <Layout/>
  </div>   
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import Layout from '../src/components/Layouts/Layout.vue'
  export default defineComponent({
    name: 'Layout-Page',
    components: {
      Layout
    }
  })
</script>

