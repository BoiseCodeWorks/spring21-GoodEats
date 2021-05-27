using System;
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
    }
}