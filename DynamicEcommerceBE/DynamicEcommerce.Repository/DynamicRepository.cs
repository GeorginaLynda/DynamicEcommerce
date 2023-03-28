using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEcommerce.Repository
{
    public class DynamicRepository : IDynamicEcommerceRepo
    {
        private IConfiguration _configuration;
        private DynamicEcommerceDb _dynamicEcommerceDb;
        public DynamicRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _dynamicEcommerceDb = new DynamicEcommerceDb(_configuration.GetSection("ConnectionStrings:DynamicEcommerce").Value); 
            

        }

        //Implementazione dei CRUD di USERS
        List<Users> IDynamicEcommerceRepo.GetUsers()
        {
            List<Users> users = new List<Users>();

            users = _dynamicEcommerceDb.Users.ToList();

            return users;
        }

        public Users GetUserById(int id)
        {
            Users user = _dynamicEcommerceDb.Users.FirstOrDefault(u => u.UserID == id);
            return user;

        }

        //IMPLEMENTAZIONE METODO LOGIN
        public Users GetUserByUsername(string username)
        {
            Users user = _dynamicEcommerceDb.Users.FirstOrDefault(u => u.Username == username);
            return user;
        }
        public UserRole GetUserRoleByUserId(int userId)
        {
            UserRole userRole = _dynamicEcommerceDb.UserRole.FirstOrDefault(x => x.UserID == userId);
            return userRole;

        }

        bool IDynamicEcommerceRepo.AddUser(Users user)
        {
            bool result = false;

            _dynamicEcommerceDb.Users.Add(user);
            result = _dynamicEcommerceDb.SaveChanges() > 0;

            return result;
        }

         public async Task<bool> PutUser(Users user)
        {
            try
            {
                _dynamicEcommerceDb.Entry(user).State = EntityState.Modified;
                await _dynamicEcommerceDb.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(PutUser)}: {ex.Message}");
                return false;
            }
        }

        bool IDynamicEcommerceRepo.DeleteUser(int Id)
        {
            bool result = false;
            Users user = _dynamicEcommerceDb.Users.FirstOrDefault(x => x.UserID == Id);

            _dynamicEcommerceDb.Remove(user);
            result = _dynamicEcommerceDb.SaveChanges() > 0;
            return result;
        }


        //Implementazione dei CRUD di USERROLE
        List<UserRole> IDynamicEcommerceRepo.GetUserRoles()
        {
            List<UserRole> userRole = new List<UserRole>();

        userRole = _dynamicEcommerceDb.UserRole.ToList();

            return userRole;
        }
        bool IDynamicEcommerceRepo.AddUserRole(UserRole userRole)
        {
            bool result = false;

            _dynamicEcommerceDb.UserRole.Add(userRole);
            result = _dynamicEcommerceDb.SaveChanges() > 0;
            return result;
        }

        //Implementazione dei CRUD di ROLE
        List<Roles> IDynamicEcommerceRepo.GetRoles()
        {
            List<Roles> roles = new List<Roles>();

            roles = _dynamicEcommerceDb.Roles.ToList();

            return roles;
        }

        public Roles GetRoleById(int roleId)
        {
            Roles role = _dynamicEcommerceDb.Roles.FirstOrDefault(u => u.RoleID == roleId);
            return role;
        }


        //IMPLEMENTAZIONE dei CRUD di PRODUCTCATEGORIES
        List<ProductCategories> IDynamicEcommerceRepo.GetCategories()
        {
            List<ProductCategories> categories = new List<ProductCategories>();

            categories = _dynamicEcommerceDb.ProductCategories.ToList();

            return categories;
        }

        bool IDynamicEcommerceRepo.AddCategorie(ProductCategories categorie)
        {
            bool result = false;


            _dynamicEcommerceDb.ProductCategories.Add(categorie);
            result = _dynamicEcommerceDb.SaveChanges() > 0;

            return result;
        }

        public ProductCategories GetCategorieById(int id)
        {
            ProductCategories categorie = _dynamicEcommerceDb.ProductCategories.FirstOrDefault(u => u.ProductCategoriesID == id);
            return categorie;

        }
       
        bool IDynamicEcommerceRepo.DeleteCategorie(int Id)
        {
            bool result = false;
            ProductCategories categorie = _dynamicEcommerceDb.ProductCategories.FirstOrDefault(x => x.ProductCategoriesID == Id);

            _dynamicEcommerceDb.Remove(categorie);
            result = _dynamicEcommerceDb.SaveChanges() > 0;
            return result;
        }

        //Implementazione dei CRUD di Product
        List<Products> IDynamicEcommerceRepo.GetProducts()
        {
            List<Products> products = new List<Products>();

            products = _dynamicEcommerceDb.Products.ToList();

            return products;
        }

        public Products GetProductById(int id)
        {
            Products product = _dynamicEcommerceDb.Products.FirstOrDefault(u => u.ProductID == id);
            return product;

        }

        public Products GetProductByCategorie(int categorieId)
        {
            Products product = _dynamicEcommerceDb.Products.FirstOrDefault(u => u.ProductCategoriesID == categorieId);
            return product;

        }

        bool IDynamicEcommerceRepo.AddProduct(Products product)
        {
            bool result = false;


            _dynamicEcommerceDb.Products.Add(product);
            result = _dynamicEcommerceDb.SaveChanges() > 0;

            return result;
        }

        public async Task<bool> PutProduct(Products product)
        {
            try
            {
                _dynamicEcommerceDb.Entry(product).State = EntityState.Modified;
                await _dynamicEcommerceDb.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(PutUser)}: {ex.Message}");
                return false;
            }
        }

        bool IDynamicEcommerceRepo.DeleteProduct(int Id)
        {
            bool result = false;
            Products product = _dynamicEcommerceDb.Products.FirstOrDefault(x => x.ProductID == Id);

            _dynamicEcommerceDb.Remove(product);
            result = _dynamicEcommerceDb.SaveChanges() > 0;
            return result;
        }

        //IMPLEMENTAZIONE dei CRUD di ORDERS

        List<Orders> IDynamicEcommerceRepo.GetOrders()
        {
            List<Orders> orders = new List<Orders>();

            orders = _dynamicEcommerceDb.Orders.ToList();

            return orders;
        }
        public Orders GetOrderById(int id)
        {
            Orders order = _dynamicEcommerceDb.Orders.FirstOrDefault(u => u.OrderID == id);
            return order;

        }
        public Orders GetOrderByUserId(int userId)
        {
            Orders order = _dynamicEcommerceDb.Orders.FirstOrDefault(u => u.UserID == userId);
            return order;

        }
        bool IDynamicEcommerceRepo.AddOrder(Orders order)
        {
            bool result = false;


            _dynamicEcommerceDb.Orders.Add(order);
            result = _dynamicEcommerceDb.SaveChanges() > 0;

            return result;
        }
        bool IDynamicEcommerceRepo.DeleteOrder(int Id)
        {
            bool result = false;
            Orders order = _dynamicEcommerceDb.Orders.FirstOrDefault(x => x.OrderID == Id);

            _dynamicEcommerceDb.Remove(order);
            result = _dynamicEcommerceDb.SaveChanges() > 0;
            return result;
        }


        //IPLEMENTAZIONE CRUD ORDERDETAILS

        List<OrderDetails> IDynamicEcommerceRepo.GetOrdersDetails()
        {
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            orderDetails = _dynamicEcommerceDb.OrderDetails.ToList();
            return orderDetails;
        }
        OrderDetails IDynamicEcommerceRepo.GetOrderDetailsById(int id)
        {
            OrderDetails orderDetail = _dynamicEcommerceDb.OrderDetails.FirstOrDefault(u => u.OrderDetailsID == id);
            return orderDetail;
        }
        OrderDetails IDynamicEcommerceRepo.GetOrderDetailsByOrderId(int orderId)
        {
            OrderDetails orderDetail = _dynamicEcommerceDb.OrderDetails.FirstOrDefault(u => u.OrderID == orderId);
            return orderDetail;
        }
        bool IDynamicEcommerceRepo.DeleteOrderDetailsByProductId(int productId)
        {
            bool result = false;
            OrderDetails orderDetail = _dynamicEcommerceDb.OrderDetails.FirstOrDefault(u => u.ProductID == productId);
            _dynamicEcommerceDb.Remove(orderDetail);
            result = _dynamicEcommerceDb.SaveChanges() > 0;
            return result;
        }

    }



}
