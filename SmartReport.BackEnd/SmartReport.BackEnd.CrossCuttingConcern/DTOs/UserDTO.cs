using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public IList<ReportDTO> Reports { get; set; }
        public IList<TaskDTO> UserTasks { get; set; }
    }
}
