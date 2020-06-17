using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;

namespace SmartReport.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ICurrentUser _currentUser;

        public ReportController(IReportService reportService,
            ICurrentUser currentUser)
        {
            _reportService = reportService;
            _currentUser = currentUser;
        }

        [HttpPut]
        [Route("create")]
        public async Task Create([FromBody] ReportDTO reportDTO)
        {
            await _reportService.Create(reportDTO, _currentUser.UserId.ToString());
        }
    }
}
