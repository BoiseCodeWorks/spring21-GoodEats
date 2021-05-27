<template>
  <div>
    {{ profile }}
  </div>
</template>

<script>
import { computed, reactive, watchEffect } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'

export default {
  setup() {
    const route = useRoute()
    watchEffect(() => {
      profilesService.getProfile(route.params.id)
      // reviewsService.getReviewsByCreator(route.params.id);
    })

    return reactive({
      profile: computed(() => AppState.profile)
    })
  }
}
</script>

<style>

</style>
