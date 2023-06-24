using Microsoft.AspNetCore.Mvc;
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


        //public async Task<IActionResult> PrintReport2()
        //{
        //    return await Task.Run(() => PrepareReport("PDF", "pdf", "application/pdf", "rptEPInsuranceBillingPayment"));
        //}

        //public IActionResult PrintReport()
        //{
        //    return PrepareReport("PDF", "pdf", "application/pdf", "rptEPInsuranceBillingPayment");
        //}

        //public IActionResult PrepareReport(string renderFormat, string extension, string mimyType, string reportName)
        //{
        //    try
        //    {
        //        using var report = new LocalReport();

        //        byte[] bytePdf;
        //        string reportPath2 = Path.Combine(Environment.CurrentDirectory, @"Views\Insurance\InsuranceBilling\Reports\", $"{reportName}.rdlc"); //perbaiki urutan foldernya
        //        //Stream reportDefinition = File.OpenRead(reportPath); // your RDLC from file or resource
        //        IEnumerable dataSource1;
        //        IEnumerable dataSource2;
        //        #region List Dokumen

        //        //sementara gunakan data dummy untuk show report
        //        List<rptEpInsuranceBillingPaymentHeaderDTO> returnData1 = new List<rptEpInsuranceBillingPaymentHeaderDTO> {
        //        new rptEpInsuranceBillingPaymentHeaderDTO{ Tanggal=DateTime.Now.ToString(), DariRekening="PT. MEGA CENTRAL FINANCE" , NoRekening="010020011017825 (MEGA)", BilyetGiroCek=""},
        //        };

        //        List<rptEPInsuranceBillingPaymentDetailDTO> returnData2 = new List<rptEPInsuranceBillingPaymentDetailDTO>{
        //        new rptEPInsuranceBillingPaymentDetailDTO {No="1",Asuransi="PT  XYZ 1",AtasNamaRekening="PT XYZ 11", NoRekening="12314125125", NamaBank="MANDIRI", NilaiTranfer=100000 },
        //        new rptEPInsuranceBillingPaymentDetailDTO {No="2",Asuransi="PT  XYZ 2",AtasNamaRekening="PT XYZ 112", NoRekening="12314125125", NamaBank="MANDIRI", NilaiTranfer=1215235 },
        //        new rptEPInsuranceBillingPaymentDetailDTO {No="3",Asuransi="PT  XYZ 3",AtasNamaRekening="PT XYZ 113", NoRekening="12314125125", NamaBank="MANDIRI", NilaiTranfer=12412515 },
        //        new rptEPInsuranceBillingPaymentDetailDTO {No="4",Asuransi="PT  XYZ 4",AtasNamaRekening="PT XYZ 114", NoRekening="12314125125", NamaBank="MANDIRI", NilaiTranfer=324634666 },

        //        };

        //        dataSource1 = returnData1;
        //        dataSource2 = returnData2;



        //        Stream reportDefinition = System.IO.File.OpenRead(reportPath2);
        //        report.LoadReportDefinition(reportDefinition);

        //        report.DataSources.Clear();
        //        report.DataSources.Add(new ReportDataSource("Header", dataSource1));
        //        report.DataSources.Add(new ReportDataSource("DetailData", dataSource2));

        //        //set parameter to report
        //        ReportParameter parameter = new ReportParameter("user", "Sutanto");
        //        report.SetParameters(parameter);

        //        #endregion

        //        bytePdf = report.Render(renderFormat);
        //        return File(bytePdf, "application/pdf", "Report Insurance Billing Payment." + extension);

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
        //        return RedirectToAction("List");
        //    }


        //}
    }
}
