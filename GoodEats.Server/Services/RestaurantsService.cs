using System;
using System.Collections.Generic;
using GoodEats.Models;
using GoodEats.Repositories;

namespace GoodEats.Services
{
    public class RestaurantsService
    {
        private readonly RestaurantsRepository _rp;
        private readonly ReviewsRepository _reviewsRepo;

        public RestaurantsService(RestaurantsRepository rp, ReviewsRepository reviewsRepo)
        {
            _rp = rp;
            _reviewsRepo = reviewsRepo;
        }

        internal Restaurant Create(Restaurant r)
        {
            return _rp.Create(r);
        }

        internal List<Restaurant> GetAll()
        {
            return _rp.GetAll();
        }

        internal Restaurant Update(Restaurant r, string id)
        {
            // Business Logic
            Restaurant restaurant = _rp.GetById(r.Id);

            // let x = findOneAndUpdate({userId: userId, id: id}, update)

            if (restaurant == null)
            {
                throw new Exception("Invalid Id");
            }
            if (restaurant.OwnerId != id)
            {
                throw new Exception("You are not allowed to edit because you do not own this r");
            }

            return _rp.Update(r);
        }

        internal Restaurant Get(int id)
        {
            var r = _rp.GetById(id);
            if (r == null)
            {
                throw new Exception("Invalid Id");
            }
            return r;
        }

        internal void Remove(int id, string userId)
        {
            // Business Logic
            // REVIEW notice I can re-use my own coolness
            Restaurant restaurant = Get(id);

            // let x = findOneAndUpdate({userId: userId, id: id}, update)

            if (restaurant.OwnerId != userId)
            {
                throw new Exception("You are not allowed to delete because you do not own this r");
            }
            _rp.Remove(id);
        }

        internal List<Review> GetReviews(int restaurantId)
        {
            return _reviewsRepo.GetReviews(restaurantId);
        }
    }
}