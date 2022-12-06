using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaStoreEF_CF.Models
{
    public class Franchise
    {
        // PK
        public int Id { get; set; }

        // Fields
        [Required]
        [MaxLength(150)]
        public string FranchiseTitle { get; set; }
        [MaxLength(600)]
        public string Description { get; set; }

        // Property navigation - one-to-many-releationship with Movie. Franchise is the "one" side 
        public ICollection<Movie> Movies { get; set; }
    }
}
