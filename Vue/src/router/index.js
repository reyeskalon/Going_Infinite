import { createRouter, createWebHistory } from "vue-router"
import Home from '../views/Home.vue'
import DeckBuilder from '../views/DeckBuilder.vue'
import MyDecks from '../views/MyDecks.vue'
import FNM from '../views/FNM.vue'


const routes = [
    {
        path: "/",
        name: "Home",
        component: Home,
    },
    {
        path: "/deck-builder",
        name: "DeckBuilder",
        component: DeckBuilder,
    },
    {
        path: "/my-decks",
        name: "MyDecks",
        component: MyDecks,
    },
    {
        path: "/fnm",
        name: "FNM",
        component: FNM,
    }
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
});

export default router;