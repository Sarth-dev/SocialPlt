using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Dtos;
using Social_Media.Dtos;
using Social_Media.Models;


namespace Social_Media.Services;

public class FeedService
{
    private readonly ApplicationDbContext _context;

    public FeedService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<FeedPostDto>> GetFeedAsync()
    {
        var users = await _context.Users.ToListAsync();
        var posts = await _context.Posts.ToListAsync();
        var likes = await _context.Likes.ToListAsync();
        var comments = await _context.Comments.ToListAsync();

        var feed = posts.Select(post =>
        {
            var author = users.First(u => u.UserID == post.UserID);

            return new FeedPostDto
            {
                PostID = post.PostID,
                PostContent = post.Content,
                UserName = author.UserName,

                LikesBy = likes
                    .Where(l => l.PostID == post.PostID)
                    .Select(l => users.First(u => u.UserID == l.UserID).UserName)
                    .ToList(),

                Comments = comments
                    .Where(c => c.PostID == post.PostID)
                    .Select(c => new CommentDto
                    {
                        UserName = users.First(u => u.UserID == c.UserID).UserName,
                        Content = c.Content
                    })
                    .ToList()
            };
        })
        .OrderByDescending(p => p.PostID)
        .ToList();

        return feed;
    }
}
