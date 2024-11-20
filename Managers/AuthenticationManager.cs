using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class AuthenticationManager
{
    private const string PROFILES_FILE = "player_profiles.json";
    private List<PlayerProfile> _playerProfiles;

    public AuthenticationManager()
    {
        LoadProfiles();
    }

    private void LoadProfiles()
    {
        if (!File.Exists(PROFILES_FILE))
        {
            _playerProfiles = new List<PlayerProfile>();
            return;
        }

        var jsonText = File.ReadAllText(PROFILES_FILE);
        _playerProfiles = JsonSerializer.Deserialize<List<PlayerProfile>>(jsonText)
            ?? new List<PlayerProfile>();
    }

    private void SaveProfiles()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonText = JsonSerializer.Serialize(_playerProfiles, options);
        File.WriteAllText(PROFILES_FILE, jsonText);
    }

    public PlayerProfile Register(string username, string password)
    {
        // Check if username already exists
        if (_playerProfiles.Any(p => p.Username == username))
        {
            Console.WriteLine("Username already exists!");
            return null;
        }

        // Create new player profile
        var newProfile = new PlayerProfile
        {
            Username = username,
            PasswordHash = HashPassword(password),
            GameState = new User(),
            DaysElapsed = 0,
            CurrentLevel = 1
        };

        _playerProfiles.Add(newProfile);
        SaveProfiles();
        return newProfile;
    }

    public PlayerProfile Login(string username, string password)
    {
        var profile = _playerProfiles
            .FirstOrDefault(p => p.Username == username);

        if (profile == null)
        {
            Console.WriteLine("User not found!");
            return null;
        }

        if (VerifyPassword(password, profile.PasswordHash))
        {
            return profile;
        }

        Console.WriteLine("Incorrect password!");
        return null;
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private bool VerifyPassword(string inputPassword, string storedHash)
    {
        return HashPassword(inputPassword) == storedHash;
    }
}