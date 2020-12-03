using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab3.Models;
using Lab3.DataAccess;

namespace Lab3.Controllers
{
    public class ProfileController : Controller
    {

        private readonly LibraryDbContext _context;

        public ProfileController(LibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var user = Models.User.LoggedInUser;
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            var user = Models.User.LoggedInUser;

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(int? userID, String? password)
        {
            /*
            if(password != "password123")
            {
                return View();
            }
            */

            if (userID == null)
            {
                return View();
            }

            String query = "SELECT * FROM Users " +
                "NATURAL JOIN BelongsToDepartment " +
                "NATURAL JOIN Departments " +
                "WHERE userID = " + userID;

            var admin = await _context.administrators.FromSqlRaw(query).FirstOrDefaultAsync();

            if (admin == null)
            {
                query = "SELECT * FROM Users " +
                "NATURAL JOIN Attends " +
                "NATURAL JOIN Programmes " +
                "WHERE userID = " + userID;
                var student = await _context.students.FromSqlRaw(query).FirstOrDefaultAsync();

                if (student == null)
                {
                    return View();
                }
                Models.User.LoggedInUser = student;
                Models.User.Administrator = false;
            }
            else
            {
                Models.User.LoggedInUser = admin;
                Models.User.Administrator = true;
            }

            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Borrows()
        {
            var user = Models.User.LoggedInUser;
            if (user != null)
            {
                String query = "SELECT * FROM Borrows " +
                    "NATURAL JOIN Users " +
                    "WHERE userID = " + user.userid;

                return View(_context.borrows.FromSqlRaw(query).ToList());
            }
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Fines()
        {
            var user = Models.User.LoggedInUser;
            if (user != null)
            {

                String query = "SELECT * FROM Fines " +
                    "NATURAL JOIN Borrows " +
                    "WHERE userID = " + user.userid;

                var fines = _context.fines.FromSqlRaw(query).ToList();

                return View(fines);
            }
           
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Logout()
        {
            Models.User.LoggedInUser = null;
            Models.User.Administrator = false;
            return View();
        }
        public IActionResult PayFine(int borrowID)
        {

            String query = "UPDATE FINES " +
                "SET " +
                "paid = true " +
                "WHERE borrowid = " + borrowID;

            _context.Database.ExecuteSqlRaw(query);

            return RedirectToAction(nameof(Fines));
        }
    }
}