using BlogApi.DTOs;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogContentController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogContentController(BlogContext context)
        {
            _context = context;
        }

        // GET: api/BlogContent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogContentDTO>>> GetBlogContents()
        {
            return await _context.BlogContents
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Select(b => new BlogContentDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    CategoryId = b.CategoryId,
                    // CategoryName = b.Category.Name,
                    AuthorId = b.AuthorId,
                    // AuthorName = $"{b.Author.FirstName} {b.Author.LastName}"
                })
                .ToListAsync();
        }

        // GET: api/BlogContent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogContentDTO>> GetBlogContent(int id)
        {
            var blogContent = await _context.BlogContents
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Select(b => new BlogContentDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    CategoryId = b.CategoryId,
                    // CategoryName = b.Category.Name,
                    AuthorId = b.AuthorId,
                    // AuthorName = $"{b.Author.FirstName} {b.Author.LastName}"
                })
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blogContent == null)
            {
                return NotFound();
            }

            return blogContent;
        }

        // POST: api/BlogContent
        [HttpPost]
        public async Task<ActionResult<BlogContent>> CreateBlogContent(BlogContent blogContent)
        {
            // Validate Category and Author
            if (!CategoryExists(blogContent.CategoryId) || !AuthorExists(blogContent.AuthorId))
            {
                return BadRequest("Invalid Category or Author ID");
            }

            _context.BlogContents.Add(blogContent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlogContent), new { id = blogContent.Id }, blogContent);
        }

        // PUT: api/BlogContent/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogContent(int id, BlogContent blogContent)
        {
            if (id != blogContent.Id)
            {
                return BadRequest();
            }

            // Validate Category and Author
            if (!CategoryExists(blogContent.CategoryId) || !AuthorExists(blogContent.AuthorId))
            {
                return BadRequest("Invalid Category or Author ID");
            }

            _context.Entry(blogContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogContentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BlogContent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogContent(int id)
        {
            var blogContent = await _context.BlogContents.FindAsync(id);
            if (blogContent == null)
            {
                return NotFound();
            }

            _context.BlogContents.Remove(blogContent);
            await _context.SaveChangesAsync();

            return Ok(blogContent);
        }

        private bool BlogContentExists(int id) => _context.BlogContents.Any(e => e.Id == id);
        private bool CategoryExists(int id) => _context.Categories.Any(c => c.Id == id);
        private bool AuthorExists(int id) => _context.Authors.Any(a => a.Id == id);
    }
}
