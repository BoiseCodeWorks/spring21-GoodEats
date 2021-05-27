<template>
  <form @submit.prevent="handleSubmit">
    <div class="form-group">
      <label for="name" class="sr-only">Name:</label>
      <input class="form-control" placeholder="Name" type="text" v-model="editable.name" required>
    </div>
    <div class="form-group">
      <label for="location" class="sr-only">Location:</label>
      <input class="form-control" placeholder="Location" type="text" v-model="editable.location" required>
    </div>
    <div>
      <button class="btn btn-primary" type="submit">
        Save
      </button>
    </div>
  </form>
</template>

<script>
import { reactive, ref, watchEffect } from 'vue'
import { restaurantsService } from '../services/RestaurantsService'
export default {
  props: {
    restaurant: {
      type: Object,
      default() {
        return {
          name: '',
          location: ''
        }
      }
    }
  },
  setup(props) {
    // NOTE use editable when needing to edit a prop
    const editable = ref(props.restaurant)
    watchEffect(() => {
      editable.value = props.restaurant
    })
    return reactive({
      editable,
      handleSubmit() {
        if (this.editable.id) {
          // update
        } else {
          // create
          restaurantsService.create(editable.value)
        }
      }
    })
  }
}
</script>

<style>

</style>
