using Microsoft.AspNetCore.Http;
using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SmartReport.BackEnd.BusinessLogicLayer.Services
{
    public class CurrentWebUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _context;

        public CurrentWebUser(IHttpContextAccessor context)
        {
            _context = context;
        }

        public bool IsAuthenticated
        {
            get
            {
                return _context.HttpContext.User.Identity.IsAuthenticated;
            }
        }

        public string[] Roles
        {
            get
            {
                string role = _context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                return role.Split(" ");
            }
        }

        public Guid UserId
        {
            get
            {
                var userId = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? _context.HttpContext.User.FindFirst("sub")?.Value;
                return Guid.Parse(userId);
            }
        }
    }
}
