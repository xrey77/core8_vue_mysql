import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle'
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { faUnlock, faElevator, faCircleQuestion, faChartArea, faLocationArrow } from '@fortawesome/free-solid-svg-icons'; // Example icons

// Add selected icons to the library
library.add(faUnlock, faElevator, faCircleQuestion, faChartArea, faLocationArrow);
const app = createApp(App);
app.component('font-awesome-icon', FontAwesomeIcon);
app.use(router).mount('#app')
