using System;
using System.Collections.Generic;
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
    }
}
