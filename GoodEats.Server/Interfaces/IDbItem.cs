using System;

namespace GoodEats.Interfaces
{
    public interface IDbItem
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}