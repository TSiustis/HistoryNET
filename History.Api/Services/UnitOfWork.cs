using History.Api.Data;
using History.Api.Helper;
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
        public GenericRepository<Birth> birthRepository;
        public GenericRepository<Death> deathRepository;
        private readonly IDataShaper<Event> _eventDataShaper;
        private readonly IDataShaper<Birth> _birthDataShaper;
        private readonly IDataShaper<Death> _deathDataShaper;
        public UnitOfWork(HistoryDbContext context, IDataShaper<Event> eventDataShaper, IDataShaper<Birth> birthDataShaper
                            , IDataShaper<Death> deathDataShaper)
        {
            _context = context;
            _eventDataShaper = eventDataShaper;
            _birthDataShaper = birthDataShaper;
            _deathDataShaper = deathDataShaper;
        }
        public GenericRepository<Event> EventRepository
        {
            get
            {

                if (this.eventRepository == null)
                {
                    this.eventRepository = new GenericRepository<Event>(_context,_eventDataShaper);
                }
                return eventRepository;
            }
        }

        public GenericRepository<Birth> BirthRepository
        {
            get
            {

                if (this.birthRepository == null)
                {
                    this.birthRepository = new GenericRepository<Birth>(_context,_birthDataShaper);
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
                    this.deathRepository = new GenericRepository<Death>(_context,_deathDataShaper);
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
