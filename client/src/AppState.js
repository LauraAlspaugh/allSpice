import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  recipes: [],
  activeRecipe: null,
  /** @type {import('./models/Favorite').Favorite.js} */
  myFavoriteRecipes: [],
  ingredients: [],
  activeIngredient: null,
  isFavorite: false,
  favorites: [],
  filteredRecipes: []
})
