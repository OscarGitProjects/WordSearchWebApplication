using Microsoft.EntityFrameworkCore;

namespace WordSearchWebApplication.Models
{
    public class WordSearchContext : DbContext
    {
        public WordSearchContext(DbContextOptions<WordSearchContext> options)
            : base(options)
        {
        }

        public DbSet<WordSearchResult> WordSearchResults { get; set; }
    }
}
