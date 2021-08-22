using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chuanhoafile.Data;
using Chuanhoafile.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using OfficeOpenXml;

namespace Chuanhoafile.Controllers
{
    public class PlaceController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _env;

        public PlaceController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
             IWebHostEnvironment env
            )
        {
            _env = env;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET: Place
        [Authorize]
        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {

            var locationList = await _context.Places.ToListAsync();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DataTablePartial", locationList);
            }

            return View(locationList);
        }

        [Authorize]
        public async Task<IActionResult> Create(Guid? id)
        {
            var place = new places();
            if (id.HasValue)
            {
                place = await _context.Places.FindAsync(id);
                return PartialView("_OrderPartial", place);
            }
            return PartialView("_OrderPartial", place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? id, [Bind("NameOutput,Code,FatherId,Id,DateUpdate,UpdateBy")] places places)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    _context.Update(places);
                }
                else
                {
                    places.Id = Guid.NewGuid();
                    _context.Add(places);
                }
                
                await _context.SaveChangesAsync();
                return PartialView("_OrderPartial", places);
            }
            return PartialView("_OrderPartial", places);
        }

        [Authorize]
        public IActionResult  CreateByFile(Guid? id)
        {
            return PartialView("_FileOrderPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByFile(IFormFile inputb8)
        {
            if (ModelState.IsValid)
            {
                if (inputb8 != null && inputb8.Length > 0)
                {
                    using (var stream = inputb8.OpenReadStream())
                    {
                        using (ExcelPackage excelPack = new ExcelPackage())
                        {
                            excelPack.Load(stream);
                            var ws = excelPack.Workbook.Worksheets[0];
                            var start = ws.Dimension.Start;
                            var end = ws.Dimension.End;
                            for (int rowInd = start.Row + 1; rowInd <= end.Row; rowInd++)
                            {
                                var thanhpho = new places();
                                if(ws.Cells[rowInd, 2].Value != null)
                                {
                                    if (!ckeckplace(ws.Cells[rowInd, 2].Value.ToString()))
                                    {
                                        thanhpho.Id = Guid.NewGuid();
                                        thanhpho.Code = ws.Cells[rowInd, 2].Value.ToString();
                                        thanhpho.NameOutput = ws.Cells[rowInd, 1].Value.ToString();
                                        thanhpho.FatherId = "";
                                        await _context.Places.AddAsync(thanhpho);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                               
                                var quanhuyen = new places();
                                if (ws.Cells[rowInd, 4].Value != null)
                                {
                                    if (!ckeckplace(ws.Cells[rowInd, 4].Value.ToString()))
                                    {
                                        quanhuyen.Id = Guid.NewGuid();
                                        quanhuyen.Code = ws.Cells[rowInd, 4].Value.ToString();
                                        quanhuyen.NameOutput = ws.Cells[rowInd, 3].Value.ToString();
                                        quanhuyen.FatherId = ws.Cells[rowInd, 2].Value.ToString();
                                        await _context.Places.AddAsync(quanhuyen);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                             
                                var phuongxa = new places();
                                if(ws.Cells[rowInd, 6].Value != null)
                                {
                                    if (!ckeckplace(ws.Cells[rowInd, 6].Value.ToString()))
                                    {
                                        phuongxa.Id = Guid.NewGuid();
                                        phuongxa.Code = ws.Cells[rowInd, 6].Value.ToString();
                                        phuongxa.NameOutput = ws.Cells[rowInd, 5].Value.ToString();
                                        phuongxa.FatherId = ws.Cells[rowInd, 4].Value.ToString();
                                        await _context.Places.AddAsync(phuongxa);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                               
                            }
                           
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return PartialView("_FileOrderPartial");
            }
            return PartialView("_FileOrderPartial");
        }

        protected bool ckeckplace(string code)
        {
            if (code == null)
            {
                return true;
            }
            var place = _context.Places.Where(a => a.Code == code).FirstOrDefault();
            if(place == null)
            {
                return false;
            }
            return true;
        }

        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var places = await _context.Places.FirstOrDefaultAsync(m => m.Id == id);
            if (places == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", model: places);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var places = await _context.Places.FindAsync(id);

            if (places == null)
            {
                return NotFound();
            }

            try
            {
                foreach (var item in _context.Places)
                {
                    if (item.FatherId == places.Code)
                    {
                        _context.Places.Remove(item);
                    }
                }
                _context.Places.Remove(places);

                _context.SaveChanges();

                TempData["Notifications"] = "Xóa thành công";
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", model: places);
        }

        private bool placesExists(Guid id)
        {
            return _context.Places.Any(e => e.Id == id);
        }
    }
}
