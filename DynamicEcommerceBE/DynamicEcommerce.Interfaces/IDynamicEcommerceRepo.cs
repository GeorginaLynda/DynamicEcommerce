using DynamicEcommerce.Models;

namespace DynamicEcommerce.Interfaces
{
    public interface IDynamicEcommerceRepo
    {
        //METODI CRUD USER : registra, elimina, modifica

        List<Users> GetUsers();
        bool AddUser(Users users);
        Users GetUserById(int id); //SERVIRA' PER MODIFICA ED ELIMINAZIONE
        Task<bool> PutUser(Users user);
        
        bool DeleteUser(int id); //TO DO: DELETE USER

        Users GetUserByUsername(string username); //SERVE PER LOGIN
        


        //METODI CRUD USERROLE: GET
        List<UserRole> GetUserRoles();
        UserRole GetUserRoleByUserId(int userId);
        bool AddUserRole(UserRole userRole);


        //METODI CRUD ROLE: GET
        List<Roles> GetRoles();
        Roles GetRoleById(int roleId);


        //METODI CRUD PRODUCTCATEGORIES: aggiunta, elimina
        List<ProductCategories> GetCategories();
        ProductCategories GetCategorieById(int id);
       
        bool AddCategorie(ProductCategories categorie);
       
        bool DeleteCategorie(int id); //TO DO: DELETE PRODUCTCATEGORIES


        //METODI CRUD PRODUCTS: aggiunta, modifica, elimina 
        List<Products> GetProducts();
        bool AddProduct(Products product);
       Products GetProductById(int id);
        Products GetProductByCategorie(int categorieId);
       Task <bool> PutProduct(Products product);
        bool DeleteProduct(int id); //TO DO: DELETE PRODUCTS


        //METODI CRUD ORDERS: aggiunta, elimina, visualizza
        List<Orders> GetOrders();
        Orders GetOrderById(int id);
        Orders GetOrderByUserId(int userId);//gestiamo cosi per visualizzare ordini clienti??
        bool AddOrder(Orders order);
        bool DeleteOrder(int id);



        //METODI CRUD ORDERDETAILS: visualizza
        List<OrderDetails> GetOrdersDetails();
        OrderDetails GetOrderDetailsById(int id);
        OrderDetails GetOrderDetailsByOrderId(int orderId);
        bool DeleteOrderDetailsByProductId(int productId);
        
    }

}
