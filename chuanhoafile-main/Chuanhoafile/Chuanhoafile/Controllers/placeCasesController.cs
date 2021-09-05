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
    public class placeCasesController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _env;

        public placeCasesController(
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

            var locationList = await _context.PlaceCases.AsNoTracking().Take(100).ToListAsync();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DataTablePartial", locationList);
            }

            return View(locationList);
        }

        [Authorize]
        public async Task<IActionResult> Create(Guid? id)
        {
            var place = new placeCase();
            if (id.HasValue)
            {
                place = await _context.PlaceCases.FindAsync(id);
                return PartialView("_OrderPartial", place);
            }
            return PartialView("_OrderPartial", place);
        }
        // GET: placeCases/Details/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? id, [Bind("nameCase,placeCode,placeFatherCode,Id,DateUpdate,UpdateBy")] placeCase placeCase)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    _context.Update(placeCase);
                }
                else
                {
                    placeCase.Id = Guid.NewGuid();
                    _context.Add(placeCase);
                }

                await _context.SaveChangesAsync();
                return PartialView("_OrderPartial", placeCase);
            }
            return PartialView("_OrderPartial", placeCase);
        }

        // GET: placeCases/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeCase = await _context.PlaceCases.FindAsync(id);
            if (placeCase == null)
            {
                return NotFound();
            }
            return View(placeCase);
        }

        // POST: placeCases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("nameCase,placeCode,Id,DateUpdate,UpdateBy")] placeCase placeCase)
        {
            if (id != placeCase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placeCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!placeCaseExists(placeCase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(placeCase);
        }

        // GET: placeCases/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeCase = await _context.PlaceCases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeCase == null)
            {
                return NotFound();
            }

            return View(placeCase);
        }

        // POST: placeCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var placeCase = await _context.PlaceCases.FindAsync(id);
            _context.PlaceCases.Remove(placeCase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public IActionResult CreateByFile(Guid? id)
        {
            return PartialView("_FileOrderPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByFile(IFormFile inputb7)
        {
            if (ModelState.IsValid)
            {
                if (inputb7 != null && inputb7.Length > 0)
                {
                    using (var stream = inputb7.OpenReadStream())
                    {
                        using (ExcelPackage excelPack = new ExcelPackage())
                        {
                            excelPack.Load(stream);
                            var ws = excelPack.Workbook.Worksheets[0];
                            var start = ws.Dimension.Start;
                            var end = ws.Dimension.End;
                            for (int rowInd = start.Row; rowInd <= end.Row; rowInd++)
                            {
                                if (ws.Cells[rowInd, 1].Value != null && ws.Cells[rowInd, 2].Value != null)
                                {
                                    if (! await placeCaseExistsNameAsync(ws.Cells[rowInd, 1].Value.ToString(),""))
                                    {
                                        var thanhpho = new placeCase();
                                        thanhpho.Id = Guid.NewGuid();
                                        thanhpho.nameCase = ws.Cells[rowInd, 1].Value.ToString();
                                        thanhpho.placeCode = ws.Cells[rowInd, 2].Value.ToString();
                                        thanhpho.placeFatherCode ="";
                                        await _context.PlaceCases.AddAsync(thanhpho);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                if (ws.Cells[rowInd, 3].Value != null && ws.Cells[rowInd, 4].Value != null && ws.Cells[rowInd, 2].Value != null)
                                {
                                    if (!await placeCaseExistsNameAsync(ws.Cells[rowInd, 3].Value.ToString(), ws.Cells[rowInd, 2].Value.ToString()))
                                    {
                                        var quanhuyen = new placeCase();
                                        quanhuyen.Id = Guid.NewGuid();
                                        quanhuyen.nameCase = ws.Cells[rowInd, 3].Value.ToString();
                                        quanhuyen.placeCode = ws.Cells[rowInd, 4].Value.ToString();
                                        quanhuyen.placeFatherCode = ws.Cells[rowInd, 2].Value.ToString();
                                        await _context.PlaceCases.AddAsync(quanhuyen);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                if (ws.Cells[rowInd, 4].Value != null && ws.Cells[rowInd, 5].Value != null && ws.Cells[rowInd, 6].Value != null)
                                {
                                    if (!await placeCaseExistsNameAsync(ws.Cells[rowInd, 5].Value.ToString(), ws.Cells[rowInd, 4].Value.ToString()))
                                    {
                                        var xaphuong = new placeCase();
                                        xaphuong.Id = Guid.NewGuid();
                                        xaphuong.nameCase = ws.Cells[rowInd, 5].Value.ToString();
                                        xaphuong.placeCode = ws.Cells[rowInd, 6].Value.ToString();
                                        xaphuong.placeFatherCode = ws.Cells[rowInd, 4].Value.ToString();
                                        await _context.PlaceCases.AddAsync(xaphuong);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                            }

                        }
                    }
                }
                return PartialView("_FileOrderPartial");
            }
            return PartialView("_FileOrderPartial");
        }

        private async Task<bool> placeCaseExistsNameAsync(string  name, string code)
        {
            return await _context.PlaceCases.AnyAsync(e => e.nameCase == name && e.placeFatherCode == code);
        }

        private bool placeCaseExists(Guid id)
        {
            return _context.PlaceCases.Any(e => e.Id == id);
        }
    }
}
