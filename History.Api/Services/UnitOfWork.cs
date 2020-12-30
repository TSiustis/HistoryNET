using History.Api.Data;
using History.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace History.Api.Services
{
    public class UnitOfWork : IDisposable
    {
        public HistoryDbContext _context;
        public GenericRepository<Event> eventRepository;
        public GenericRepository<Event> birthRepository;
        public GenericRepository<Death> deathRepository;
        public UnitOfWork(HistoryDbContext context)
        {
            _context = context;
        }
        public GenericRepository<Event> EventRepository
        {
            get
            {

                if (this.eventRepository == null)
                {
                    this.eventRepository = new GenericRepository<Event>(_context);
                }
                return eventRepository;
            }
        }

        public GenericRepository<Event> BirthRepository
        {
            get
            {

                if (this.birthRepository == null)
                {
                    this.birthRepository = new GenericRepository<Event>(_context);
                }
                return birthRepository;
            }
        }

        public GenericRepository<Death> DeathRepository
        {
            get
            {

                if (this.deathRepository == null)
                {
                    this.deathRepository = new GenericRepository<Death>(_context);
                }
                return deathRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

       

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
