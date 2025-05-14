using System.Diagnostics;
using System.Text;
using ClosedXML.Excel;
using CoursesAdminApplication.Models;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ExcelDataReader;

namespace CoursesAdminApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };

            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:44379/")
            };

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("api/Admin/GetAllTransfers");
            var result = await response.Content.ReadAsAsync<List<TransferRequest>>();

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ImportUsers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportUsers(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<UserDTO> users = getAllUsersFromFile(file.FileName);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/Admin/ImportAllUsers", content);
            var result = await response.Content.ReadAsAsync<bool>();

            return RedirectToAction("Index", "Home");
        }

        private List<UserDTO> getAllUsersFromFile(string fileName)
        {
            var users = new List<UserDTO>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        users.Add(new UserDTO
                        {
                            Email = reader.GetValue(0)?.ToString(),
                            Password = reader.GetValue(1)?.ToString(),
                            ConfirmPassword = reader.GetValue(2)?.ToString()
                        });
                    }
                }
            }

            return users;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = new { Id = id };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Admin/GetDetailsForTransfers", content);
            var result = await response.Content.ReadAsAsync<TransferRequest>();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ExportTransferRequests()
        {
            string fileName = "TransferRequests.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var response = await _client.GetAsync("api/TransferRequest/GetAll");
            var result = await response.Content.ReadAsAsync<List<TransferRequest>>();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Transfer Requests");

                worksheet.Cell(1, 1).Value = "Request ID";
                worksheet.Cell(1, 2).Value = "Created By";
                worksheet.Cell(1, 3).Value = "Date Created";
                worksheet.Cell(1, 5).Value = "Courses";

                for (int i = 0; i < result.Count; i++)
                {
                    var currentRequest = result[i];

                    worksheet.Cell(i + 2, 1).Value = currentRequest.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = currentRequest.CreatedBy?.Name ?? "N/A";
                    worksheet.Cell(i + 2, 3).Value = currentRequest.DateCreated.ToString("yyyy-MM-dd HH:mm");

                    var courseNames = string.Join(", ",
                        currentRequest.CourseTransfers
                            .Where(c => c?.Enrolment?.Course?.CourseName != null)
                            .Select(c => c.Enrolment.Course.CourseName));

                    worksheet.Cell(i + 2, 5).Value = string.IsNullOrEmpty(courseNames) ? "No courses" : courseNames;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }
        }

        public async Task<IActionResult> CreateInvoice(Guid id)
        {
            var model = new { Id = id };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Admin/GetDetailsForTransfers", content);
            var result = await response.Content.ReadAsAsync<TransferRequest>();

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);

            StringBuilder userSb = new StringBuilder();
            userSb.Append(result.CreatedBy?.Name ?? "FirstName");
            userSb.Append(" - ");
            userSb.Append(result.CreatedBy?.Surname ?? "LastName");
            userSb.Append(" - ");
            userSb.Append(result.CreatedBy?.Email ?? "Email");

            document.Content.Replace("{{TransferNumber}}", result.Id.ToString());
            document.Content.Replace("{{UserName}}", userSb.ToString());

            StringBuilder courseListBuilder = new StringBuilder();
            foreach (var course in result.CourseTransfers)
            {
                if (course?.Enrolment?.Course?.CourseName != null)
                {
                    courseListBuilder.AppendLine(course.Enrolment.Course.CourseName);
                }
                else
                {
                    courseListBuilder.AppendLine("Unknown course");
                }
            }

            document.Content.Replace("{{CourseList}}", courseListBuilder.ToString());

            int validCourseCount = result.CourseTransfers.Count(c => c?.Enrolment?.Course?.CourseName != null);
            document.Content.Replace("{{TotalCourses}}", validCourseCount.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }
    }
}
