using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.DTOs
{
    public class VisitDateDTO
    {
        public string UserId { get; set; }
        public bool IsWork { get; set; }
        public TimeSpan WorkTime { get; set; }
    }
}
