

using DynamicEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicEcommerce.Repository
{
    public class DynamicEcommerceDb:DbContext
    {
        public DynamicEcommerceDb(DbContextOptions<DynamicEcommerceDb> options) : base(options) { }

        private string _connectionString;
        public DynamicEcommerceDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //RELAZIONE 1 A 1 USERS - USERROLE
            modelBuilder.Entity<Users>().HasOne(ur => ur.UserRole).WithOne(u => u.Users).HasForeignKey<UserRole>(ur => ur.UserID);
            //"Users" ha una relazione "HasOne" con la classe "UserRole" tramite la proprietà "UserRole".
            //Inoltre, stiamo specificando che la classe "UserRole" ha una relazione "WithOne"
            //con la classe "Users" tramite la proprietà "Users".
            //Infine, stiamo dichiarando che la chiave esterna nella classe "UserRole" si riferisce
            //alla chiave primaria nella classe "Users" utilizzando la proprietà "UserID" come chiave esterna.

            //RELAZIONE 1 A M USERROLE - ROLE 
            modelBuilder.Entity<Roles>().HasMany(ur => ur.UserRole).WithOne(r => r.Role).HasForeignKey(ur => ur.RoleID);

            //RELAZIONE 1 A M USERS - ORDERS
            modelBuilder.Entity<Users>().HasMany(u => u.Orders).WithOne(o => o.Users).HasForeignKey(o => o.UserID);

            //RELAZIONE 1 A M ORDERS - ORDERDETAILS (m a m con products)
            modelBuilder.Entity<Orders>().HasMany(or => or.OrderDetails).WithOne(o => o.Orders).HasForeignKey(or => or.OrderID);

            //RELAZIONE 1 A M ORDERDETAILS - PRODUCTS (M A M CON ORDERS)
            modelBuilder.Entity<Products>().HasMany(or => or.OrderDetails).WithOne(p => p.Products).HasForeignKey(or => or.ProductID);

            //RELAZIONE 1 A M PRODUCTS - PRODUCTSCATEGORIES
            modelBuilder.Entity<ProductCategories>().HasMany(p => p.Products).WithOne(pc => pc.ProductCategories).HasForeignKey(p => p.ProductCategoriesID);

        }

        public DbSet<Users>? Users { get; set; }
         public DbSet<Roles>? Roles { get; set; }
         public DbSet<UserRole>? UserRole { get; set; }
         public DbSet<Products>? Products { get; set; }
         public DbSet<ProductCategories>? ProductCategories { get; set; }
         public DbSet<Orders>? Orders { get; set; }
         public DbSet<OrderDetails>? OrderDetails { get; set; }

    }
    
}