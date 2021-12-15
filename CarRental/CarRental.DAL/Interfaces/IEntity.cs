using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Interfaces
{
    interface IEntity
    {
        public Guid Id { get; set; }
    }
}
