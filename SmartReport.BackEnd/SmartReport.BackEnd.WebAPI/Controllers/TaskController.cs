using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;

namespace SmartReport.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public readonly ICurrentUser _currentUser;

        public TaskController(ITaskService taskService, ICurrentUser currentUser)
        {
            _taskService = taskService;
            _currentUser = currentUser;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IList<TaskDTO>> GetAll()
        {
            return await _taskService.GetAll();
        }

        [HttpGet]
        [Route("GetMissedTasks")]
        public async Task<IList<TaskDTO>> GetMissedTasks()
        {
            return await _taskService.GetMissedTasks(_currentUser.UserId.ToString());
        }

        [HttpPost]
        [Route("CreateTaskForUser")]
        public async Task CreateTaskForUser([FromBody]UserTaskDTO userTask)
        {
            await _taskService.CreateTaskForUser(userTask.TaskId, userTask.UserId);
        }

        [HttpPut]
        [Route("create")]
        public async Task<TaskDTO> Create([FromBody]TaskDTO taskDTO)
        {
            return await _taskService.Create(taskDTO);
        }
    }
}
