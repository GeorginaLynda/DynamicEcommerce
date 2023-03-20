using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEcommerce.Models
{
	public class UserRole
	{
		[Key]
		public int UserRoleID { get; set; }
		public int RoleID { get; set; }
		public int UserID { get; set; }

		//FOREIGN KEY CON ROLEID
		public Roles Role { get; set; }

		//FOREIGN KEY CON USERID
		public Users Users { get; set; }

	}
}
