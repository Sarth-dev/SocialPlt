using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(user.UserID))
            {
                return null;
            }
            else
            {
                throw;
            }
        }

        return user;
    }

    public async Task<User> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return null;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.UserID == id);
    }
    public class UserEngagementDto
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public int EngagementScore { get; set; }
}



public async Task<List<UserEngagementDto>> GetUserEngagementAsync()
{
    var users = await _context.Users.ToListAsync();

    var posts = await _context.Posts
        .GroupBy(p => p.UserID)
        .Select(g => new { UserID = g.Key, Count = g.Count() })
        .ToListAsync();

    var likes = await _context.Likes
        .GroupBy(l => l.UserID)
        .Select(g => new { UserID = g.Key, Count = g.Count() })
        .ToListAsync();

    var comments = await _context.Comments
        .GroupBy(c => c.UserID)
        .Select(g => new { UserID = g.Key, Count = g.Count() })
        .ToListAsync();

    return users.Select(u =>
    {
        var p = posts.FirstOrDefault(x => x.UserID == u.UserID)?.Count ?? 0;
        var l = likes.FirstOrDefault(x => x.UserID == u.UserID)?.Count ?? 0;
        var c = comments.FirstOrDefault(x => x.UserID == u.UserID)?.Count ?? 0;

        return new UserEngagementDto
        {
            UserID = u.UserID,
            UserName = u.UserName,
            EngagementScore = (p * 5) + (l * 2) + (c * 3)
        };
    })
    .OrderByDescending(x => x.EngagementScore)
    .ToList();
}

}