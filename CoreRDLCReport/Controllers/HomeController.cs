using AspNetCore.Reporting;
using CoreRDLCReport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace CoreRDLCReport.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        }

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IActionResult Print()
        {
            var dt = new DataTable();
            dt = GetEmployeeList();
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Employees.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC report (Set as parameter)");
            LocalReport lr = new LocalReport(path);
            lr.AddDataSource("dsEmployee", dt);
            var result = lr.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }

        public IActionResult Export()
        {
            var dt = new DataTable();
            dt = GetEmployeeList();
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Employees.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC report (Set as parameter)");
            LocalReport lr = new LocalReport(path);
            lr.AddDataSource("dsEmployee", dt);
            var result = lr.Execute(RenderType.Excel, extension, parameters, mimetype);
            return File(result.MainStream, "application/msexcel", "Export.xls");
        }
        private DataTable GetEmployeeList()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("Department");
            dt.Columns.Add("BirthDate");
            DataRow row;

            for (int i = 1; i < 100; i++)
            {
                row = dt.NewRow();
                row["EmpId"] = i;
                row["EmpName"] = i.ToString() + " Empl";
                row["Department"] = "Information Technology";
                row["BirthDate"] = DateTime.Now.AddDays(-10000);
                dt.Rows.Add(row);
            }
            return dt;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}