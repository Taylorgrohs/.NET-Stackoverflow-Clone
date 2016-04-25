using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using StackClone.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StackClone.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db
        )
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            return View(_db.Questions.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Create()
        {
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
                var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
                question.User = currentUser;
                _db.Questions.Add(question);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }
    }
}
