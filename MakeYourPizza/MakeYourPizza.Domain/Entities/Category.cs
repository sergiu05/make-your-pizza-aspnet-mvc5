using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourPizza.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Ingredient> Ingredients { get; set; }
    }
}
