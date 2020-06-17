using Microsoft.EntityFrameworkCore;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using SmartReport.BackEnd.CrossCuttingConcern.Mappers;
using SmartReport.BackEnd.DataAccessLayer.Contexts;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.DataAccessLayer.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IList<TaskDTO>> GetMissedTasks(string userId)
        {
            return await _context.UserTasks
                .Include(ut => ut.Task)
                .Where(ut => ut.UserId == userId && !ut.IsDone)
                .Select(ut => ut.Task.ToTaskDTO())
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task CreateTaskForUser(int taskId, string userId)
        {
            UserTask userTask = new UserTask
            {
                UserId = userId,
                TaskId = taskId,
                IsDone = false
            };
            await _context.UserTasks.AddAsync(userTask);
            await _context.SaveChangesAsync();
        }
        public async System.Threading.Tasks.Task<TaskDTO> Create(TaskDTO taskDTO)
        {
            CrossCuttingConcern.Entities.Task newTask = taskDTO.ToTask();
            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
            return newTask.ToTaskDTO();

        }
    }
}
