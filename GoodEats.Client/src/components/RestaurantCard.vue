<template>
  <div class="card shadow m-1 p-4 position-relative">
    <div class="position-absolute"
         style="right: 0; top: 0"
         v-if="restaurant.ownerId == account.id"
    >
      <button class="btn-info btn" @click="edit">
        <i class="mdi mdi-pencil"></i> EDIT
      </button>
      <button class="btn-danger btn" @click="remove">
        <i class="mdi mdi-trash-can"></i> delete
      </button>
    </div>
    <div class="card-body">
      <router-link :to="{name: 'Restaurant', params: {id: restaurant.id}}">
        <h3>
          {{ restaurant.name }}
        </h3>
      </router-link>
      <em>
        <i class="mdi mdi-map-marker"></i>
        {{ restaurant.location }}
      </em>
    </div>
    <div class="mt-2 text-right">
      <router-link :to="{name: 'Profile', params: {id: restaurant.ownerId }}">
        <img :src="restaurant.owner.picture" height="40" class="rounded-circle mr-2" alt="">
        <small>
          {{ restaurant.owner.name }}
        </small>
      </router-link>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { restaurantsService } from '../services/RestaurantsService'
export default {
  props: {
    restaurant: {
      type: Object,
      required: true
    }
  },
  emits: ['edit'],
  setup(props, { emit }) {
    return reactive({
      account: computed(() => AppState.account),
      edit() {
        emit('edit', props.restaurant)
      },
      remove() {
        restaurantsService.remove(props.restaurant)
      }

    })
  }
}
</script>

<style>

</style>
