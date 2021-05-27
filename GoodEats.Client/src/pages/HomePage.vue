<template>
  <div>
    <RestaurantForm :restaurant="state.activeRestaurant" />
    <div>
      <RestaurantCard v-for="r in state.restaurants" :key="r.id" :restaurant="r" @edit="editRestaurant" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { restaurantsService } from '../services/RestaurantsService'
export default {
  name: 'Home',
  setup() {
    const state = reactive({
      restaurants: computed(() => AppState.restaurants),
      activeRestaurant: {}
    })

    onMounted(() => {
      restaurantsService.getAll()
    })

    return {
      state,
      editRestaurant(r) {
        state.activeRestaurant = r
      }
    }
  }
}
</script>

<style scoped lang="scss">

</style>
