using System.ComponentModel.DataAnnotations;

namespace MediaStoreEF_CF.Models.DTOs.Franchise
{
    public class FranchiseReadDTO
    {
        // PK
        public int Id { get; set; }

        // Fields
        [Required]
        [MaxLength(150)]
        public string FranchiseTitle { get; set; }
        [MaxLength(600)]
        public string Description { get; set; }

        //One-to-many between Franchise and Movie
        public int[] Movies { get; set; }   

    }
}
