using Microsoft.EntityFrameworkCore;

namespace PostDocuments.Models
{
    public class DocumentsDbContext:DbContext
    {
        public DocumentsDbContext(DbContextOptions<DocumentsDbContext> options) : base(options)
        {}
            
        public DbSet<Documents> Documentos { get; set; }
    }
}
