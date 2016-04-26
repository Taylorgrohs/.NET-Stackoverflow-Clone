using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc.Rendering;
using StackClone.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StackClone.Controllers
{
 public class RolesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RolesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        
        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();
            return View(roles);
        }

        // GET: /Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string RoleName)
        {
            _db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
            {
                Name = RoleName
            });
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Roles/Delete
        public IActionResult Delete( string RoleName)
        {
            var thisRole = _db.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            _db.Roles.Remove(thisRole);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Roles/Edit
        public IActionResult Edit(string roleName)
        {
            var thisRole = _db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(thisRole);
        }

        // POST: /Roles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IdentityRole role)
        {   
            _db.Entry(role).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
