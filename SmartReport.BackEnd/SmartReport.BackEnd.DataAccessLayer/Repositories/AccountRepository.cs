using Microsoft.EntityFrameworkCore;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using SmartReport.BackEnd.DataAccessLayer.Contexts;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.DataAccessLayer.Repositories
{
    class AccountRepository: IAccountRepository
    {
        private ApplicationDbContext context;

        public AccountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async System.Threading.Tasks.Task<bool> SetVisitDate(string userId)
        {
            User user = context.Users.Find(userId);
            user.IsWork = (user.IsWork == true) ? (false) : (true);
            VisitDate vd = new VisitDate
            {
                UserId = userId,
                Date = DateTimeOffset.UtcNow
            };
            //context.VisitDates.Add(vd);
            await context.SaveChangesAsync();
            return user.IsWork;
        }

        public async System.Threading.Tasks.Task<IList<VisitDate>> GetVisitDate(string userId, DateTimeOffset clientTime)
        {
            return await context.VisitDates.Where(vd => vd.UserId == userId)
                .Where(vd => vd.Date.DayOfYear == clientTime.DayOfYear && vd.Date.Year == clientTime.Year)
                .OrderBy(vd => vd.Date)
                .ToListAsync();
        }
    }
}
