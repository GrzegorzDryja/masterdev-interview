using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientListManager.Services
{
    public interface IFileImportService
    {
        Task UploadClientsFromFile(IFormFile file);
    }
}
