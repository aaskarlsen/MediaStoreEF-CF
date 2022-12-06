using System.ComponentModel.DataAnnotations;

namespace MediaStoreEF_CF.Models.DTOs.Franchise
{
    public class FranchiseCreateDTO
    {
        // Fields
        [Required]
        [MaxLength(150)]
        public string FranchiseTitle { get; set; }
        [MaxLength(600)]
        public string Description { get; set; }

    }
}
