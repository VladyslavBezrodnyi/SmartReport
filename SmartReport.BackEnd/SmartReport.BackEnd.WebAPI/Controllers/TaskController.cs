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
        [Route("GetMissedTasks")]
        public async Task CreGetMissedTasksate()
        {
            await _taskService.GetMissedTasks(_currentUser.UserId.ToString());
        }

        [HttpPost]
        [Route("CreateTaskForUser/{taskId}/{userId}")]
        public async Task CreateTaskForUser(int taskId, string userId)
        {
            await _taskService.CreateTaskForUser(taskId, userId);
        }

        [HttpPut]
        [Route("create")]
        public async Task Create([FromBody]TaskDTO taskDTO)
        {
            await _taskService.Create(taskDTO);
        }
    }
}
