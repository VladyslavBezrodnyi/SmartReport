using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.DataAccessLayer.Interfaces
{
    public interface IAccountRepository
    {
        System.Threading.Tasks.Task<bool> SetVisitDate(string userId);
        System.Threading.Tasks.Task<IList<VisitDate>> GetVisitDate(string userId, DateTimeOffset clientTime);
    }
}
