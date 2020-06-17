using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.DataAccessLayer.Interfaces
{
    public interface ITaskRepository
    {
        System.Threading.Tasks.Task<IList<TaskDTO>> GetMissedTasks(string userId);
        System.Threading.Tasks.Task CreateTaskForUser(int taskId, string userId);
        System.Threading.Tasks.Task<TaskDTO> Create(TaskDTO taskDTO);
    }
}
