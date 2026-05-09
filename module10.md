# CoblentzContext - Module 10 Documentation

This project has been updated to include Session and Cookie management for the "Favorites" feature.

## Requirements Met

### 1. Configuration & Wrappers
*   **Session Support**: Enabled session state in `Program.cs` to allow temporary data storage across requests.
*   **Wrapper Classes**:
    *   **`SongSession.cs`**: Encapsulates `ISession` to handle list serialization (JSON).
    *   **`SongCookies.cs`**: Encapsulates `IResponseCookies` to manage persistent cookie storage for user preferences.

### 2. Controller Implementation
*   **`HomeController.cs`**:
    *   **`Add(int id)`**: Adds a song to the session-based favorites list and updates a persistent cookie.
    *   **`Remove(int id)`**: Removes a song from the session list.
    *   **`Favorites()`**: Displays the current favorites stored in the session.
*   **`SongsController.cs`**: Modified to use `TempData` for user feedback and migrated out of the Admin area.

### 3. User Interface Enhancements
*   **Home Index**: Added icons for adding songs to favorites and a quick-link to view the favorites list.
*   **Favorites Index**: Created a dedicated dashboard to view and manage favorite songs.
*   **Layout Notifications**: Integrated a Bootstrap alert system in `_Layout.cshtml` to display `TempData` success messages.

### 4. Code Practices
*   **Separation of Concerns**: Logic for storage is relegated to wrapper classes, keeping controllers clean.
*   **Persistent vs. Transient Data**: 
    *   **Sessions** are used for the Favorites list (clears on browser close).
    *   **Cookies** are used for the "Last Added Song" (persists for 30 days).

## How to Test
1. Compile and run the application.
2. Click the Heart icon next to any song to add it to your favorites.
3. Observe the green success message.
4. Click "View Favorites" to see your list.
5. Click "Remove" on the favorites page to update your list.
