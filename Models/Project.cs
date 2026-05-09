// Models for the database context

using System.ComponentModel.DataAnnotations;

namespace CoblentzContext.Models
{
    // Model for the database context
    public class Project
    {
        // Project ID
        public int ProjectId { get; set; }

        // Project name
        [Required(ErrorMessage = "Project must have a name.")]
        public string Name { get; set; } = string.Empty;

        // Navigation property for Setlists
        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
