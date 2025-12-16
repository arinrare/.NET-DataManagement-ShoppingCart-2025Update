using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            // Check whether the Shopping Cart is empty or not.
            if (Session["cart"] != null && ((Dictionary<Product, int>)Session["cart"]).Count() != 0)
            {
                // Show Shopping Cart.
                return View("Cart");
            }
            else
            {
                // Show Shopping Cart Empty message.
                return View("Empty");
            }
        }


        // GET: Reset
        public ActionResult Reset()
        {
            // Reset the Shopping Cart.
            ((Dictionary<Product, int>)Session["cart"]).Clear();

            // Show Shopping Cart Empty message.
            return View("Empty");
        }

        [HttpPost]
        public int AddToCart()
        {
            
            using (ShoppingCartProductsEntities database = new ShoppingCartProductsEntities())
            {
            
                // Get id of product from ajax POST
                var id = Convert.ToInt32(Request["id"]);

                // Plus or minus button will aways be a 1, set quantity to reflect this
                var quantity = 1;
                
                // Find the product in the database and assign it to the product variable
                Product product = database.Products.Find(id);

                // Check if Cart exists.
                if (Session["cart"] == null)
                {
                    // Cart does not exist - Instantiate the Cart.
                    Session["cart"] = new Dictionary<Product, int>();
                }

                // Check whether Product is already in the Cart.
                if (((Dictionary<Product, int>)Session["cart"]).ContainsKey(product))
                {
                    // Product is already in Cart - Increase the Quantity.
                    ((Dictionary<Product, int>)Session["cart"])[product] += quantity;
                }
                else
                {
                    // Add Product to the Cart.
                    ((Dictionary<Product, int>)Session["cart"]).Add(product, quantity);
                }

                return ((Dictionary<Product, int>)Session["cart"])[product];
 
            }

        }


        public string RemoveFromCart()
        {

            using (ShoppingCartProductsEntities database = new ShoppingCartProductsEntities())
            {

                // Get id of product from ajax POST
                var id = Convert.ToInt32(Request["id"]);

                // Plus or minus button will aways be a 1, set quantity to reflect this
                var quantity = 1;
                
                // Variable to check if return "0" to ajax is needed if cart item has been deleted;
                var zero = false;

                // Find the product in the database and assign it to the product variable
                Product product = database.Products.Find(id);
                
                // Check if Cart exists.
                if (Session["cart"] == null)
                {
                    //Cart does not exist - Show Empty Cart message.
                    RedirectToAction("Empty");
                }
                
                // Check Product is in the Cart.
                if (((Dictionary<Product, int>)Session["cart"]).ContainsKey(product))
                {
                    
                    // Product is in Cart - Deduct Quantity.
                    ((Dictionary<Product, int>)Session["cart"])[product] -= quantity;

                    // If Quantity of Product is 0, Remove from Cart completely.
                    if (((Dictionary<Product, int>)Session["cart"])[product] == 0)
                    {
                        ((Dictionary<Product, int>)Session["cart"]).Remove(product);
                        zero = true;
                    }
                }
               
                // Check if Cart is now Empty.
                if (((Dictionary<Product, int>)Session["cart"]).Count == 0)
                {
                    // Cart is now empty after Product removal - Reset the cart.
                    Session["cart"] = null;

                    // Direct ajax to show Cart Empty message.
                }

                if (zero == true)
                {
                    return "0";
                }
                return ((Dictionary<Product, int>)Session["cart"])[product].ToString();
            }
       
        }
    }
}