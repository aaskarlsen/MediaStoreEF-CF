using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaStoreEF_CF.Models
{
    public class Character
    {
        // PK
        public int Id { get; set; }

        // Fields
        [Required]
        [MaxLength(150)]
        public string CharacterName { get; set; }
        [MaxLength(150)]
        public string Alias { get; set; }
        public char Gender { get; set; }
        [MaxLength(300)] // choose MaxLength instead of url-annotation. Url converts to nvarchar(max) through EF.
        public string LinkToPicture { get; set; }

        // Navigation property - many-to-many relationship with Movie
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
