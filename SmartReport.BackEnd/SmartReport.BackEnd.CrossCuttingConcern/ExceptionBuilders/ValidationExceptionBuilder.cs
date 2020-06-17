using SmartReport.BackEnd.CrossCuttingConcern.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.ExceptionBuilders
{
    public static class ValidationExceptionBuilder
    {
        public static ValidationException BuildValidationException(ValidationResults validationResults)
        {
            var errorMessages = validationResults.ValidationResultsMessages;
            StringBuilder summaryErrorMessage = new StringBuilder();
            foreach (ValidationResult result in errorMessages)
            {
                summaryErrorMessage.Append(result.ErrorMessage);
            }
            return new ValidationException(summaryErrorMessage.ToString());
        }

    }
}
