using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.Mappers
{
    public static class TaskExtension
    {
        public static TaskDTO ToTaskDTO(this Task task)
        {
            TaskDTO taskDTO = new TaskDTO
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                DeadLine = task.DeadLine,
                Place = task.Place?.ToPlaceDTO()
            };
            return taskDTO;
        }
        public static Task ToTask(this TaskDTO taskDTO)
        {
            Task task = new Task
            {
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                StartDate = taskDTO.StartDate,
                DeadLine = taskDTO.DeadLine,
                PlaceId = taskDTO?.Place.Id
            };
            return task;
        }
    }
}