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
    public class RecommentsController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _env;

        public RecommentsController(
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

            var recomment = await _context.Recomments.ToListAsync();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DataTablePartial", recomment);
            }

            return View(recomment);
        }

        [Authorize]
        public async Task<IActionResult> Create(Guid? id)
        {
            var recomment = new Recomment();
            if (id.HasValue)
            {
                recomment = await _context.Recomments.FindAsync(id);
                return PartialView("_OrderPartial", recomment);
            }
            return PartialView("_OrderPartial", recomment);
        }

        private bool RecommentExists(Guid id)
        {
            return _context.Recomments.Any(e => e.Id == id);
        }
    }
}
