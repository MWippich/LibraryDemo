using Lab3.DataAccess;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(String title, String author, DateTime? date)
        {
            String filters = "";

            if (!String.IsNullOrEmpty(title))
            {
                filters = "AND title ILIKE '%" + title + "%' ";
            }
            if (!String.IsNullOrEmpty(author))
            {
                filters += "AND authorName ILIKE '%" + author + "%' ";
            }
            if (date != null)
            {
                filters += "AND publishingDate >= '" + ((DateTime)date).ToString("yyyy-MM-dd") + "' ";
            }

            String query = "SELECT Books.bookID, prequelID, BookInfo.bookInfoID, 0 AS publisherid, publishingID, title, series, language, isbn, edition, publishingDate, pageCount, publisherName, '' AS genres, STRING_AGG(authorName, ', ') AS authors, 0 AS numCopies, 0 AS unavailbleCopies FROM Books " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "NATURAL JOIN Publishers " +
                "LEFT JOIN Authorings ON Books.bookID = Authorings.bookID " +
                "LEFT JOIN Authors ON Authorings.authorID = Authors.authorID " +
                "LEFT JOIN LanguageOf ON BookInfo.bookInfoID = LanguageOf.bookInfoID " +
                "LEFT JOIN Languages ON LanguageOf.languageID = Languages.languageID " +
                "WHERE true " + filters + " " +
                "GROUP BY Books.bookID, prequelID, BookInfo.bookInfoID, publishingID, title, series, language, isbn, edition, publishingDate, pageCount, publisherName";

            var books = _context.books.FromSqlRaw(query);

            return View(await books.ToListAsync());
        }

        public IActionResult EmptyFilters()
        {
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            String authorsQuery = "SELECT string_agg(authorName, ' ,') FROM Authorings " +
                    "NATURAL JOIN BookInfo " +
                    "NATURAL JOIN Publishings " +
                    "LEFT JOIN Authors ON Authorings.authorID = Authors.authorID " +
                    "WHERE publishingID = " + id + " " +
                    "GROUP BY bookid";

            String availbleCopiesQuery = "SELECT count(itemID) AS stock FROM Items " +
                "NATURAL JOIN Books " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "NATURAL JOIN Borrows " +
                "WHERE publishingID = " + id + " AND returnDate IS NULL";

            String genreQuery = "SELECT STRING_AGG(genre, ', ') FROM Books " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "LEFT JOIN GenreOf ON Books.bookID = GenreOf.bookID " +
                "LEFT JOIN Genres ON Genres.genreID = GenreOf.genreID " +
                "WHERE publishingID = " + id + " " +
                "GROUP BY Books.bookID";

            String query = "SELECT Books.bookID, prequelID, BookInfo.bookInfoID, 0 AS publisherid, publishingID, title, series, language, isbn, edition, publishingDate, pageCount, publisherName," +
                " (" + genreQuery + ") AS genres, (" + authorsQuery + ") AS authors, count(itemID) AS numCopies, (" + availbleCopiesQuery + ") AS unavailbleCopies FROM Books " +
                "NATURAL JOIN Items " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "NATURAL JOIN Publishers " +
                "LEFT JOIN LanguageOf ON BookInfo.bookInfoID = LanguageOf.bookInfoID " +
                "LEFT JOIN Languages ON LanguageOf.languageID = Languages.languageID " +
                "WHERE publishingID = " + id + " " +
                "GROUP BY Books.bookID, prequelID, BookInfo.bookInfoID, publishingID, title, series, language, isbn, edition, publishingDate, pageCount, publisherName";

            var book = await _context.books.FromSqlRaw(query).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("bookid,prequelid,series,title,isbn,edition,publishingdate,pagecount,language,publishername,genres,authors")] Book book, List<Author> Authors, int numCopies)
        {

            if (ModelState.IsValid)
            {
                String input;
                if (book.prequelid == null)
                {
                    input = "null";
                }
                else
                {
                    input = "" + book.prequelid;
                }

                String query = "INSERT INTO Books " +
                    "(prequelid, series) " +
                    "VALUES " +
                    "(" + input + ", '" + book.series + "')";

                _context.Database.ExecuteSqlRaw(query);

                String idQuery = "SELECT bookID, prequelID, 0 AS bookInfoID, 0 AS publisherid, 0 AS publishingID, '' AS title, series, '' AS language, '' AS isbn, '' AS edition, " +
                    "null AS publishingDate, 0 AS pageCount, '' AS publisherName, '' AS genres, '' AS authors, 0 AS numCopies, 0 AS unavailbleCopies FROM Books " +
                    "ORDER BY bookID DESC " +
                    "LIMIT 1";

                var newBook = _context.books.FromSqlRaw(idQuery).FirstOrDefault();


                var bookID = newBook.bookid;

                String bookInfoQuery = "INSERT INTO BookInfo " +
                    "(bookID, title) " +
                    "VALUES " +
                    "(" + bookID + ", '" + book.title + "')";

                _context.Database.ExecuteSqlRaw(bookInfoQuery);

                var newBook2 = _context.books.FromSqlRaw("SELECT 0 AS bookID, 0 AS prequelID, 0 AS publisherid, bookInfoID, 0 AS publishingID, title, '' AS series, '' AS language, '' AS isbn," +
                    " '' AS edition, null AS publishingDate, 0 AS pageCount, '' AS publisherName, '' AS genres, '' AS authors, 0 AS numCopies, 0 AS unavailbleCopies FROM BookInfo " +
                    "ORDER BY bookInfoID DESC " +
                    "LIMIT 1").FirstOrDefault();

                int bookInfoId = newBook2.bookinfoid;

                _context.Database.ExecuteSqlRaw("INSERT INTO Publishers " +
                    "(publisherName) " +
                    "VALUES" +
                    "( '" + book.publishername + "' )");

                var newPublisher = _context.publishers.FromSqlRaw("SELECT * FROM Publishers " +
                    "ORDER BY publisherID DESC " +
                    "LIMIT 1").FirstOrDefault();

                int publisherID = newPublisher.publisherid;

                String pubDate;
                if (book.publishingdate == null)
                {
                    pubDate = "null";
                }
                else
                {
                    pubDate = "'" + book.publishingdate + "'";
                }

                String pubQuery = "INSERT INTO Publishings " +
                    "(publisherID, bookInfoID, ISBN, edition, publishingDate, pageCount) " +
                    "VALUES " +
                    "(" + publisherID + ", " + bookInfoId + ", '" + book.isbn + "', '" + book.edition + "', " + pubDate + ", " + book.pagecount + ")";

                _context.Database.ExecuteSqlRaw(pubQuery);

                String genQuery = "INSERT INTO Genres " +
                    "(genre) " +
                    "VALUES " +
                    "('" + book.genres + "')";

                _context.Database.ExecuteSqlRaw(genQuery);

                var newGenre = _context.genres.FromSqlRaw("SELECT * FROM Genres " +
                    "ORDER BY genreID DESC " +
                    "LIMIT 1").FirstOrDefault();

                int genreID = newGenre.genreid;

                String genOfQuery = "INSERT INTO GenreOf " +
                    "(bookID, genreID) " +
                    "VALUES " +
                    "( " + bookID + ", " + genreID + ")";

                _context.Database.ExecuteSqlRaw(genOfQuery);

                foreach (Author author in Authors)
                {
                    String authQuery = "INSERT INTO Authors " +
                    "(authorName) " +
                    "VALUES " +
                    "( '" + author.authorname + "')";

                    _context.Database.ExecuteSqlRaw(authQuery);

                    var newAuthor = _context.authors.FromSqlRaw("SELECT * FROM Authors " +
                    "ORDER BY authorID DESC " +
                    "LIMIT 1").FirstOrDefault();

                    int authorID = newAuthor.authorid;

                    String authQuery2 = "INSERT INTO Authorings " +
                    "(bookID, authorID) " +
                    "VALUES " +
                    "( " + bookID + ", " + authorID + ")";

                    _context.Database.ExecuteSqlRaw(authQuery2);
                }

                var publishing = _context.publishings.FromSqlRaw("SELECT publishingID FROM Publishings " +
                    "ORDER BY publishingID DESC " +
                    "LIMIT 1").FirstOrDefault();

                int publishingId = publishing.publishingid;

                int ks = publishingId;
                Console.WriteLine(publishing.publishingid);
                Console.WriteLine(ks);
                Console.WriteLine(publishingId);

                for (int i = 0; i < numCopies; i++)
                {
                    String itemQuery = "INSERT INTO Items " +
                        "(publishingID) " +
                        "VALUES " +
                        "(" + publishingId + ")";

                    _context.Database.ExecuteSqlRaw(itemQuery);
                }

                
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult _AddAuthorRow(int id)
        {
            ViewData["ID"] = id;
            return PartialView("_AuthorRow", new Author());
        }

        public IActionResult _RemoveAuthorRow(int id, List<Author> Authors)
        {
            return PartialView("_AuthorRow", new Author());
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            String availbleCopiesQuery = "SELECT count(itemID) AS stock FROM Items " +
                "NATURAL JOIN Books " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "NATURAL JOIN Borrows " +
                "WHERE publishingID = " + id + " AND returnDate IS NULL";

            String genreQuery = "SELECT STRING_AGG(genre, ', ') FROM Books " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "LEFT JOIN GenreOf ON Books.bookID = GenreOf.bookID " +
                "LEFT JOIN Genres ON Genres.genreID = GenreOf.genreID " +
                "WHERE publishingID = " + id + " " +
                "GROUP BY Books.bookID";

            String query = "SELECT Books.bookID, prequelID, 0 AS publisherid, BookInfo.bookInfoID, publishingID, title, series, '' AS language, isbn, edition, publishingDate, pageCount, publisherName," +
                " (" + genreQuery + ") AS genres, '' AS authors, count(itemID) AS numCopies, (" + availbleCopiesQuery + ") AS unavailbleCopies FROM Books " +
                "NATURAL JOIN Items " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "NATURAL JOIN Publishers " +
                "LEFT JOIN Authorings ON Books.bookID = Authorings.bookID " +
                "LEFT JOIN Authors ON Authors.authorID = Authorings.authorID " +
                "WHERE publishingID = " + id + " " +
                "GROUP BY Books.bookID, prequelID, BookInfo.bookInfoID, publishingID, title, series, language, isbn, edition, publishingDate, pageCount, publisherName";


            var book = await _context.books.FromSqlRaw(query).FirstOrDefaultAsync();

            try
            {
                ViewData["Authors"] = await _context.authors.FromSqlRaw("SELECT authors.authorID, authorName FROM Books " +
                    "NATURAL JOIN BookInfo " +
                    "NATURAL JOIN Publishings " +
                    "LEFT JOIN Authorings ON Books.bookID = Authorings.bookID " +
                    "LEFT JOIN Authors ON Authorings.authorID = Authors.authorID " +
                    "WHERE publishingID = " + id).ToListAsync();
            }
            catch
            {
                ViewData["authors"] = new List<Author>();
            }
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("bookid,bookinfoid,publishingid,publisherid,prequelid,series,title,isbn,edition,publishingdate,pagecount,language,publishername,authors")] Book book)
        {
            if (id != book.bookid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                String inp;
                if(book.prequelid == null)
                {
                    inp = "null";
                }
                else
                {
                    inp = "" + book.prequelid;
                }
                
                String query = "UPDATE Books " +
                    "SET " +
                    "prequelID = " + inp + ", " +
                    "series = '" + book.series + "' " +
                    "WHERE bookID = " + book.bookid;

                _context.Database.ExecuteSqlRaw(query);

                query = "UPDATE BookInfo " +
                    "SET " +
                    "title = '" + book.title + "' " +
                    "WHERE bookInfoID = " + book.bookinfoid;

                _context.Database.ExecuteSqlRaw(query);

                query = "UPDATE Publishings " +
                    "SET " +
                    "ISBN = '" + book.isbn + "', " +
                    "edition = '" + book.edition + "', " +
                    "publishingDate = '" + book.publishingdate + "', " +
                    "pageCount = " + book.pagecount + " " +
                    "WHERE publishingID = " + book.publishingid;

                _context.Database.ExecuteSqlRaw(query);

                query = "UPDATE Publishers " +
                    "SET " +
                    "publisherName = '" + book.publishername + "' " +
                    "WHERE publisherID = " + book.publisherid;

                _context.Database.ExecuteSqlRaw(query);

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            String authorsQuery = "SELECT string_agg(authorName, ' ,') FROM Authorings " +
                    "NATURAL JOIN BookInfo " +
                    "NATURAL JOIN Publishings " +
                    "LEFT JOIN Authors ON Authorings.authorID = Authors.authorID " +
                    "WHERE publishingID = " + id + " " +
                    "GROUP BY bookid";

            String availbleCopiesQuery = "SELECT count(itemID) AS stock FROM Items " +
                "NATURAL JOIN Books " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "NATURAL JOIN Borrows " +
                "WHERE publishingID = " + id + " AND returnDate IS NULL";

            String genreQuery = "SELECT STRING_AGG(genre, ', ') FROM Books " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "LEFT JOIN GenreOf ON Books.bookID = GenreOf.bookID " +
                "LEFT JOIN Genres ON Genres.genreID = GenreOf.genreID " +
                "WHERE publishingID = " + id + " " +
                "GROUP BY Books.bookID";

            String query = "SELECT Books.bookID, prequelID, BookInfo.bookInfoID, 0 AS publisherid, publishingID, title, series, language, isbn, edition, publishingDate, pageCount, publisherName," +
                " (" + genreQuery + ") AS genres, (" + authorsQuery + ") AS authors, count(itemID) AS numCopies, (" + availbleCopiesQuery + ") AS unavailbleCopies FROM Books " +
                "NATURAL JOIN Items " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "NATURAL JOIN Publishers " +
                "LEFT JOIN LanguageOf ON BookInfo.bookInfoID = LanguageOf.bookInfoID " +
                "LEFT JOIN Languages ON LanguageOf.languageID = Languages.languageID " +
                "WHERE publishingID = " + id + " " +
                "GROUP BY Books.bookID, prequelID, BookInfo.bookInfoID, publishingID, title, series, language, isbn, edition, publishingDate, pageCount, publisherName";

            var book = await _context.books.FromSqlRaw(query).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            String idQuery = "SELECT bookID, prequelID, 0 AS bookInfoID, 0 AS publisherid, publishingID, title, series, '' AS language, isbn, edition, publishingDate, pageCount, '' AS publisherName," +
                " ''AS genres, '' AS authors, 0 AS numCopies, 0 AS unavailbleCopies FROM Books " +
                "NATURAL JOIN BookInfo " +
                "NATURAL JOIN Publishings " +
                "WHERE publishingID = " + id;

            var book = _context.books.FromSqlRaw(idQuery).FirstOrDefault();

            int bookid = book.bookid;

            String query = "DELETE FROM Books " +
                "WHERE bookID = " + bookid;
            await _context.Database.ExecuteSqlRawAsync(query);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(int publishingID, int userID)
        {


            string userBorrows = "SELECT count(itemID) FROM Borrows " +
                "WHERE userID = " + userID + " AND returndate IS NULL " +
                "GROUP BY userID " +
                "HAVING count(itemID) >= 3";

            int res = _context.Database.ExecuteSqlRaw(userBorrows);
            if(res > 0)
            {
                RedirectToAction(nameof(Index));
            }

            string findItem = "SELECT Items.itemid, publishingid FROM Items " +
                "WHERE publishingid = " + publishingID + " AND items.itemid NOT IN(SELECT itemid FROM  Borrows " +
                    "WHERE returndate IS NULL AND borrowingDate IS NOT NULL)";

            var item = _context.items.FromSqlRaw(findItem).FirstOrDefault();
            if  (item != null)
            {
                String query = "INSERT INTO Borrows " +
                    "(itemid, userID, borrowingdate, expirydate) " +
                    "VALUES " +
                    "(" + item.itemid + ", " + userID + ", CURRENT_DATE, CURRENT_DATE + 7)";

                _context.Database.ExecuteSqlRaw(query);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.books.Any(e => e.bookid == id);
        }

        
    }
}
