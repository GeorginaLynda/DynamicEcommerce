using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEcommerce.Models
{
    public class Products
    {
		[Key]
		public int ProductID { get; set; }
		public int ProductCategoriesID { get; set; }
		public decimal? UnitPrice { get; set; }
		public string? Image { get; set; } 
		public string? Field1 { get; set; }
		public string? Field2 { get; set; }
		public int? Field3 { get; set; }
		public int? Field4 { get; set; }
        public Products()
        {
            ProductCategoriesID = 2; // impostare il valore predefinito di default
        }

        //FOREIGN KEY PRODUCTCATEGORIESID
        public ProductCategories ProductCategories { get; set; }

		//RELAZIONE CON ORDERDETAILS (manytomany con ORDERS)

		public List<OrderDetails> OrderDetails { get; set; }

	


	}
}
