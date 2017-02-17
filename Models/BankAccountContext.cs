using Microsoft.EntityFrameworkCore;
namespace BankAccount.Models
{
    public class BankAccountContext : DbContext
    {
        public BankAccountContext(DbContextOptions<BankAccountContext> options) : base(options)
        { }
 
        public DbSet<User> Users { get; set; }
    }
}