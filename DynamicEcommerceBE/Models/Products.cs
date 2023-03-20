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
		public string? Field1 { get; set; }
		public string? Field2 { get; set; }
		public string? Field3 { get; set; }
		public int? Field4 { get; set; }
		public int? Field5 { get; set; }
	
		//FOREIGN KEY PRODUCTCATEGORIESID
		public ProductCategories ProductCategories { get; set; }

		//RELAZIONE CON ORDERDETAILS (manytomany con ORDERS)

		public List<OrderDetails> OrderDetails { get; set; }

	


	}
}
