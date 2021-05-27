using System;
using System.Data;
using Dapper;
using GoodEats.Models;

namespace GoodEats.Repositories
{
    public class RestaurantsRepository
    {
        private readonly IDbConnection _db;

        public RestaurantsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Restaurant Create(Restaurant r)
        {
            string sql = @"
                INSERT INTO 
                restaurants(name, location, ownerId)
                VALUES (@Name, @Location, @OwnerId);
                SELECT LAST_INSERT_ID();
            ";
            r.Id = _db.ExecuteScalar<int>(sql, r);
            return r;
        }
    }
}