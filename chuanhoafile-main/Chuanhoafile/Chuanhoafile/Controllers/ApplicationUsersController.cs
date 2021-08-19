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

namespace Chuanhoafile.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _env;

        public ApplicationUsersController(
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = await _userManager.FindByIdAsync(userId);
            if (users.IsAdmin)
            {
                ViewBag.checkAdmin = "isadmin";
            }

            var userList = (from user in _context.Users
                            join userrole in _context.UserRoles on user.Id equals userrole.UserId into join1
                            from j1 in join1.DefaultIfEmpty()

                            join role in _context.Roles on j1.RoleId equals role.Id into join2
                            from j2 in join2.DefaultIfEmpty()
                            select new UserViewModel()
                            {
                                user = user,
                                role = j2
                            }).ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DataTablePartial", userList);
            }

            return View(userList);
        }
        [Authorize]
        public async Task<IActionResult> AddRole(Guid? id)
        {


            if (id == null)
            {
                return NotFound();
            }
            ViewBag.UserID = id;
            ViewBag.Roles = await _context.Roles.ToListAsync();
            return PartialView("_AddRolePartial");
        }
        [Authorize]
        [HttpPost, ActionName("AddRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(IFormCollection Info)
        {

            try
            {
                IdentityUserRole<Guid> role = new IdentityUserRole<Guid>();
                role.RoleId = Guid.Parse(Info["Role"]);
                role.UserId = Guid.Parse(Info["Id"]);
                IdentityUserRole<Guid> temp = _context.UserRoles.Where(m => m.UserId == Guid.Parse(Info["Id"])).FirstOrDefault();
                if (temp != null)
                {
                    _context.UserRoles.Remove(temp);
                    await _context.SaveChangesAsync();
                }
                _context.UserRoles.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

       

        [Authorize]
        public async Task<IActionResult> Create(Guid? id)
        {
            var user = new RegisterViewmodel();
            if (id.HasValue)
            {
                var userN = await _context.Users.FindAsync(id);
                var userN2 = new RegisterViewmodel()
                {
                    id = userN.Id,
                    Email = userN.Email,
                    FullName = userN.FullName,
                    PhoneNumber = userN.PhoneNumber,
                    Assignment = userN.Assignment
                };
                return PartialView("_OrderPartial", userN2);
            }
            return PartialView("_OrderPartial", user);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Guid? id, [Bind("Password,ConfirmPassword,FullName,UserName,Email,PhoneNumber")] RegisterViewmodel registerViewmodel, IFormFile image)
        {


            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    var userN = _context.Users.Find(id);
                    userN.UserName = registerViewmodel.Email;
                    userN.NormalizedUserName = registerViewmodel.Email.ToUpper();
                    userN.Email = registerViewmodel.Email;
                    userN.NormalizedEmail = registerViewmodel.Email.ToUpper();
                    userN.FullName = registerViewmodel.FullName;
                    userN.PhoneNumber = registerViewmodel.PhoneNumber;
                    if (image != null)
                    {
                        if (checkPath(Path.GetExtension(image.FileName)))
                        {
                            var streamN = new MemoryStream();
                            image.CopyTo(streamN);
                            userN.Assignment = streamN.ToArray();
                            streamN.Close();
                        }
                    }
                    _context.Users.Update(userN);
                    await _context.SaveChangesAsync();
                }
                else
                if (_userManager.FindByEmailAsync(registerViewmodel.Email).Result == null)
                {
                    if (!checkPath(Path.GetExtension(image.FileName)))
                    {
                        return PartialView("_OrderPartial", registerViewmodel);
                    }
                    var user = new ApplicationUser
                    {
                        Id = Guid.NewGuid(),
                        UserName = registerViewmodel.Email,
                        Email = registerViewmodel.Email,
                        FullName = registerViewmodel.FullName,
                        PhoneNumber = registerViewmodel.PhoneNumber
                    };
                    using (var stream = new MemoryStream())
                    {
                        await image.CopyToAsync(stream);
                        user.Assignment = stream.ToArray();
                        stream.Close();
                    }
                    var result = _userManager.CreateAsync(user, registerViewmodel.Password).Result;

                    if (result.Succeeded)
                    {

                        //_userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
            }

            return PartialView("_OrderPartial", registerViewmodel);
        }
        private bool checkPath(string path)
        {
            if (path.ToUpper().Equals(".jpg".ToUpper())) return true;
            if (path.ToUpper().Equals(".jpeg".ToUpper())) return true;
            if (path.ToUpper().Equals(".png".ToUpper())) return true;
            return false;
        }
        [Authorize]
        public async Task<IActionResult> ResetPassword(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            ResetPassword resetPassword = new ResetPassword();
            resetPassword.Email = user.Email;

            return PartialView("_ResetPassword", model: resetPassword);

        }
        [Authorize]
        [HttpPost, ActionName("ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(Guid? id, [Bind("Email,Password,ConfirmPassword")] ResetPassword resetPassword)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(resetPassword.Email);
                    if (user == null || id != user.Id)
                    {
                        return NotFound();
                    }
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                    var result = await _userManager.ResetPasswordAsync(user, Code, resetPassword.Password);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return PartialView("_ResetPassword", resetPassword);
        }
        // GET: ApplicationUsers/Edit/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", model: user);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {


            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                _context.Users.Remove(user);
                foreach (var temp in _context.UserRoles)
                {
                    if (temp.UserId == id)
                        _context.UserRoles.Remove(temp);
                }
                await _context.SaveChangesAsync();

                TempData["Notifications"] = "Xóa thành công";
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return PartialView("_DeletePartial", user);
        }
        //Tồn tại
        private bool ApplicationUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
