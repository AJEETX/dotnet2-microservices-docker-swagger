using Authorize.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthorizeService.Persistence
{
    public class ApplicationDbContext:DbContext  
    {  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)  
        {  
    
        }  
        public DbSet<Customer> Customers { get; set; }  
    } 
}