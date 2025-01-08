namespace BlogApi.DTOs
{
    public class BlogContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        // public string CategoryName { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        // public string AuthorName { get; set; } = string.Empty;
    }
}
