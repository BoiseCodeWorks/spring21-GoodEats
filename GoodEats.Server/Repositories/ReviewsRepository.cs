using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GoodEats.Models;

namespace GoodEats.Repositories
{
    public class ReviewsRepository
    {
        private readonly IDbConnection _db;

        public ReviewsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Review Create(Review r)
        {
            string sql = @"
                INSERT INTO 
                reviews(title, body, rating, creatorId, restaurantId)
                VALUES (@Title, @Body, @Rating, @CreatorId, @RestaurantId);
                SELECT LAST_INSERT_ID();
            ";
            r.Id = _db.ExecuteScalar<int>(sql, r);
            return r;
        }

        internal List<Review> GetReviews(int restaurantId)
        {
            string sql = @"
                SELECT 
                    r.*,
                    a.* 
                FROM reviews r
                JOIN accounts a ON a.id = r.creatorId
                WHERE r.restaurantId = @restaurantId;
            ";
            return _db.Query<Review, Profile, Review>(sql, (r, p) =>
            {
                r.Creator = p;
                return r;
            }, new { restaurantId }).ToList();
        }
    }
}