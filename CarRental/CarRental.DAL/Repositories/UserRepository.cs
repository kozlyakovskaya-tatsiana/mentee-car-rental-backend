using System;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private CarRentalDbContext _db;

        public UserRepository(CarRentalDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(User item)
        {
            if (item != null)
            {
                _db.Users.Add(item);
            }
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        public User Get(Guid id)
        {
            return _db.Users.Find(id);
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
