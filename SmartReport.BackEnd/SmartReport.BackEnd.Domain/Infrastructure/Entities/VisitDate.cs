using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartReport.BackEnd.Domain.Infrastructure.Entities
{
    public class VisitDate
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
