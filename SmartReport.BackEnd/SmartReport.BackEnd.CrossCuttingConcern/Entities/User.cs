using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SmartReport.BackEnd.CrossCuttingConcern.Entities
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public bool IsWork { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<UserTask> UserTasks { get; set; }
        public virtual ICollection<VisitDate> VisitDates { get; set; }
        public User()
        {
            Reports = new HashSet<Report>();
            UserTasks = new HashSet<UserTask>();
            VisitDates = new HashSet<VisitDate>();
        }
    }
}
