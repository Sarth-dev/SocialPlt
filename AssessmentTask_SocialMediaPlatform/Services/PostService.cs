using Social_Media.Models;
using Microsoft.EntityFrameworkCore;

namespace Social_Media.Services;

public class PostService
{
    private readonly ApplicationDbContext _context;
    private readonly ContentModerationService _moderation;

   public PostService(
    ApplicationDbContext context,
    ContentModerationService moderation)
{
    _context = context;
    _moderation = moderation;
}

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task<Post> CreatePostAsync(Post post)
{
    _moderation.Validate(post.Content);

    _context.Posts.Add(post);
    await _context.SaveChangesAsync();
    return post;
}

    public async Task<Post> UpdatePostAsync(Post post)
    {
        _context.Entry(post).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PostExists(post.PostID))
            {
                return null;
            }
            else
            {
                throw;
            }
        }

        return post;
    }

    public async Task<Post> DeletePostAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return null;
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return post;
    }
    

    private bool PostExists(int id)
    {
        return _context.Posts.Any(e => e.PostID == id);
    }
}