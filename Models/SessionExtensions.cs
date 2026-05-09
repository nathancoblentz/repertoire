// Models for the database context

using Microsoft.AspNetCore.Session;
using System.Text.Json;

namespace CoblentzContext.Models
{
    // Extension method to set an object in the session
    public static class SessionExtensions 
    {
        // This method allows you to store any serializable object in the session by converting it to a JSON string.
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            // Serialize the object to a JSON string and store it in the session using the provided key.
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // This method allows you to retrieve an object from the session by converting it from a JSON string.
        public static T? GetObject<T>(this ISession session, string key)
        {
            // Retrieve the JSON string from the session using the provided key.
            string json = session.GetString(key); 

            // Return the object from the session.
            return (string.IsNullOrEmpty(json)) ? default(T) :
                JsonSerializer.Deserialize<T>(json);
        }
    }
}
