using Microsoft.EntityFrameworkCore;
using Social_Media.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Social_Media.ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=SQLLiteDatabase.db"));

builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LikeService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<FeedService>();
builder.Services.AddScoped<ContentModerationService>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
