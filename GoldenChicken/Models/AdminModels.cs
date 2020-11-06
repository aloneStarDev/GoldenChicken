using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GoldenChicken.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
    }
    public enum MediaType
    {
        Photo,Video
    }
    public class Media
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public MediaType type { get; set; }

        public bool Active { get; set; } = true;
        public string Caption { get; set; }
        
        public string Link { get; set; }

    }
}