using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ITaskRepository Tasks { get; }
        public IReportRepository Reports { get; }
        public IPlaceRepository Places { get; }
        public IRatingRepository Rating { get; }
        Task SaveAsync();
    }
}
