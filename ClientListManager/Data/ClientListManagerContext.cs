using Microsoft.EntityFrameworkCore;

namespace ClientListManager.Data
{
    public class ClientListManagerContext : DbContext
    {
        public ClientListManagerContext (DbContextOptions<ClientListManagerContext> options)
            : base(options)
        {
        }

        public DbSet<ClientListManager.Models.Client> Client { get; set; }
    }
}
