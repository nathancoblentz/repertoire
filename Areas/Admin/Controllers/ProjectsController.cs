// Admin/Controllers/ProjectsController.cs
// This is the controller for the admin area.

using CoblentzContext.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CoblentzContext.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProjectsController : Controller
    {
        // Database context
        private CoblentzContext.Models.CoblentzContext _context;
    
        // Constructor
        public ProjectsController(CoblentzContext.Models.CoblentzContext context)
        {
            // Initialize database context
            _context = context;
        }

        // GET: /Admin/Projects/Index
        public async Task<IActionResult> Index()
        {
            // Get all projects
            var projects = await _context.Projects // Get all projects
                .OrderBy(p => p.Name) // Order by name
                .ToListAsync(); // Convert to list
            
            // Return the list of projects to the view
            return View(projects);
        }

        // GET: /Admin/Projects/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View("Edit", new Project());
        }

        // GET: /Admin/Projects/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Get the project
            var project = await _context.Projects.FindAsync(id);
            // Return the project to the view
            return View(project);
        }

        // POST: /Admin/Projects/Edit   
        [HttpPost]
        public async Task<IActionResult> Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                // Check if the project is new or existing
                if (project.ProjectId == 0)
                    _context.Projects.Add(project); // Add the project
                else
                    _context.Projects.Update(project); // Update the project

                await _context.SaveChangesAsync(); // Save changes
                TempData["message"] = $"{project.Name} project saved."; // Set message
                return RedirectToAction("Index"); // Redirect to index
            }
            // Return the project to the view
            return View(project);
        }

        // GET: /Admin/Projects/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // Get the project
            var project = await _context.Projects.FindAsync(id);

            // Return the project to the view
            return View(project);
        }

        // POST: /Admin/Projects/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Project project)
        {
            _context.Projects.Remove(project); // Remove the project
            await _context.SaveChangesAsync(); // Save changes

            TempData["message"] = $"{project.Name} project deleted."; // Set message
            return RedirectToAction("Index"); // Redirect to index
        }
    }
}
