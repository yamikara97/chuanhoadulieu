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
    public class HSSKController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _hostingEnvironment;
        private IWebHostEnvironment _env;
        public HSSKController(ApplicationDbContext context, IHostEnvironment hostingEnvironment, IWebHostEnvironment env)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _env = env;
        }
        public IActionResult Index()
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
                int email = int.Parse(collect["email"]);
                int nghenghiep = int.Parse(collect["nghenghiep"]);
                int manhom = int.Parse(collect["manhom"]);
                int ngaythangnamsinh = int.Parse(collect["ngaythangnamsinh"]);
                int sodienthoai = int.Parse(collect["sodienthoai"]);
                int cmnd = int.Parse(collect["cmnd"]);
                int thebaohiem = int.Parse(collect["thebaohiem"]);
                int tinhthanh = int.Parse(collect["tinhthanh"]);
                int quanhuyen = int.Parse(collect["quanhuyen"]);
                int phuongxa = int.Parse(collect["phuongxa"]);
                int donvi = int.Parse(collect["donvi"]);
                int dantoc = int.Parse(collect["dantoc"]);
                int quoctich = int.Parse(collect["quoctich"]);
                int diachi = int.Parse(collect["diachi"]);
                int cosotiem = int.Parse(collect["cosotiem"]);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (inputb9 != null)
                {
                    string pathFile = "";
                    if (inputb9.Length > 0)
                    {
                        var filePath = Path.Combine(_env.WebRootPath, "File", collect["rowTypeFile"] + ".xlsx");
                        pathFile = filePath;
                        using (var stream = inputb9.OpenReadStream())
                        {
                            using (ExcelPackage excelPack = new ExcelPackage())
                            {
                                string file_name = Guid.NewGuid().ToString().Substring(1, 19) + "_DuLieuTiemChung.xlsx";
                                string filePathreturn = Path.Combine(_env.WebRootPath, "File", file_name);
                                var fileinfo = new FileInfo(filePathreturn);
                                System.IO.File.Copy(filePath, filePathreturn);
                                using (ExcelPackage resultSheet = new ExcelPackage(fileinfo))
                                {
                                    try
                                    {
                                        excelPack.Load(stream);
                                        var ws = excelPack.Workbook.Worksheets[0];
                                        var start = ws.Dimension.Start;
                                        var end = ws.Dimension.End;
                                        int rowIndex = int.Parse(collect["rowIndex"].ToString());

                                        var resultWorkSheet = resultSheet.Workbook.Worksheets[0];

                                        int count = 1;
                                        int resultRowIndex = 4;

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
                                            ///

                                            if (ws.Cells[rowInd, gioitinh].Value == null || ws.Cells[rowInd, gioitinh].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu giới tính; ";
                                                resultWorkSheet.Cells[resultRowIndex, 3].Value = "";
                                            }
                                            else
                                            {
                                                if (ws.Cells[rowInd, gioitinh].Value.ToString().ToLower() == "nam")
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 3].Value = 1;
                                                }
                                                else if (ws.Cells[rowInd, gioitinh].Value.ToString().Trim().ToLower() == "nữ" || ws.Cells[rowInd, gioitinh].Value.ToString().Trim().ToLower().Contains("nu"))
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 3].Value = 2;
                                                }
                                                else
                                                {
                                                    errorlist += "Sai định dạng giới tính; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 3].Value = "";
                                                }

                                            }
                                            //////// col 3

                                            if (ws.Cells[rowInd, ngaythangnamsinh].Value == null || ws.Cells[rowInd, ngaythangnamsinh].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu Ngày tháng năm sinh; ";
                                                resultWorkSheet.Cells[resultRowIndex, 4].Value = "";
                                            }
                                            else
                                            {
                                                DateTime date;
                                                if (DateTime.TryParse(ws.Cells[rowInd, ngaythangnamsinh].Value.ToString(), out date))
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 4].Value = date.ToString("yyyyMMdd");
                                                }
                                                else
                                                {
                                                    string date2 = ChuanhoaDate(ws.Cells[rowInd, ngaythangnamsinh].Value.ToString());
                                                    DateTime datefinal;
                                                    if (DateTime.TryParse(date2, out datefinal))
                                                    {
                                                        resultWorkSheet.Cells[resultRowIndex, 4].Value = datefinal.ToString("yyyyMMdd");
                                                    }
                                                    else
                                                    {
                                                        errorlist += "Ngày tháng năm sinh sai định dạng; ";
                                                        resultWorkSheet.Cells[resultRowIndex, 4].Value = "";
                                                    }
                                                }
                                            }
                                            //////// col 4
                                            if (ws.Cells[rowInd, email].Value == null || ws.Cells[rowInd, email].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu Email; ";
                                                resultWorkSheet.Cells[resultRowIndex, 5].Value = "";
                                            }
                                            else
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 5].Value = ws.Cells[rowInd, email].Value.ToString();

                                            }
                                            //////// col 5
                                            if (ws.Cells[rowInd, manhom].Value == null || ws.Cells[rowInd, manhom].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu Mã nhóm; ";
                                                resultWorkSheet.Cells[resultRowIndex, 6].Value = "";
                                            }
                                            else
                                            {
                                                int Ma;
                                                if (int.TryParse(ws.Cells[rowInd, manhom].Value.ToString(), out Ma))
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 6].Value = Ma.ToString();
                                                }
                                                else
                                                {
                                                    errorlist += "Mã nhóm sai định dạng; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 6].Value = "";
                                                }
                                            }
                                            //////// col 6
                                            ///
                                            if (ws.Cells[rowInd, nghenghiep].Value == null || ws.Cells[rowInd, nghenghiep].Value.ToString() == "")
                                            {

                                                resultWorkSheet.Cells[resultRowIndex, 7].Value = "";
                                            }
                                            else
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 7].Value = ws.Cells[rowInd, nghenghiep].Value.ToString();

                                            }
                                            //////// col 7
                                            ///  ///
                                            if (ws.Cells[rowInd, donvi].Value == null || ws.Cells[rowInd, donvi].Value.ToString() == "")
                                            {

                                                resultWorkSheet.Cells[resultRowIndex, 8].Value = "";
                                            }
                                            else
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 8].Value = ws.Cells[rowInd, donvi].Value.ToString();

                                            }
                                            //////// col 8

                                            if (ws.Cells[rowInd, sodienthoai].Value == null || ws.Cells[rowInd, sodienthoai].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu số điện thoại; ";
                                                resultWorkSheet.Cells[resultRowIndex, 9].Value = "";
                                            }
                                            else
                                            {
                                                string phonenum = ws.Cells[rowInd, sodienthoai].Value.ToString().Replace("+", "").Replace(" ", "").Replace(".", "").Replace("-", "").Replace(" ", "").Trim();
                                                while (phonenum[0] == '0')
                                                {
                                                    phonenum = phonenum.Substring(1);
                                                }
                                                if (phonenum.Length == 9)
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 9].Value = "0" + phonenum;
                                                }
                                                else
                                                {
                                                    errorlist += "Số điện thoại " + phonenum + " không hợp lệ; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 9].Value = "";
                                                }
                                            }
                                            //////// col 9

                                            if (ws.Cells[rowInd, cmnd].Value == null || ws.Cells[rowInd, cmnd].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu số cmnd; ";
                                                resultWorkSheet.Cells[resultRowIndex, 10].Value = "";
                                            }
                                            else
                                            {
                                                string cmndS = ws.Cells[rowInd, cmnd].Value.ToString().Replace(" ", "").Replace(".", "").Replace("-", "").Replace(" ", "").Trim();
                                                if (cmndS.Length == 8 || cmndS.Length == 9 || cmndS.Length == 12)
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 10].Value = cmndS;
                                                }
                                                else
                                                {
                                                    errorlist += "CMND/CCCD " + cmndS + " không hợp lệ ; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 10].Value = "";
                                                }
                                            }
                                            //////// col 10
                                            ///
                                            if (ws.Cells[rowInd, thebaohiem].Value == null || ws.Cells[rowInd, thebaohiem].Value.ToString() == "")
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 11].Value = "";
                                            }
                                            else
                                            {
                                                string bhyt = ws.Cells[rowInd, thebaohiem].Value.ToString().Replace(" ", "").Replace(".", "").Replace("-", "").Replace(" ", "").Trim();
                                                if (bhyt.Length == 10 || bhyt.Length == 15)
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 11].Value = bhyt;
                                                }
                                                else
                                                {
                                                    errorlist += "Mã thẻ BHYT " + bhyt + " số ký tự: " + bhyt.Length + " không hợp lệ (Không bắt buộc, nếu không sửa được thì để trống); ";
                                                    resultWorkSheet.Cells[resultRowIndex, 11].Value = "";
                                                }
                                            }
                                            //////// col 11
                                            ///
                                            if (ws.Cells[rowInd, dantoc].Value == null || ws.Cells[rowInd, dantoc].Value.ToString() == "")
                                            {

                                                resultWorkSheet.Cells[resultRowIndex, 12].Value = "";
                                            }
                                            else
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 12].Value = ws.Cells[rowInd, dantoc].Value.ToString();

                                            }
                                            //////// col 12

                                            if (ws.Cells[rowInd, quoctich].Value == null || ws.Cells[rowInd, quoctich].Value.ToString() == "")
                                            {

                                                resultWorkSheet.Cells[resultRowIndex, 13].Value = "";
                                            }
                                            else
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 13].Value = ws.Cells[rowInd, quoctich].Value.ToString();

                                            }
                                            //////// col 13


                                            string tpCode = "none";
                                            string qhcode = "none";
                                            if (ws.Cells[rowInd, tinhthanh].Value == null || ws.Cells[rowInd, tinhthanh].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu tỉnh thành; ";
                                                resultWorkSheet.Cells[resultRowIndex, 14].Value = "";
                                                resultWorkSheet.Cells[resultRowIndex, 15].Value = "";
                                            }
                                            else
                                            {
                                                var tinhthanhcell = GetCorrectName(ws.Cells[rowInd, tinhthanh].Value.ToString(), "");
                                                if (tinhthanhcell != null)
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 14].Value = tinhthanhcell.NameOutput;
                                                    resultWorkSheet.Cells[resultRowIndex, 15].Value = tinhthanhcell.Code;
                                                    tpCode = tinhthanhcell.Code;
                                                }
                                                else
                                                {
                                                    errorlist += "tỉnh thành: " + ws.Cells[rowInd, tinhthanh].Value.ToString() + " không tồn tại hoặc sai; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 14].Value = "";
                                                    resultWorkSheet.Cells[resultRowIndex, 15].Value = "";
                                                }

                                            }
                                            //////// col 14-15
                                            ///
                                            if (ws.Cells[rowInd, quanhuyen].Value == null || ws.Cells[rowInd, quanhuyen].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu quận huyện; ";
                                                resultWorkSheet.Cells[resultRowIndex, 16].Value = "";
                                                resultWorkSheet.Cells[resultRowIndex, 17].Value = "";
                                            }
                                            else
                                            {
                                                var quanhuyencell = GetCorrectName(ws.Cells[rowInd, quanhuyen].Value.ToString(), tpCode);
                                                if (quanhuyencell != null)
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 16].Value = quanhuyencell.NameOutput;
                                                    resultWorkSheet.Cells[resultRowIndex, 17].Value = quanhuyencell.Code;
                                                    qhcode = quanhuyencell.Code;
                                                }
                                                else
                                                {
                                                    errorlist += "quận huyện: " + ws.Cells[rowInd, quanhuyen].Value.ToString() + " không tồn tại hoặc sai; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 16].Value = "";
                                                    resultWorkSheet.Cells[resultRowIndex, 17].Value = "";
                                                }

                                            }
                                            //////// col 16-17


                                            if (ws.Cells[rowInd, phuongxa].Value == null || ws.Cells[rowInd, phuongxa].Value.ToString() == "")
                                            {
                                                errorlist += "Thiếu phường xã; ";
                                                resultWorkSheet.Cells[resultRowIndex, 18].Value = "";
                                                resultWorkSheet.Cells[resultRowIndex, 19].Value = "";
                                            }
                                            else
                                            {
                                                var phuongxacell = GetCorrectName(ws.Cells[rowInd, phuongxa].Value.ToString(), qhcode);
                                                if (phuongxacell != null)
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 18].Value = phuongxacell.NameOutput;
                                                    resultWorkSheet.Cells[resultRowIndex, 19].Value = phuongxacell.Code;
                                                }
                                                else
                                                {
                                                    errorlist += "phường xã: " + ws.Cells[rowInd, phuongxa].Value.ToString() + " không tồn tại hoặc sai; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 18].Value = "";
                                                    resultWorkSheet.Cells[resultRowIndex, 19].Value = "";
                                                }

                                            }
                                            //////// col 18-19
                                            ///

                                            if (ws.Cells[rowInd, diachi].Value == null || ws.Cells[rowInd, diachi].Value.ToString() == "")
                                            {

                                                resultWorkSheet.Cells[resultRowIndex, 20].Value = "";
                                            }
                                            else
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 20].Value = ws.Cells[rowInd, diachi].Value.ToString();

                                            }
                                            //////// col 20
                                            ///

                                            if (ws.Cells[rowInd, cosotiem].Value == null || ws.Cells[rowInd, cosotiem].Value.ToString() == "")
                                            {

                                                resultWorkSheet.Cells[resultRowIndex, 21].Value = "";
                                            }
                                            else
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 21].Value = ws.Cells[rowInd, cosotiem].Value.ToString();

                                            }
                                            //////// col 21
                                            ///


                                            if (filePath.Contains("mau3"))
                                            {
                                                resultWorkSheet.Cells[resultRowIndex, 23].Value = errorlist;
                                            }
                                            if (filePath.Contains("mau4"))
                                            {
                                                int vacxin1 = int.Parse(collect["vacxin1"]);
                                                int vacxin2 = int.Parse(collect["vacxin2"]);
                                                int ngaytiem1 = int.Parse(collect["ngaytiem1"]);
                                                int lovacxin1 = int.Parse(collect["lovacxin1"]);
                                                int diadiemtiem1 = int.Parse(collect["diadiemtiem1"]);
                                                int ngaytiem2 = int.Parse(collect["ngaytiem2"]);
                                                int lovacxin2 = int.Parse(collect["lovacxin2"]);
                                                int diadiemtiem2 = int.Parse(collect["diadiemtiem2"]);


                                                if (ws.Cells[rowInd, vacxin1].Value == null || ws.Cells[rowInd, vacxin1].Value.ToString() == "")
                                                {
                                                    errorlist += "Thiếu tên vắc xin 1; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 23].Value = "";
                                                }
                                                else
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 23].Value = ws.Cells[rowInd, vacxin1].Value.ToString();

                                                }
                                                //////// col vacxin1
                                                ///

                                                if (ws.Cells[rowInd, ngaytiem1].Value == null || ws.Cells[rowInd, ngaytiem1].Value.ToString() == "")
                                                {
                                                    errorlist += "Thiếu Ngày tháng tiêm mũi 1; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 24].Value = "";
                                                }
                                                else
                                                {
                                                    DateTime date;
                                                    if (DateTime.TryParse(ws.Cells[rowInd, ngaytiem1].Value.ToString(), out date))
                                                    {
                                                        resultWorkSheet.Cells[resultRowIndex, 24].Value = date.Hour == 0 ? date.ToString("yyyyMMdd") + " 09:00" : date.ToString("yyyyMMdd HH:ss");
                                                    }
                                                    else
                                                    {
                                                        string date2 = ChuanhoaDate(ws.Cells[rowInd, ngaytiem1].Value.ToString());
                                                        DateTime datefinal;
                                                        if (DateTime.TryParse(date2, out datefinal))
                                                        {
                                                            resultWorkSheet.Cells[resultRowIndex, 24].Value = datefinal.Hour == 0 ? datefinal.ToString("yyyyMMdd") + " 09:00" : datefinal.ToString("yyyyMMdd HH:ss");
                                                        }
                                                        else
                                                        {
                                                            errorlist += "Ngày tháng tiêm mũi 1 sai định dạng; ";
                                                            resultWorkSheet.Cells[resultRowIndex, 24].Value = "";
                                                        }
                                                    }
                                                }
                                                //////// col ngày tiêm 1
                                                if (ws.Cells[rowInd, lovacxin1].Value == null || ws.Cells[rowInd, lovacxin1].Value.ToString() == "")
                                                {
                                                    errorlist += "Thiếu tên lô vắc xin 1; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 25].Value = "";
                                                }
                                                else
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 25].Value = ws.Cells[rowInd, lovacxin1].Value.ToString();

                                                }
                                                //////// col lovacxin1


                                                if (ws.Cells[rowInd, vacxin2].Value == null || ws.Cells[rowInd, vacxin2].Value.ToString() == "")
                                                {
                                                    errorlist += "Thiếu tên vắc xin 2; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 26].Value = "";
                                                }
                                                else
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 26].Value = ws.Cells[rowInd, vacxin2].Value.ToString();

                                                }
                                                //////// col vacxin 2
                                                ///

                                                if (ws.Cells[rowInd, ngaytiem2].Value == null || ws.Cells[rowInd, ngaytiem2].Value.ToString() == "")
                                                {
                                                    errorlist += "Thiếu Ngày tháng tiêm mũi 2; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 27].Value = "";
                                                }
                                                else
                                                {
                                                    DateTime date;
                                                    if (DateTime.TryParse(ws.Cells[rowInd, ngaytiem2].Value.ToString(), out date))
                                                    {
                                                        resultWorkSheet.Cells[resultRowIndex, 27].Value = date.Hour == 0 ? date.ToString("yyyyMMdd") + " 09:00" : date.ToString("yyyyMMdd HH:ss");
                                                    }
                                                    else
                                                    {
                                                        string date2 = ChuanhoaDate(ws.Cells[rowInd, ngaytiem2].Value.ToString());
                                                        DateTime datefinal;
                                                        if (DateTime.TryParse(date2, out datefinal))
                                                        {
                                                            resultWorkSheet.Cells[resultRowIndex, 27].Value = datefinal.Hour == 0 ? datefinal.ToString("yyyyMMdd") + " 09:00" : datefinal.ToString("yyyyMMdd HH:ss");
                                                        }
                                                        else
                                                        {
                                                            errorlist += "Ngày tháng tiêm mũi 2 sai định dạng; ";
                                                            resultWorkSheet.Cells[resultRowIndex, 27].Value = "";
                                                        }
                                                    }
                                                }
                                                //////// col ngày tiêm 2
                                                if (ws.Cells[rowInd, lovacxin2].Value == null || ws.Cells[rowInd, lovacxin2].Value.ToString() == "")
                                                {
                                                    errorlist += "Thiếu tên lô vắc xin 2; ";
                                                    resultWorkSheet.Cells[resultRowIndex, 28].Value = "";
                                                }
                                                else
                                                {
                                                    resultWorkSheet.Cells[resultRowIndex, 28].Value = ws.Cells[rowInd, lovacxin1].Value.ToString();

                                                }
                                                //////// col lovacxin 2

                                                resultWorkSheet.Cells[resultRowIndex, 30].Value = errorlist;
                                            }
                                            resultRowIndex++;
                                            count++;
                                        }

                                        //var result = await resultSheet.GetAsByteArrayAsync();
                                        /*      await resultSheet.SaveAsAsync(fi);    */
                                        resultSheet.Save();  //return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","File_da_xu_ly");
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
            System.IO.File.Delete(fullPath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", file_name);
        }
        public ExcelWorksheet CopySheet(ExcelWorkbook workbook, string existingWorksheetName, string newWorksheetName)
        {
            ExcelWorksheet worksheet = workbook.Worksheets.Copy(existingWorksheetName, newWorksheetName);
            return worksheet;
        }

        [HttpPost]
        public async Task<JsonResult> FileExecute(IFormCollection collect, IFormFile inputb9)
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

                                    if (ws.Cells[rowIndex, col].Text != null && ws.Cells[rowIndex, col].Text != "")
                                    {
                                        excl.colIndex = col;
                                        excl.name = ws.Cells[rowIndex, col].Text;
                                        result.Add(excl);
                                    }
                                    if (ws.Cells[rowIndex, col].Text == null || ws.Cells[rowIndex, col].Text == "")
                                    {
                                        excl.colIndex = col;
                                        excl.name = "Cột: " + col.ToString();
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
            catch (Exception ex)
            {
                return Json(ex.Data);
            }

        }


        private places GetCorrectName(string wrongName, string fatherID)
        {
            wrongName = wrongName.Trim();
            while (wrongName.Contains("  "))
            {
                wrongName = wrongName.Replace("  ", " ");
            }
            var plc = new places();
            if (wrongName == "")
            {
                return null;
            }

            plc = _context.Places.Where(a => a.NameOutput.ToLower().Contains(NormalizeWord(wrongName)) == true && a.FatherId == fatherID).FirstOrDefault();
            if (plc != null)
            {
                return plc;
            }
            else
            {
                var plcase = _context.PlaceCases.Where(a => a.nameCase == NormalizeWord(wrongName)).FirstOrDefault();
                if (plcase != null)
                {
                    plc = _context.Places.Where(a => a.Code == plcase.placeCode && a.FatherId == fatherID).FirstOrDefault();
                    return plc == null ? null : plc;
                }
            }
            return plc;
        }

        private string ChuanhoaDate(string input)
        {
            string DateChuan = "";
            string[] template = input.Split("/");
            if (template.Count() == 3)
            {
                DateChuan = template[1] + "/" + template[0] + "/" + template[2];
            }
            return DateChuan;
        }

        private string getdatehssk(DateTime date)
        {
            string result = "";
            result += date.Year.ToString("yyyy");
            result += date.Month.ToString("MM");
            result += date.Day.ToString("dd") + " ";
            result +=  date.TimeOfDay.ToString("HH:mm") == "00:00" ? "09:00" : date.TimeOfDay.ToString("HH:mm");


            return result;
        }
        private string NormalizeWord(string input)
        {
            string output = input.Trim();
            output = input.ToLower();
            while (output.Contains("  "))
            {
                output = output.Replace("  ", " ");
            }

            return output;
        }
    }
}
