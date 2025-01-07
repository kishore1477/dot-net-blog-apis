using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

            public DbSet<Author> Authors { get; set; } = null!;

}