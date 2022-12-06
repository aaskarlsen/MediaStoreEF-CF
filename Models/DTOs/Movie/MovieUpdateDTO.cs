using System.ComponentModel.DataAnnotations;

namespace MediaStoreEF_CF.Models.DTOs.Movie
{
    public class MovieUpdateDTO
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

    }
}
