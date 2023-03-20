using System.ComponentModel.DataAnnotations;

namespace DynamicEcommerce.Models
{
	
    public class Users
    {
		[Key]
        public int UserID { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string? Field3 { get; set; }
		public string? Field4 { get; set; }
		public DateTime? Field5 { get; set; }
		public int? Field6 { get; set; }
		public int? Field7 { get; set; }
		public int? Field8 { get; set; }

		//RELAZIONE CON USERROLE
		public UserRole UserRole { get; set; }
		
		//RELAZIONE CON ORDERS
		public List<Orders>? Orders { get; set; }


    }
}