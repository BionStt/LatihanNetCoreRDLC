namespace WebAPIWithRDLC.Services
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string reportName, string fileType);

    }
}
