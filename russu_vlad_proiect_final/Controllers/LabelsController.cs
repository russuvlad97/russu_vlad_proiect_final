using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using russu_vlad_proiect_final.Data;
using russu_vlad_proiect_final.Models;
using russu_vlad_proiect_final.Models.RecordStoreViewModels;

namespace russu_vlad_proiect_final.Controllers
{
    public class LabelsController : Controller
    {
        private readonly RecordStoreContext _context;

        public LabelsController(RecordStoreContext context)
        {
            _context = context;
        }

        // GET: Labels
        public async Task<IActionResult> Index(int? id, int? albumID)
        {
            var viewModel = new LabelIndexData();
            viewModel.Labels = await _context.Labels
                .Include(i => i.ReleasedAlbums)
                .ThenInclude(i => i.Album)
                .ThenInclude(i => i.Orders)
                .ThenInclude(i => i.Customer)
                .AsNoTracking()
                .OrderBy(i => i.LabelName)
                .ToListAsync();

            if (id != null)
            {
                ViewData["LabelID"] = id.Value;
                Label label = viewModel.Labels.Where(i => i.ID == id.Value).Single();
                viewModel.Albums = label.ReleasedAlbums.Select(s => s.Album);
            }

            if (albumID != null)
            {
                ViewData["AlbumID"] = albumID.Value;
                viewModel.Orders = viewModel.Albums.Where(
                x => x.ID == albumID).Single().Orders;
            }

            return View(viewModel);
        }

        // GET: Labels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Labels
                .FirstOrDefaultAsync(m => m.ID == id);
            if (label == null)
            {
                return NotFound();
            }

            return View(label);
        }

        // GET: Labels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Labels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LabelName,Address")] Label label)
        {
            if (ModelState.IsValid)
            {
                _context.Add(label);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(label);
        }

        // GET: Labels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Labels.FindAsync(id);
            if (label == null)
            {
                return NotFound();
            }
            return View(label);
        }

        // POST: Labels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LabelName,Address")] Label label)
        {
            if (id != label.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(label);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabelExists(label.ID))
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
            return View(label);
        }

        // GET: Labels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Labels
                .FirstOrDefaultAsync(m => m.ID == id);
            if (label == null)
            {
                return NotFound();
            }

            return View(label);
        }

        // POST: Labels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var label = await _context.Labels.FindAsync(id);
            _context.Labels.Remove(label);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabelExists(int id)
        {
            return _context.Labels.Any(e => e.ID == id);
        }
    }
}
