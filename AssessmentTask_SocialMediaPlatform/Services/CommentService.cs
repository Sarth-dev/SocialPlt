using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

public class CommentService
{
    private readonly Social_Media.ApplicationDbContext _context;
    private readonly Social_Media.Services.ContentModerationService _moderation;

    public CommentService(Social_Media.ApplicationDbContext context,
                          Social_Media.Services.ContentModerationService moderation)
    {
        _context = context;
        _moderation = moderation;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        _moderation.Validate(comment.Content);

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<List<Comment>> GetByPostAsync(int postId)
    {
        return await _context.Comments
            .Where(c => c.PostID == postId)
            .ToListAsync();
    }

    
}
