// Controller for project catalogs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoblentzContext.Models;

// Controller for project catalogs
namespace CoblentzContext.Controllers
{
    // Controller for project catalogs
    public class ProjectController : Controller
    {
        // Database context
        private CoblentzContext.Models.CoblentzContext _context;

        // Constructor
        public ProjectController(CoblentzContext.Models.CoblentzContext context)
        {
            _context = context;
        }

        // --- View Project Catalog ---
        public async Task<IActionResult> Details(int id)
        {
            // Get project by ID
            var project = await _context.Projects
                // Using AsNoTracking for speed on read-only pages
                .AsNoTracking()
                // Include song and user
                .Include(p => p.Songs)
                    .ThenInclude(s => s.User)
                // Get project by ID
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            // If project not found, return 404
            if (project == null)
            {
                // Return 404
                return NotFound();
            }

            // Get current user's favorites to highlight them in the setlist
            var userId = _context.Users.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault();
            
            // Get song IDs favorited by this user
            var favoriteIds = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.SongId)
                .ToListAsync();
            
            // Pass favorite IDs to the view
            ViewBag.FavoriteSongIds = favoriteIds;

            // Sort songs by title for now
            project.Songs = project.Songs.OrderBy(s => s.Title).ToList();

            return View(project);
        }

        // --- List all Projects ---
        public async Task<IActionResult> Index()
        {
            // Get all projects
            var projects = await _context.Projects
                // Using AsNoTracking for speed on read-only pages
                .AsNoTracking()
                // Include song and user
                .Include(p => p.Songs)
                .OrderBy(p => p.Name)
                .ToListAsync();

            // Return projects
            return View(projects);
        }
    }
}
