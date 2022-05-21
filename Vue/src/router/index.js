import { createRouter, createWebHistory } from "vue-router"
import Home from '../views/Home.vue'
import DeckBuilder from '../views/DeckBuilder.vue'
import MyDecks from '../views/MyDecks.vue'


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
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
});

export default router;