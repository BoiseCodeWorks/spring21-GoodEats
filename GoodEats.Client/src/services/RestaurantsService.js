import { api } from './AxiosService'
class RestaurantsService {
  async create(r) {
    await api.post('/api/restaurants', r)
  }
}

export const restaurantsService = new RestaurantsService()
