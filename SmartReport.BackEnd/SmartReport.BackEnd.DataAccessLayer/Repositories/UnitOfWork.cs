using SmartReport.BackEnd.DataAccessLayer.Contexts;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.DataAccessLayer.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private bool disposed = false;
        private readonly ApplicationDbContext _context;
        private ITaskRepository _taskRepository;
        private IReportRepository _reportRepository;
        private IPlaceRepository _placeRepository;
        private IRatingRepository _ratingRepository;
        private IAccountRepository _accountRepository;

        public IAccountRepository Account
        {
            get
            {
                if (_accountRepository == null)
                    _accountRepository = new AccountRepository(_context);
                return _accountRepository;
            }
        }

        public ITaskRepository Tasks
        {
            get
            {
                if (_taskRepository == null)
                    _taskRepository = new TaskRepository(_context);
                return _taskRepository;
            }
        }

        public IReportRepository Reports
        {
            get
            {
                if (_reportRepository == null)
                    _reportRepository = new ReportRepository(_context);
                return _reportRepository;
            }
        }

        public IPlaceRepository Places
        {
            get
            {
                if (_placeRepository == null)
                    _placeRepository = new PlaceRepository(_context);
                return _placeRepository;
            }
        }


        public IRatingRepository Rating
        {
            get
            {
                if (_ratingRepository == null)
                    _ratingRepository = new RatingRepository(_context);
                return _ratingRepository;
            }
        }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        Task IUnitOfWork.SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
