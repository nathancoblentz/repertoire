// Admin/Controllers/HomeController.cs
// This is the controller for the admin area.


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CoblentzContext.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CoblentzContext.Areas.Admin.Controllers
{
    // Tag for admin area
    [Area("Admin")] 

    // Only authorized users can access this area
    [Authorize] 
    public class HomeController : Controller
    {
        // Database context
        private CoblentzContext.Models.CoblentzContext _context;

        // User manager
        private UserManager<User> _userManager;

        // Constructor
        public HomeController(CoblentzContext.Models.CoblentzContext context, UserManager<User> userManager)
        {
            // Initialize database context
            _context = context;
            // Initialize user manager
            _userManager = userManager;
        }

        // GET: /Admin/Home/Index
        public async Task<IActionResult> Index()
        {
            // Include Project and User for the admin table

            var songs = await _context.Song // Get all songs
                .AsNoTracking() // Don't track changes
                .Include(s => s.Project) // Include project
                .Include(s => s.User) // Include user
                .OrderBy(s => s.Title) // Order by title
                .ToListAsync(); // Convert to list

            // Get current user's favorites to highlight them
            var userId = _userManager.GetUserId(User) ?? "";

            // Get all favorite song IDs for the current user
            var favoriteIds = await _context.Favorites // Get all favorites
                .AsNoTracking() // Don't track changes
                .Where(f => f.UserId == userId) // Filter by user ID
                .Select(f => f.SongId) // Select song IDs
                .ToListAsync(); // Convert to list
            
            // Pass the list of favorite song IDs to the view
            ViewBag.FavoriteSongIds = favoriteIds;

            // Return the list of songs to the view
            return View(songs);
        }
    }
}
