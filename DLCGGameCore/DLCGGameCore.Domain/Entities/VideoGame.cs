using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCGGameCore.Domain.Entities
{
    public class VideoGame
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        public int ReleaseYear { get; set; }
        
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public double Rating { get; set; }
    }
}
