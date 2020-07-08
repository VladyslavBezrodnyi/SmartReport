using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartReport.BackEnd.DataAccessLayer.Contexts;

namespace SmartReport.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataBase : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DataBase(ApplicationDbContext db)
        {
            _context = db;
        }

        [HttpGet("restore-db")]
        public async Task<string> Restore()
        {
            var backupName = $"db-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.bak";
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\backups", backupName);

            string dbname = _context.Database.GetDbConnection().Database;
            string sqlCommand = @"BACKUP DATABASE [{0}] TO  DISK = '{1}'";
            await _context.Database.ExecuteSqlRawAsync(string.Format(sqlCommand, dbname, path));

            return "/backups";
        }
    }
}
