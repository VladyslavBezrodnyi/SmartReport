using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using SmartReport.BackEnd.CrossCuttingConcern.Mappers;
using SmartReport.BackEnd.DataAccessLayer.Contexts;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.DataAccessLayer.Repositories
{
    public class ReportRepository: IReportRepository
    {
        private ApplicationDbContext _context;
        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task Create(ReportDTO reportDTO, string userId)
        {
            Report newReport = reportDTO.ToReport();
            newReport.UserId = userId;
            await _context.Reports.AddAsync(newReport);
            await _context.SaveChangesAsync();

            if (reportDTO.Tasks != null)
            {
                IList<int> taskIds = reportDTO.Tasks.Where(t => t.Id != null)
                    .Select(t => t.Id.Value)
                    .Distinct()
                    .ToList();
                IList<UserTask> userTasks = _context.UserTasks
                    .Where(ut => ut.UserId == userId && 
                    !ut.IsDone && 
                    taskIds.Contains(ut.TaskId))
                    .ToList();
                foreach (UserTask userTask in userTasks)
                {
                    TaskReport tr = new TaskReport
                    {
                        TaskId = userTask.TaskId,
                        ReportId = newReport.Id
                    };
                    userTask.IsDone = true;
                    await _context.TaskReports.AddAsync(tr);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<ICollection<ReportDTO>> GetByUserId(string userId)
        {
            return await _context.Reports
                .Include(r => r.TaskReports)
                .ThenInclude(tr => tr.Task)
                .Where(r => r.UserId == userId)
                .Select(r => r.ToReportDTO())
                .ToListAsync();
        }
    }
}
