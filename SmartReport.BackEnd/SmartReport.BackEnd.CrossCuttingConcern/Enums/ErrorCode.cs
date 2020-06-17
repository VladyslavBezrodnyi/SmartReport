namespace SmartReport.BackEnd.CrossCuttingConcern.Enums
{
    public enum ErrorCode
    {
        ValidationError = 1,
        DALNotFoundError,
        PermissionError,
        DBError,
        ServerError
    }
}
