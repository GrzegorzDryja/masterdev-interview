using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClientListManager.Data;
using ClientListManager.Models;

namespace ClientListManager.Views.Test
{
    public class IndexModel : PageModel
    {
        private readonly ClientListManager.Data.ClientListManagerContext _context;

        public IndexModel(ClientListManager.Data.ClientListManagerContext context)
        {
            _context = context;
        }

        public IList<Client> Client { get;set; }

        public async Task OnGetAsync()
        {
            Client = await _context.Client.ToListAsync();
        }
    }
}
