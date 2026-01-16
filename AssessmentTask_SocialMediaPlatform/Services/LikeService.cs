using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

public class LikeService
{
    private readonly Social_Media.ApplicationDbContext _context;

    public LikeService(Social_Media.ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task LikeAsync(Like like)
    {
        var exists = await _context.Likes.AnyAsync(l =>
            l.UserID == like.UserID && l.PostID == like.PostID);

        if (exists)
            throw new InvalidOperationException("Post already liked by user.");

        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
    }
}
