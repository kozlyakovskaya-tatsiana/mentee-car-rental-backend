using System;

namespace CarRental.DAL.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}