using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCGGameCore.Application.DTOs
{
    public class VideoGameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;        
        public int ReleaseYear { get; set; }     
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
    }
}
