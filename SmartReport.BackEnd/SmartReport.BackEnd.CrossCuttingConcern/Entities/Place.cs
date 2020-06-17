using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.Entities
{
    public class Place
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public Place()
        {
            Tasks = new HashSet<Task>();
        }

    }
}
