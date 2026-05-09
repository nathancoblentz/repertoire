using CoblentzContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using AppDbContext = CoblentzContext.Models.CoblentzContext;

namespace CoblentzContext.Areas.Admin
{
    [Area("Admin")]
    [Authorize] // Temporarily allow all logged-in users to manage songs during testing
    public class SongsController : Controller
    {
        private AppDbContext _context { get; set; }
        private UserManager<User> _userManager;

        public SongsController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var songs = await _context.Song
                .AsNoTracking()
                .Include(s => s.User)
                .Include(s => s.Project)
                .OrderBy(s => s.Title)
                .ToListAsync();
            return View(songs);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Projects = await _context.Projects.AsNoTracking().OrderBy(p => p.Name).ToListAsync();
            return View("Edit", new Song());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Projects = await _context.Projects.AsNoTracking().OrderBy(p => p.Name).ToListAsync();
            var song = await _context.Song.FindAsync(id);
            return View(song);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Song song)
        {
            if (ModelState.IsValid)
            {
                bool isNew = song.SongId == 0;
                if (isNew)
                {
                    song.UserId = _userManager.GetUserId(User) ?? "";
                    _context.Song.Add(song);
                }
                else
                {
                    _context.Song.Update(song);
                }
                
                await _context.SaveChangesAsync();
                TempData["message"] = $"{song.Title} was successfully {(isNew ? "added" : "updated")}.";
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            {
                ViewBag.Action = (song.SongId == 0) ? "Add" : "Edit";
                ViewBag.Projects = await _context.Projects.AsNoTracking().OrderBy(p => p.Name).ToListAsync();
                return View(song);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _context.Song.FindAsync(id);
            return View(song);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Song song)
        {
            // Retrieve full song object to get the Title for the toast message
            var songToDelete = await _context.Song.FindAsync(song.SongId);
            if (songToDelete == null) return NotFound();

            // Remove any favorites referencing this song to prevent foreign key conflicts
            var relatedFavorites = await _context.Favorites.Where(f => f.SongId == song.SongId).ToListAsync();
            if (relatedFavorites.Any())
            {
                _context.Favorites.RemoveRange(relatedFavorites);
            }

            _context.Song.Remove(songToDelete);
            await _context.SaveChangesAsync();
            TempData["message"] = $"{songToDelete.Title} was successfully deleted.";
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}
