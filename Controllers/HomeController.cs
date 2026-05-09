// HomeController.cs
// Controller for home page

using Microsoft.AspNetCore.Mvc;
using CoblentzContext.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoblentzContext.Controllers
{
    // Controller for home page
    [Authorize]
    public class HomeController : Controller
    {
        // Database context
        private CoblentzContext.Models.CoblentzContext context { get; set; }
        // User manager
        private UserManager<User> userManager;

        // Constructor
        public HomeController(CoblentzContext.Models.CoblentzContext ctx, UserManager<User> userMgr)
        {
            // Initialize database context
            context = ctx;
            // Initialize user manager
            userManager = userMgr;
        }
        
        // GET: /Home/Index
        public async Task<IActionResult> Index()
        {
            // Include User and Project - Using AsNoTracking for speed on read-only pages
            var songs = await context.Song  
                .AsNoTracking()
                .Include(s => s.User)
                .Include(s => s.Project)
                .OrderBy(s => s.Title)
                .ToListAsync();

            // --- SESSION LOGIC: Set session start time if not exists ---
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionStartTime")))
            {
                // Set session start time
                HttpContext.Session.SetString("SessionStartTime", DateTime.Now.ToString("T"));
            }
            // Get session start time
            ViewBag.SessionTime = HttpContext.Session.GetString("SessionStartTime");

            // Get current user's favorites to highlight them
            var userId = userManager.GetUserId(User) ?? "";

            // Get favorite song IDs for current user
            var favoriteIds = await context.Favorites
                .AsNoTracking()
                .Where(f => f.UserId == userId)
                .Select(f => f.SongId)
                .ToListAsync();
            
            // Set favorite song IDs for current user
            ViewBag.FavoriteSongIds = favoriteIds;

            // Get last action from cookie for banner (Filtered by current user)
            SongCookies cookies = new SongCookies(Request.Cookies);
            ViewBag.LastSong = cookies.GetLastInteractedSong(userId);

            // Return view with songs
            return View(songs);
        }

        // POST: /Home/ToggleTheme
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ToggleTheme()
        {
            // Create cookie object
            SongCookies cookies = new SongCookies(Request.Cookies);
            // Get current theme
            string currentTheme = cookies.GetTheme();
            // Set new theme
            string newTheme = currentTheme == "dark" ? "light" : "dark";

            // Create cookie object
            SongCookies responseCookies = new SongCookies(Response.Cookies);
            // Set new theme
            responseCookies.SetTheme(newTheme);

            // Redirect back to wherever the user came from
            string returnUrl = Request.Headers["Referer"].ToString();
            return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
        }

        // Action to add a song to the PERSISTENT list of favorites in the database
        public async Task<IActionResult> Add(int id)
        {
            // Get user ID
            string userId = userManager.GetUserId(User) ?? "";

            // Check if it's already a favorite
            var existingFavorite = await context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.SongId == id);

            // If not already a favorite
            if (existingFavorite == null)
            {
                // Create new favorite
                var favorite = new Favorite
                {
                    UserId = userId,
                    SongId = id
                };
                // Add favorite to database
                context.Favorites.Add(favorite);
                // Save changes
                await context.SaveChangesAsync();

                // Get song title for feedback
                var song = await context.Song.FindAsync(id);

                // If song is found
                if (song != null)
                {
                    // Set feedback message
                    TempData["message"] = $"{song.Title} added to favorites (not persistent)";
                
                    // COOKIE LOGIC: Track the last interaction (User-Specific)
                    SongCookies cookies = new SongCookies(Response.Cookies);
                    cookies.SetLastInteractedSong(song.Title, userId);
                }
            }

            // Redirect back to wherever the user came from
            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
        }

        // Action to remove a song from the database favorites
        public async Task<IActionResult> Remove(int id)
        {
            // Get user ID
            string userId = userManager.GetUserId(User) ?? "";
            // Get favorite song
            var favorite = await context.Favorites
                .Include(f => f.Song)
                .FirstOrDefaultAsync(f => f.UserId == userId && f.SongId == id);

            // If favorite is found
            if (favorite != null)
            {
                // Get song title
                string songTitle = favorite.Song?.Title ?? "Song";
                // Remove favorite
                context.Favorites.Remove(favorite);
                // Save changes
                await context.SaveChangesAsync();
                // Set feedback message
                TempData["message"] = $"{songTitle} removed from favorites.";
            }

            // Redirect back to wherever the user came from 
            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
        }

        // Action to display favorites for the CURRENT user only
        public async Task<IActionResult> Favorites()
        {
            // Get user ID
            string userId = userManager.GetUserId(User) ?? "";

            // Get songs favorited by this specific user
            var favoriteSongs = await context.Favorites
                // Using AsNoTracking for speed on read-only pages
                .AsNoTracking() 
                // Filter by user ID
                .Where(f => f.UserId == userId)
                // Include song and user
                .Include(f => f.Song)
                    .ThenInclude(s => s.User)
                // Include song and project
                .Include(f => f.Song)
                    .ThenInclude(s => s.Project)
                // Select only the song
                .Select(f => f.Song)
                // Convert to list
                .ToListAsync();

            // Return view with favorite songs
            return View(favoriteSongs);
        }

        // Action to display the privacy policy
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
