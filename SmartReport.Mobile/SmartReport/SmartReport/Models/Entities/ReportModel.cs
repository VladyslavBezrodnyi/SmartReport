using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.Models.Entities
{
    public class ReportModel
    {
        public int Id { get; set; }

        public string ReportText { get; set; }

        public DateTime Date { get; set; }

        public List<TaskModel> Tasks { get; set; }
    }
}
