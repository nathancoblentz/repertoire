// Models for the database context

using System.ComponentModel.DataAnnotations;

namespace CoblentzContext.Models
{
    // Model for the database context
    public class Favorite
    {
        // The foreign key for the User
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        // The foreign key for the Song
        public int SongId { get; set; }
        public Song? Song { get; set; }
    }
}
