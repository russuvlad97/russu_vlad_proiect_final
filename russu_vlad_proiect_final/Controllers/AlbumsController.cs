using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using russu_vlad_proiect_final.Data;
using russu_vlad_proiect_final.Models;

namespace russu_vlad_proiect_final.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly RecordStoreContext _context;

        public AlbumsController(RecordStoreContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var albums = from b in _context.Albums select b;

            if(!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    albums = albums.OrderByDescending(b => b.Title);
                    break;
                case "Price":
                    albums = albums.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    albums = albums.OrderByDescending(b => b.Price);
                    break;
                default:
                    albums = albums.OrderBy(b => b.Title);
                    break;
            }

            return View(await PaginatedList<Album>.CreateAsync(albums.AsNoTracking(), pageNumber ?? 1, 3));
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(s => s.Orders)
                .ThenInclude(e => e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Artist,ReleaseDate,Price")] Album album)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(album);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Albums.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Album>(studentToUpdate, "", s => s.Artist, s => s.Title, s => s.Price, s => s.ReleaseDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Albums.Remove(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }


        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.ID == id);
        }
    }
}
