using Chuanhoafile.Data;
using Chuanhoafile.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuanhoafile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _hostingEnvironment;
        private IWebHostEnvironment _env;
        public HomeController(ApplicationDbContext context, IHostEnvironment hostingEnvironment, IWebHostEnvironment env)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _env = env;
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

        [HttpPost]
        public async Task<IActionResult> FinishExecute(IFormCollection collect, IFormFile inputb9)
        {
            try
            {
                int hoten = int.Parse(collect["hoten"]);
                int gioitinh = int.Parse(collect["gioitinh"]);
                int manhom = int.Parse(collect["manhom"]);
                int ngaythangnamsinh = int.Parse(collect["ngaythangnamsinh"]);
                int sodienthoai = int.Parse(collect["sodienthoai"]);
                int cmnd = int.Parse(collect["cmnd"]);
                int thebaohiem = int.Parse(collect["thebaohiem"]);
                int tinhthanh = int.Parse(collect["tinhthanh"]);
                int quanhuyen = int.Parse(collect["quanhuyen"]);
                int phuongxa = int.Parse(collect["phuongxa"]);
                int donvi = int.Parse(collect["donvi"]);
                int diachi = int.Parse(collect["diachi"]);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (inputb9 != null)
                {
                    string pathFile = "";
                    if (inputb9.Length > 0)
                    {
                        var filePath = Path.Combine(_env.WebRootPath, "File", "DuLieuTiemChung.xlsx");
                        pathFile = filePath;
                        using (var stream = inputb9.OpenReadStream())
                        {
                            using (ExcelPackage excelPack = new ExcelPackage())
                            {
                                using (ExcelPackage resultSheet = new ExcelPackage())
                                {
                                    try
                                    {
                                        using (FileStream fs = System.IO.File.Open(filePath, FileMode.Open))
                                        {
                                            ExcelPackage templateSheet = new ExcelPackage();
                                            await templateSheet.LoadAsync(fs);
                                            resultSheet.Workbook.Worksheets.Add("sheet_1", CopySheet(templateSheet.Workbook, "sheet_default", "sheet1"));
                                        }
                                        excelPack.Load(stream);
                                        var ws = excelPack.Workbook.Worksheets[0];
                                        var start = ws.Dimension.Start;
                                        var end = ws.Dimension.End;
                                        int rowIndex = int.Parse(collect["rowIndex"].ToString());

                                        var resultWorkSheet = resultSheet.Workbook.Worksheets[0];

                                        int count = 1;
                                        int resultRowIndex = 9;

                                        for (int rowInd = rowIndex; rowInd <= end.Row; rowInd++)
                                        {
                                            string errorlist = "";
                                            resultWorkSheet.Cells[resultRowIndex, 1].Value = count;
                                            if (ws.Cells[rowInd, hoten].Value == null || ws.Cells[rowInd, hoten].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu Họ Tên; ";
                                                resultWorkSheet.Cells[resultRowIndex, 2].Value = "";
                                            }
                                            else
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 2].Value = ws.Cells[rowInd, hoten].Value.ToString();
                                            }
                                            //////// col 2

                                            if (ws.Cells[rowInd, ngaythangnamsinh].Value == null || ws.Cells[rowInd, ngaythangnamsinh].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu Ngày tháng năm sinh; ";
                                                resultWorkSheet.Cells[resultRowIndex, 2].Value = "";
                                            }
                                            else
                                            {
                                                DateTime date;
                                                if (DateTime.TryParse(ws.Cells[rowInd, ngaythangnamsinh].Value.ToString(), out date))
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 3].Value = date.ToString("dd/MM/yyyy");
                                                }
                                                else
                                                {
                                                    errorlist += "Ngày tháng năm sinh sai định dạng; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 3].Value = "";
                                                }
                                            }
                                            //////// col 3
                                            

                                            if (ws.Cells[rowInd, gioitinh].Value == null || ws.Cells[rowInd, gioitinh].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu giới tính; ";
                                                resultWorkSheet.Cells[resultRowIndex, 4].Value = "";
                                            }
                                            else
                                            {
                                                if (ws.Cells[rowInd, gioitinh].Value.ToString().ToLower() == "nam")
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 4].Value = 0;
                                                }
                                                else if(ws.Cells[rowInd, gioitinh].Value.ToString().Trim().ToLower() == "nữ" || ws.Cells[rowInd, gioitinh].Value.ToString().Trim().ToLower().Contains("nu"))
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 4].Value = 1;
                                                }
                                                else
                                                {
                                                    errorlist += "Sai định dạng giới tính; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 4].Value = "";
                                                }
                                                
                                            }
                                            //////// col 4


                                            resultWorkSheet.Cells[resultRowIndex, 18].Value = errorlist;
                                            resultRowIndex++;
                                            count++;
                                        }
                                        string file_name = Guid.NewGuid().ToString().Substring(1, 19) + "_DuLieuTiemChung.xlsx";
                                        filePath = Path.Combine(_env.WebRootPath, "File", file_name);

                                        FileInfo fi = new FileInfo(filePath);
                                        //var result = await resultSheet.GetAsByteArrayAsync();
                                        await resultSheet.SaveAsAsync(fi);                                    //return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","File_da_xu_ly");
                                        return Json(new { status = "success", message = file_name });
                                    }
                                    catch (Exception ex)
                                    {
                                        return Json(new { status = "error", message = ex.Message });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message + "----" + collect["dinhdangngaysinh"].ToString() });
            }
            return Json(new { status = "error", message = "Hệ thống không thể xử lý" });
        }

        [HttpGet]
        public async Task<IActionResult> Download(string file_name)
        {
            string fullPath = Path.Combine(_env.WebRootPath, "File", file_name);
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", file_name);
        }
        public ExcelWorksheet CopySheet(ExcelWorkbook workbook, string existingWorksheetName, string newWorksheetName)
        {
            ExcelWorksheet worksheet = workbook.Worksheets.Copy(existingWorksheetName, newWorksheetName);
            return worksheet;
        }

        [HttpPost]
        public async Task<JsonResult> FileExecute(IFormCollection collect ,IFormFile inputb9)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                string pathFile = "";
                List<ExcelObject> result = new List<ExcelObject>();
                if (inputb9 != null)
                {
                    if (inputb9.Length > 0)
                    {
                        var filePath = Path.Combine(_env.WebRootPath, "File", Guid.NewGuid().ToString() + inputb9.FileName);
                        pathFile = filePath;
                        using (var stream = inputb9.OpenReadStream())
                        {
                            using (ExcelPackage excelPack = new ExcelPackage())
                            {
                                excelPack.Load(stream);
                                var ws = excelPack.Workbook.Worksheets[0];
                                var start = ws.Dimension.Start;
                                var end = ws.Dimension.End;
                                int rowIndex = int.Parse(collect["rowIndex"].ToString());
                                for (int col = start.Column; col <= end.Column; col++)
                                {
                                    var excl = new ExcelObject();
                                    excl.colIndex = col;
                                    excl.name = ws.Cells[rowIndex, col].Text;
                                    if (ws.Cells[rowIndex, col].Text != null && ws.Cells[rowIndex, col].Text != "")
                                    {
                                        result.Add(excl);
                                    }
                                }
                                return Json(result);
                            }
                        }
                    }
                }
                return Json("");
            }
            catch(Exception ex)
            {
                return Json(ex.Data);
            }
            
        }
    }
}
