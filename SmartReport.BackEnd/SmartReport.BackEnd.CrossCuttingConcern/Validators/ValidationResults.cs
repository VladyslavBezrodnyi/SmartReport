using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartReport.BackEnd.CrossCuttingConcern.Validators
{
    public class ValidationResults
    {
        public List<ValidationResult> ValidationResultsMessages { get; set; }
        public bool Successed { get; set; }
    }
}
