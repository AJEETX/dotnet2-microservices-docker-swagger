using Microsoft.EntityFrameworkCore;

namespace HelpDeskSupportService.Persistence
{
    public class ApplicationDbContext:DbContext  
    {  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context):base(context)  
        {  
    
        }  
        public DbSet<Ticket> Tickets { get; set; }  
    }  
}