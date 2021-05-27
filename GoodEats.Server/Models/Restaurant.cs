using System;
using System.ComponentModel.DataAnnotations;
using GoodEats.Interfaces;

namespace GoodEats.Models
{
    public class Restaurant : IDbItem
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        public string Location { get; set; }
        public string OwnerId { get; set; }
        // NOTE Virtual use profile not account
        public Profile Owner { get; set; }
    }
}