using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;   
using Lab3.DataAccess;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class UsersController : Controller
    {
        private readonly LibraryDbContext _context;

        public UsersController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(String username, String email, DateTime? bornAfter, String? userType)
        {
            String filters = "";

            if (!String.IsNullOrEmpty(username))
            {
                filters = "AND username ILIKE '%" + username + "%' ";
            }
            if (!String.IsNullOrEmpty(email))
            {
                filters += "AND emailAccount ILIKE '%" + email + "%' ";
            }
            if (bornAfter != null)
            {
                filters += "AND dateOfBirth >= '" + ((DateTime)bornAfter).ToString("yyyy-MM-dd") + "' ";
            }
            if (!Models.User.Administrator)
            {
                return RedirectToAction("Index", "Home");
            }

            List<User> users = new List<User>();

            if (String.IsNullOrEmpty(userType) || userType == "Student")
            {
                String query = "SELECT * FROM Users " +
                    "NATURAL JOIN Attends " +
                    "NATURAL JOIN Programmes " +
                    "WHERE true " + filters;

                var students = await _context.students.FromSqlRaw(query).ToListAsync();
                users.AddRange(students);
            }
            if (String.IsNullOrEmpty(userType) || userType == "Administrator")
            {
                String query = "SELECT * FROM Users " +
                    "NATURAL JOIN BelongsToDepartment " +
                    "NATURAL JOIN Departments " +
                    "WHERE true " + filters;

                var admins = await _context.administrators.FromSqlRaw(query).ToListAsync();
                users.AddRange(admins);
            }


            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!Models.User.Administrator)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            String query = "SELECT * FROM Borrows " +
                   "NATURAL JOIN Users " +
                   "WHERE userID = " + id;

            ViewData["borrows"] = _context.borrows.FromSqlRaw(query).ToList();

            query = "SELECT * FROM Fines " +
                    "NATURAL JOIN Borrows " +
                    "WHERE userID = " + id;

            ViewData["fines"] = _context.fines.FromSqlRaw(query).ToList();

            query = "SELECT * FROM Users " +
                "NATURAL JOIN BelongsToDepartment " +
                "NATURAL JOIN Departments " +
                "WHERE userID = " + id;

            var admin = await _context.administrators.FromSqlRaw(query).FirstOrDefaultAsync();


            if(admin == null)
            {
                query = "SELECT * FROM Users " +
                "NATURAL JOIN Attends " +
                "NATURAL JOIN Programmes " +
                "WHERE userID = " + id;
                var student = await _context.students.FromSqlRaw(query).FirstOrDefaultAsync();

                if(student == null)
                {
                    return NotFound();
                }

                return View(student);
            }

            return View(admin);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {

            String deptQuery = "SELECT * FROM Departments";
            String progQuery = "SELECT * FROM Programmes";

            ViewData["programmes"] = await _context.programmes.FromSqlRaw(progQuery).ToListAsync();
            ViewData["departments"] = await _context.departments.FromSqlRaw(deptQuery).ToListAsync();

            if (!Models.User.Administrator)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("userid,username,emailaccount,phonenumber,dateofbirth,address")] User user, String userType, int? department, int? programme)
        {
            if (!Models.User.Administrator)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {

                String query = "INSERT INTO Users " +
                    "(username, emailAccount, phoneNumber, dateOfBirth, address) " +
                    "VALUES " +
                    "( '" + user.username + "', '" + user.emailaccount + "', '" + user.phonenumber + "', '" + user.dateofbirth + "', '" + user.address + "')";


                _context.Database.ExecuteSqlRaw(query);

                String idQuery = "SELECT userID, username, emailAccount, phoneNumber, dateOfBirth, address, '' AS departmentName From Users " +
                   "ORDER BY userID DESC " +
                   "LIMIT 1";

                Administrator newUser = _context.administrators.FromSqlRaw(idQuery).FirstOrDefault();

                int id = newUser.userid;


                String query2 = "";

                if (userType == "Administrator")
                {
                    query2 = "INSERT INTO BelongsToDepartment " +
                        "(userID, departmentID) " +
                        "VALUES " +
                        "( " + id + ", '" + department + "')";

                }
                else if (userType == "Student")
                {
                    query2 = "INSERT INTO Attends " +
                        "(userID, programmeID) " +
                        "VALUES " +
                        "( " + id + ", '" + programme + "')";
                }
                _context.Database.ExecuteSqlRaw(query2);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!Models.User.Administrator)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }


            String query = "SELECT * FROM Users " +
                "NATURAL JOIN BelongsToDepartment " +
                "NATURAL JOIN Departments " +
                "WHERE userID = " + id;

            var admin = await _context.administrators.FromSqlRaw(query).FirstOrDefaultAsync();

            String deptQuery = "SELECT * FROM Departments";
            String progQuery = "SELECT * FROM Programmes";

            ViewData["programmes"] = await _context.programmes.FromSqlRaw(progQuery).ToListAsync();
            ViewData["departments"] = await _context.departments.FromSqlRaw(deptQuery).ToListAsync();

            if (admin == null)
            {
                query = "SELECT * FROM Users " +
                "NATURAL JOIN Attends " +
                "NATURAL JOIN Programmes " +
                "WHERE userID = " + id;
                var student = await _context.students.FromSqlRaw(query).FirstOrDefaultAsync();

                if (student == null)
                {
                    return NotFound();
                }

                ViewData["userType"] = "Student";

                return View(student);
            }
            ViewData["userType"] = "Administrator";
            return View(admin);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userid,username,emailaccount,phonenumber,dateofbirth,address")] User user, String userType, String department, String programme)
        {
            if (!Models.User.Administrator)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id != user.userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    String query = "UPDATE Users " +
                        "SET " +
                        "username = '" + user.username + "', " +
                        "emailaccount = '" + user.emailaccount + "', " +
                        "phonenumber = '" + user.phonenumber + "', " +
                        "dateofbirth = '" + user.dateofbirth + "', " +
                        "address = '" + user.address + "' " +
                        "WHERE userid = " + user.userid;
                    await _context.Database.ExecuteSqlRawAsync(query);

                    if (userType == "Administrator")
                    {
                        query = "UPDATE BelongsToDepartment " +
                            "SET " +
                            "departmentID = " + department + " " +
                            "WHERE userID = " + user.userid;
                        await _context.Database.ExecuteSqlRawAsync(query);
                    }
                    else if (userType == "Student")
                    {
                        query = "UPDATE Attends " +
                            "SET " +
                            "programmeID = " + programme + " " +
                            "WHERE userID = " + user.userid;
                        await _context.Database.ExecuteSqlRawAsync(query);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.userid))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!Models.User.Administrator)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            String query = "SELECT * FROM Users " +
                "NATURAL JOIN BelongsToDepartment " +
                "NATURAL JOIN Departments " +
                "WHERE userID = " + id;

            var admin = await _context.administrators.FromSqlRaw(query).FirstOrDefaultAsync();


            if (admin == null)
            {
                query = "SELECT * FROM Users " +
                "NATURAL JOIN Attends " +
                "NATURAL JOIN Programmes " +
                "WHERE userID = " + id;
                var student = await _context.students.FromSqlRaw(query).FirstOrDefaultAsync();

                if (student == null)
                {
                    return NotFound();
                }

                return View(student);
            }
            return View(admin);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!Models.User.Administrator)
            {
                return RedirectToAction("Index", "Home");
            }

            String query = "DELETE FROM Users " +
                "WHERE userID = " + id;
            var user = await _context.Database.ExecuteSqlRawAsync(query);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.administrators.Any(e => e.userid == id);
        }
        public IActionResult ReturnBook(int borrowID)
        {
            String query = "UPDATE Borrows " +
                "SET returnDate = CURRENT_DATE " +
                "WHERE borrowid = " + borrowID;

            _context.Database.ExecuteSqlRaw(query);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PayFine(int borrowID)
        {

            String query = "UPDATE FINES " +
                "SET " +
                "paid = true " +
                "WHERE borrowid = " + borrowID;

            _context.Database.ExecuteSqlRaw(query);

            return RedirectToAction(nameof(Index));
        }
    }

   
}
