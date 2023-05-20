using Microsoft.Reporting.NETCore;
using System.Collections;
using System.Reflection;
using WebAPIWithRDLC.DTO;

namespace WebAPIWithRDLC.Services
{
    public class ReportService : IReportService
    {
        //public static string ExtractFileNameWithoutExtention(string path)
        //{
        //    string fileName = Path.GetFileName(path);
        //    int lastIndex = fileName.LastIndexOf(".");
        //    if (lastIndex != -1)
        //    {
        //        fileName = fileName.Substring(0, lastIndex);
        //    }
        //    return fileName;
        //}
        private string ExtractFileNameWithoutExtention(string path)
        {
            string fileName = Path.GetFileName(path);
            int lastIndex = fileName.LastIndexOf(".");
            if (lastIndex != -1)
            {
                fileName = fileName.Substring(0, lastIndex);
            }
            return fileName;
        }
        public byte[] GenerateReportAsync(string reportName, string fileType)
        {
            //string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("AspNetCoreRDLC.dll", string.Empty);
            //string rdlcFilePath = string.Format("{0}ReportFiles/{1}.rdlc", fileDirPath, reportName);

            string path = Path.Combine(Environment.CurrentDirectory, @"ReportFiles\", $"{reportName}");
            Stream reportDefinition = File.OpenRead(path); // your RDLC from file or resource
            //Stream reportDefinition = File.OpenRead(rdlcFilePath); // your RDLC from file or resource
            IEnumerable dataSource; // your datasource for the report

            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);

            string reportNameWithOutExtension = ExtractFileNameWithoutExtention(path);

            if (reportNameWithOutExtension == "UserDetails")
            {
                List<UserDto> userList = new List<UserDto>();
                userList.Add(new UserDto
                {
                    FirstName = "Alex ​សិស្សសាលា",
                    LastName = "Smith",
                    Email = "alex.smith@gmail.com",
                    Phone = "2345334432"
                });

                userList.Add(new UserDto
                {
                    FirstName = "John​ កុមារា",
                    LastName = "Legend",
                    Email = "john.legend@gmail.com",
                    Phone = "5633435334"
                });

                userList.Add(new UserDto
                {
                    FirstName = "Stuart",
                    LastName = "Jones",
                    Email = "stuart.jones@gmail.com",
                    Phone = "3575328535"
                });

                dataSource = userList;
                report.DataSources.Add(new ReportDataSource("dsUsers", dataSource));


            }
            else if (reportNameWithOutExtension == "UserProfile")
            {
                // UserDto userProfile = new UserDto();
                // userProfile.FirstName = "ឈិន";
                // userProfile.LastName = "ស្រស់";
                // userProfile.Email = "john.legend@gmail.com";
                // userProfile.Phone = "5633435334";

                List<UserDto> userList = new List<UserDto>();
                userList.Add(new UserDto
                {
                    FirstName = "ស្រស់ ​សិស្សសាលា",
                    LastName = "Chhin",
                    Email = "chhinsras@gmail.com",
                    Phone = "017999740"
                });

                userList.Add(new UserDto
                {
                    FirstName = "Alex ​សិស្សសាលា",
                    LastName = "Smith",
                    Email = "alex.smith@gmail.com",
                    Phone = "2345334432"
                });

                dataSource = userList;
                report.DataSources.Add(new ReportDataSource("dsUser", dataSource));


            }

            // report.SetParameters(new[] { new ReportParameter("Parameter1", "Parameter value") });
            byte[] result;
            if (fileType == "pdf")
            {
                result = report.Render("PDF");
            }
            else if (fileType == "excel")
            {
                result = report.Render("EXCEL"); // WORDOPENXML for cross-plateform
            }
            else if (fileType == "word") //EXCELOPENXML for cross-platform 
            {
                result = report.Render("WORDOPENXML");
            }
            else if (fileType == "html")
            {
                result = report.Render("HTML5"); // HTML4.0 / HTML5 / MHTML for cross-plateform
            }
            else
            {
                result = report.Render("PDF");
            }

            return result;
        }
    }
}
