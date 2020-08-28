using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.BusinessLogicLayer.Interfaces
{
    public interface IReportService
    {
        Task Create(ReportDTO reportDTO, string userId);
        System.Threading.Tasks.Task<ICollection<ReportDTO>> GetByUserId(string userId);
    }
}
