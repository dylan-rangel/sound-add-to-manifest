using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Entry
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Channel { get; set; }
        [Required]
        public string? Directory { get; set; } //iterate through array, and compile each entry into a single string seperated with breaks
    }
}
