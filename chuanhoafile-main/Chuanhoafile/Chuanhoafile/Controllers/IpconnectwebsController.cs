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
    public class IpconnectwebsController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _env;

        public IpconnectwebsController(
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

        [Authorize]
        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {

            var locationList = await _context.Ipconnectwebs.AsNoTracking().Take(100).ToListAsync();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DataTablePartial", locationList);
            }

            return View(locationList);
        }
      
        private bool IpconnectwebExists(Guid id)
        {
            return _context.Ipconnectwebs.Any(e => e.Id == id);
        }
    }
}
