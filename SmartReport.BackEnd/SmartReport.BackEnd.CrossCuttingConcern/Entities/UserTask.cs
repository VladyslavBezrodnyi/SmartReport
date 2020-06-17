using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.Entities
{
    public class UserTask
    {
        [Key]
        public string UserId { get; set; }
        public User User { get; set; }
        [Key]
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public bool IsDone { get; set; }
    }
}
