using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class EntriesController : Controller
    {
        private readonly projectContext _context;

        public EntriesController(projectContext context)
        {
            _context = context;
        }

        // GET: Entries
        public async Task<IActionResult> Index(string searchString, string channelString, string directoryString)
        {
            if (_context.Entry == null)
            {
                return Problem("Entity set 'projectContext.Entry'  is null.");
            }

            var entries = from m in _context.Entry
                         select m;

            if (!String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(channelString) && String.IsNullOrEmpty(directoryString))
            {
                entries = entries.Where(s => s.Name!.Contains(searchString)); //Name
            }

            if (!String.IsNullOrEmpty(channelString) && String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(directoryString))
            {
                entries = entries.Where(s => s.Channel!.Contains(channelString)); //Channel
            }

            if (!String.IsNullOrEmpty(channelString) && !String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(directoryString))
            {
                entries = entries.Where(s => s.Channel!.Contains(channelString) && s.Name!.Contains(searchString)); //Name and Channel
            }

            if (String.IsNullOrEmpty(channelString) && String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(directoryString))
            {
                entries = entries.Where(s => s.Directory!.Contains(directoryString)); //Directory
            }

            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(directoryString) && String.IsNullOrEmpty(channelString))
            {
                entries = entries.Where(s => s.Name!.Contains(searchString) && s.Directory!.Contains(directoryString)); //Directory and Name
            }

            if (!String.IsNullOrEmpty(channelString) && !String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(directoryString))
            {
                entries = entries.Where(s => s.Directory!.Contains(directoryString) && s.Channel!.Contains(channelString) && s.Name!.Contains(searchString)); // Directory Channel and Name
            }

            if (!String.IsNullOrEmpty(channelString) && String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(directoryString))
            {
                entries = entries.Where(s => s.Directory!.Contains(directoryString) && s.Channel!.Contains(channelString)); //Directory and Channel
            }

            return View(await entries.ToListAsync());
        }

        // GET: Entries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entry == null)
            {
                return NotFound();
            }

            var entry = await _context.Entry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // GET: Entries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Channel,Directory")] Entry entry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entry);
        }

        // GET: Entries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entry == null)
            {
                return NotFound();
            }

            var entry = await _context.Entry.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return View(entry);
        }

        // POST: Entries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Channel,Directory")] Entry entry)
        {
            if (id != entry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntryExists(entry.Id))
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
            return View(entry);
        }

        // GET: Entries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entry == null)
            {
                return NotFound();
            }

            var entry = await _context.Entry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: Entries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entry == null)
            {
                return Problem("Entity set 'projectContext.Entry'  is null.");
            }
            var entry = await _context.Entry.FindAsync(id);
            if (entry != null)
            {
                _context.Entry.Remove(entry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntryExists(int id)
        {
          return (_context.Entry?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
