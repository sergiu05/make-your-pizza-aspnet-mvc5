using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourPizza.Domain.Entities
{
    public class Ingredient : BaseEntity, ProductInterface
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName="Money")]
        public decimal Price { get; set; }
        public string Imagename { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
