using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.Mappers
{
    public static class ReportExtension
    {
        public static ReportDTO ToReportDTO(this Report report)
        {
            ReportDTO reportDTO = new ReportDTO
            {
                Id = report.Id,
                ReportText = report.ReportText,
                Date = report.Date,
                Tasks = report.TaskReports?.Where(tr => tr.Task != null).Select(tr => tr.Task?.ToTaskDTO()).ToList()
            };
            return reportDTO;
        }
        public static Report ToReport(this ReportDTO reportDTO)
        {
            Report report = new Report
            {
                ReportText = reportDTO.ReportText,
                Date = reportDTO.Date,
            };
            return report;
        }
    }
}