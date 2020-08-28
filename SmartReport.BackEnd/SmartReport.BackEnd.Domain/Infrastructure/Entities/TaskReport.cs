using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartReport.BackEnd.Domain.Infrastructure.Entities
{
    public class TaskReport
    {
        [Key]
        public int ReportId { get; set; }
        public virtual Report Report { get; set; }
        [Key]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
