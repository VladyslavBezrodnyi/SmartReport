using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.Domain.Services.Interfaces
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        string[] Roles { get; }
        Guid UserId { get; }
    }
}
