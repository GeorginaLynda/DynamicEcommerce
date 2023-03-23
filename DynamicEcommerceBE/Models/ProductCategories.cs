using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEcommerce.Models
{
    public class ProductCategories
    {
        [Key]   
        public int ProductCategoriesID { get; set; }
        public string Categorie { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
       
        //RELAZIONE CON PRODUCTS
        public List<Products> Products { get; set; }
    }
}
