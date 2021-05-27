import { AppState } from '../AppState'
import { api } from './AxiosService'
class RestaurantsService {
  async create(r) {
    const res = await api.post('/api/restaurants', r)
    AppState.restaurants.push(res.data)
  }

  async getAll() {
    const res = await api.get('/api/restaurants')
    AppState.restaurants = res.data
  }

  async update(r) {
    await api.put(`/api/restaurants/${r.id}`, r)
  }

  async remove(r) {
    await api.delete(`/api/restaurants/${r.id}`)
    AppState.restaurants = AppState.restaurants.filter(x => x.id !== r.id)
  }
}

export const restaurantsService = new RestaurantsService()
