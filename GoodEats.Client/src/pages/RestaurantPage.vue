<template>
  <div>
    <h1>
      {{ state.restaurant.name }}
    </h1>
    <form @submit.prevent="addReview">
      <input type="text" v-model="state.review.title">
      <input type="text" v-model="state.review.body">
      <input type="range" min="1" max="5" step="1" v-model="state.review.rating">
      <button type="submit" class="btn btn-primary">
        <i class="mdi mdi-food" style="font-size:24px"></i>
      </button>
    </form>
    <div>
      <div class="card shadow my-4" v-for="review in state.reviews" :key="review.id">
        <div class="card-body">
          <i class="mdi mdi-star" v-for="n in review.rating" :key="n"></i>
          <div class="card-title">
            {{ review.title }}
          </div>
          <p>{{ review.body }}</p>
        </div>
        <div class="card-footer text-right">
          <router-link :to="{name: 'Profile', params: {id: review.creatorId }}">
            <img :src="review.creator.picture" height="45" alt="">
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive, watchEffect } from 'vue'
import { useRoute } from 'vue-router'
import { restaurantsService } from '../services/RestaurantsService'
import { AppState } from '../AppState'
export default {
  setup() {
    const route = useRoute()
    const state = reactive({
      restaurant: computed(() => AppState.activeRestaurant),
      reviews: computed(() => AppState.reviews),
      review: { rating: 5 }
    })
    watchEffect(() => {
      restaurantsService.getById(route.params.id)
    })
    return {
      state,
      addReview() {
        // REVIEW be sure to attach the id
        state.review.restaurantId = route.params.id
        restaurantsService.addReview(state.review)
        state.review = { rating: 5 }
      }
    }
  }
}
</script>

<style>

</style>
