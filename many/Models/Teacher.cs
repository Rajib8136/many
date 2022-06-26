using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace many.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Faculty { get; set; }
    }
    public enum Faculty
    {
        CSE, BBA, EEE, ENG
    }
}
