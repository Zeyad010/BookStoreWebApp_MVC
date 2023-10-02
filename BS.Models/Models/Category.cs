using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BS.Models.Models
{
    public class Category
    {
        // to set Id as a primary key we use Data Annotations --> [Key]
        // if the prop name is Id it will be the PK by default
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DispalyOrder { get; set; }
    }
}
