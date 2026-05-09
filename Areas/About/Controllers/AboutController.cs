// Areas/About/Controllers/AboutController.cs


using Microsoft.AspNetCore.Mvc;

namespace CoblentzContext.Areas.About.Controllers
{
    // This controller is part of the "About" area.
    [Area("About")] 
    // Routes for this controller will start with "About".
    [Route("About/{id?}")]
    public class AboutController : Controller
    {
        // REFACTORED: Streamlined routing to avoid "About/About" in URL
        public IActionResult Index(string id)
        {

            // This is a switch statement that will check the value of the id variable.
            
            string page = id?.ToLower() ?? ""; 

            if (page == "general")
            {
            // If the id is "general", it will return the General.cshtml view.
                return View("/Areas/About/Views/General.cshtml");
            }
            else if (page == "documentation")
            {
            // If the id is "documentation", it will return the Documentation.cshtml view.
                return View("/Areas/About/Views/Documentation.cshtml");
            }

            // Default view
            return View("/Areas/About/Views/AboutHome.cshtml");

        }
    }
}
