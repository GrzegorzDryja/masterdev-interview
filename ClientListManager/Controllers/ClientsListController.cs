﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClientListManager.Data;
using ClientListManager.Models;
using CsvHelper;
using Newtonsoft.Json;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using ClientListManager.Services;

namespace ClientListManager.Controllers
{
    public class ClientsListController : Controller
    {
        private readonly ClientListManagerContext _context; //Context wg dobry praktyk powinień być w osobnym serwisie

        private readonly IFileImportService _fileImportService;

        public ClientsListController(ClientListManagerContext context, IFileImportService fileImportService)
        {
            _context = context;
            _fileImportService = fileImportService;
        }

        // GET: ClientsList
        public async Task<IActionResult> Index()
        {
            return View(await _context.Client.ToListAsync());
        }

        // GET: ClientsList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: ClientsList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientsList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,BirthYear")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: ClientsList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: ClientsList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,BirthYear")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: ClientsList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: ClientsList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file) // Działa, ale jest brzydkie
        {
            //string filePath = $"{hostEnvironment.ContentRootPath}\\wwwroot\\files\\{file.FileName}"; //IformFile do stream przesłać do metody GetClientList
            using (FileStream fileStream = System.IO.File.Create(file.ToString()))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            await _fileImportService.UploadClientsFromFile(file);

            return View(await _context.Client.ToListAsync());
        }
    }
}
