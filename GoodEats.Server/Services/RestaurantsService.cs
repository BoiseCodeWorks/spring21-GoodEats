using System;
using System.Collections.Generic;
using GoodEats.Models;
using GoodEats.Repositories;

namespace GoodEats.Services
{
    public class RestaurantsService
    {
        private readonly RestaurantsRepository _rp;

        public RestaurantsService(RestaurantsRepository rp)
        {
            _rp = rp;
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
        internal void Remove(int id, string userId)
        {
            // Business Logic
            Restaurant restaurant = _rp.GetById(id);

            // let x = findOneAndUpdate({userId: userId, id: id}, update)

            if (restaurant == null)
            {
                throw new Exception("Invalid Id");
            }
            if (restaurant.OwnerId != userId)
            {
                throw new Exception("You are not allowed to delete because you do not own this r");
            }
            _rp.Remove(id);
        }
    }
}