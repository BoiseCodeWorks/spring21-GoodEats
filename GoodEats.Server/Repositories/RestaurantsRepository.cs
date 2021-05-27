using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        internal Restaurant GetById(int id)
        {
            string sql = @"
            SELECT 
                r.*, 
                a.* 
            FROM restaurants r 
            JOIN accounts a ON a.id = r.ownerId
            WHERE r.id = @id";
            return _db.Query<Restaurant, Account, Restaurant>(sql, (r, a) =>
            {
                r.Owner = a;
                return r;
            }, new { id }).FirstOrDefault();
        }

        internal List<Restaurant> GetAll()
        {
            string sql = @"
            SELECT 
            r.*,
            a.* 
            FROM restaurants r
            JOIN accounts a ON r.ownerId = a.id;";
            // REVIEW item1 = r item2 = a, what returns
            return _db.Query<Restaurant, Profile, Restaurant>(sql,
            // similar to array.map
            (r, a) =>
            {
                r.Owner = a;
                return r;
            }, splitOn: "id").ToList();
        }

        internal Restaurant Update(Restaurant r)
        {
            string sql = @"
            UPDATE restaurants 
            SET 
                name = @Name,
                location = @Location
            WHERE id = @Id;
            ";
            _db.Execute(sql, r);
            return r;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM restaurants WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}