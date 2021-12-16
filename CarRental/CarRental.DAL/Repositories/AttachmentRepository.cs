using System;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private CarRentalDbContext _db;

        public AttachmentRepository(CarRentalDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(Attachment item)
        {
            if (item != null)
            {
                _db.Attachments.Add(item);
            }
        }

        public IQueryable<Attachment> GetAll()
        {
            return _db.Attachments;
        }

        public Attachment Get(Guid id)
        {
            return _db.Attachments.Find(id);
        }

        public void Update(Attachment item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            var attachment = _db.Attachments.Find(id);
            if (attachment != null)
            {
                _db.Attachments.Remove(attachment);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
