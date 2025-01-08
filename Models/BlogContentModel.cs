namespace BlogApi.Models
{
    public class BlogContent
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        // Foreign key for Category
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // Foreign key for Author
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }
}
