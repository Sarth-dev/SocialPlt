namespace Social_Media.Dtos;

public class FeedPostDto
{
    public int PostID { get; set; }
    public string PostContent { get; set; }
    public string UserName { get; set; }
    public List<string> LikesBy { get; set; }
    public List<CommentDto> Comments { get; set; }
}
