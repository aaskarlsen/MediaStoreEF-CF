using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaStoreEF_CF.Models
{
    public class Movie
    {
        // PK
        public int Id { get; set; }

        // Fields
        [Required]
        [MaxLength(150)]
        public string MovieTitle { get; set; }
        [MaxLength(150)]
        public string Genre { get; set; }  
        public int ReleaseYear { get; set; }
        [MaxLength(150)]
        public string Director { get; set; }       
        [MaxLength(300)]
        public string LinkToMoviePicture { get; set; }
        [MaxLength(300)]
        public string Trailer { get; set; }

        // Foreign key property for the one-to-many-relation with Franchise. Movie is the "many" side. 
        public int? FranchiseId { get; set; }

        // Navigation property - one-to-many-releationship. Movie is the "many" side
        public Franchise Franchise { get; set; }

        // Navigation property - many-to-many relationship with Character
        public ICollection<Character> Characters { get; set; } = new List<Character>();
    }
}
