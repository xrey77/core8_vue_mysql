<template>
    <div>
        <Header/>
            <slot/>            
        <Footer/>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import Header from '../Layouts/Header/Header.vue'
    import Footer from '../Layouts/Footer/Footer.vue'

    export default defineComponent({
        name: 'Layout-Page',
        components: {
            Header,
            Footer
        }
    })

</script>