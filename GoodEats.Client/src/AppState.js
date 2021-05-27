import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  // REVIEW vvv THIS HAS NOTHING TO DO WITH AUTH
  profile: {},
  restaurants: [],
  reviews: [],
  activeRestaurant: {}
})
