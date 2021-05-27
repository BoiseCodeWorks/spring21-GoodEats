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

  async getById(id) {
    AppState.reviews = []
    const res = await api.get(`/api/restaurants/${id}`)
    AppState.activeRestaurant = res.data
    await this.getReviews(id)
  }

  async getReviews(id) {
    const res = await api.get(`/api/restaurants/${id}/reviews`)
    AppState.reviews = res.data
  }

  async update(r) {
    await api.put(`/api/restaurants/${r.id}`, r)
  }

  async remove(r) {
    await api.delete(`/api/restaurants/${r.id}`)
    AppState.restaurants = AppState.restaurants.filter(x => x.id !== r.id)
  }

  async addReview(r) {
    const res = await api.post('/api/reviews', r)
    AppState.reviews.push(res.data)
  }
}

export const restaurantsService = new RestaurantsService()
