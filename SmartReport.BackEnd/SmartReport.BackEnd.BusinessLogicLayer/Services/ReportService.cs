using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.BusinessLogicLayer.Services
{
    public class ReportService: IReportService
    {
        public readonly IUnitOfWork _db;
        public ReportService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task Create(ReportDTO reportDTO, string userId)
        {
            await _db.Reports.Create(reportDTO, userId);
        }
    }
}
