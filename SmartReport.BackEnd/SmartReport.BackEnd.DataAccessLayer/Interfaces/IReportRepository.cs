using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.DataAccessLayer.Interfaces
{
    public interface IReportRepository
    {
        System.Threading.Tasks.Task Create(ReportDTO reportDTO, string userId);
    }
}
