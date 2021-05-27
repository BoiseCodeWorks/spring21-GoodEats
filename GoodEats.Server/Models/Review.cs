using System;
using System.ComponentModel.DataAnnotations;
using GoodEats.Interfaces;

namespace GoodEats.Models
{
    public class Review : IDbItem
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public int Rating { get; set; }
        public string CreatorId { get; set; }
        public string RestaurantId { get; set; }
        public Profile Creator { get; set; }
    }
}