// Models for the database context

using System.ComponentModel.DataAnnotations;

namespace CoblentzContext.Models //namespace
{
    public class Song //class
    {
        [Required(ErrorMessage ="Song must have an ID.")] //validation
        public int SongId { get; set; }

        // Song title   
        [Required(ErrorMessage ="Song must have a Title.")]
        public string Title { get; set; } = string.Empty;

        // Song artist
        public string Artist { get; set; } = string.Empty;
        
        // Project ID
        [Required(ErrorMessage ="Song must belong to a Project")]
        public int ProjectId { get; set; } // Foreign Key

        // Project navigation property
        public Project? Project { get; set; } // Navigation Property

        // Song status
        [Required(ErrorMessage ="Song must have a status")] //validation 
        public string Status { get; set; } = string.Empty;

        // YouTube URL
        public string? YouTubeUrl { get; set; }

        // Song chart
        public string? SongChart { get; set; }

        // User ID
        public string? UserId { get; set; } = string.Empty;

        // User navigation property
        public User? User { get; set; }
    }
}
