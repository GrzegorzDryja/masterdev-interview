using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientListManager.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        [NotMapped]
        public SelectList BirthYearSelectList { get; set; }
        public Client()
        {
            BirthYearSelectList = new SelectList(Enumerable.Range(1901, DateTime.Now.Year - 1901).ToList());
        }

    }
/*    public class Page
    {
        public SelectList BirthYearSelectList { get; set; }
        public void OnGet()
        {
            BirthYearSelectList = new SelectList(Enumerable.Range(1901, DateTime.Now.Year - 1901).ToList());
        }
    }*/
}
