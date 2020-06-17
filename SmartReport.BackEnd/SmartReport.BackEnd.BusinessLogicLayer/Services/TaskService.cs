using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.BusinessLogicLayer.Services
{
    public class TaskService: ITaskService
    {
        public readonly IUnitOfWork _db;
        public TaskService(IUnitOfWork db)
        {
            _db = db;
        }
        public async Task<IList<TaskDTO>> GetMissedTasks(string userId)
        {
            return await _db.Tasks.GetMissedTasks(userId);
        }

        public async System.Threading.Tasks.Task CreateTaskForUser(int taskId, string userId)
        {
            await _db.Tasks.CreateTaskForUser(taskId, userId);
        }
        public async System.Threading.Tasks.Task<TaskDTO> Create(TaskDTO taskDTO)
        {
            return await _db.Tasks.Create(taskDTO);
        }
    }
}
