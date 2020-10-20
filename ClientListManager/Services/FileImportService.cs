using ClientListManager.Data;
using ClientListManager.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClientListManager.Services
{
    public class FileImportService : IFileImportService
    {
        private readonly ClientListManagerContext _context;
        public FileImportService(ClientListManagerContext context)
        {
            _context = context;
        }

        public async Task UploadClientsFromFile(IFormFile file)
        {
            //var  = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = file.OpenReadStream())
            {
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                var records = csv.GetRecords<Client>();

                //csv.GetRecords<Client>().ToList().Dump();
               //await _context.AddRangeAsync(records);

                foreach (var item in records)
                {
                    _context.Add(item);
                }

                await _context.SaveChangesAsync();
            }

           // System.IO.File.Delete(fileName);

        }
    }
}
