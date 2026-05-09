// Models for the database context

namespace CoblentzContext.Models
{
    // Model for the database context
    public class SongCookies    
    {
        // Response cookies
        private IResponseCookies? responseCookies;
        // Request cookies
        private IRequestCookieCollection? requestCookies;
        
        // Constructor for response cookies
        public SongCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }

        // Constructor for request cookies
        public SongCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        // --- Song Interaction Tracker (User-Specific) ---
        public void SetLastInteractedSong(string title, string userId)
        {
            // Store as "userId|songTitle" to prevent cross-user notifications
            responseCookies?.Append("lastSong", $"{userId}|{title}", new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            });
        }

        // Get last interacted song 
        public string GetLastInteractedSong(string currentUserId)
        {
            // Get the cookie value
            var cookieValue = requestCookies?["lastSong"] ?? "";
            // Check if the cookie value is null or empty
            if (string.IsNullOrEmpty(cookieValue) || !cookieValue.Contains('|'))
                return "";

            // Split the cookie value into parts
            var parts = cookieValue.Split('|');
            if (parts.Length == 2 && parts[0] == currentUserId)
            {
                return parts[1]; // Return song title only if it belongs to this user
            }
            
            return "";
        }

        // --- Theme Preference ---
        public void SetTheme(string theme)
        {
            // Set the theme cookie
            responseCookies?.Append("theme", theme, new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1)
            });
        }

        // Get the theme cookie
        public string GetTheme() => requestCookies?["theme"] ?? "light";
    }
}
