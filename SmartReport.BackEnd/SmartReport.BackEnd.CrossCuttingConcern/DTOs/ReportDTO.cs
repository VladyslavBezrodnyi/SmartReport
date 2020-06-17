using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.DTOs
{
    public class ReportDTO
    {
        public int? Id { get; set; }
        public string ReportText { get; set; }
        public DateTime Date { get; set; }
        public IList<TaskDTO> Tasks { get; set; }
    }
}
