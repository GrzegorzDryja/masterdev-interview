using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClientListManager.Models;

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
