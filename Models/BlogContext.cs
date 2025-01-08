using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

            public DbSet<Author> Authors { get; set; } = null!;
            public DbSet<Category> Categories { get; set; } = null!;
             public DbSet<BlogContent> BlogContents { get; set; } = null!;

}