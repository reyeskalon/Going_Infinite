import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import axios from 'axios'

axios.defaults.baseURL = "https://localhost:7258"
const app = createApp(App).use(router);
app.mount('#app');
