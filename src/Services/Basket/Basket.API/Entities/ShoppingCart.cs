using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart()
        {

        }
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        // We will use this method since we are just getting data from it, not setting any data
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var items in Items)
                {
                    totalPrice += items.Price * items.Quantity;
                }
                return totalPrice;
            }
        }
    }
}


