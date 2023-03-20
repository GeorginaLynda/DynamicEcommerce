using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEcommerce.Models
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public string Field1 { get; set; }

        //RELAZIONE CON USERROLE
        public List<UserRole> UserRole { get; set; }
    }
}
