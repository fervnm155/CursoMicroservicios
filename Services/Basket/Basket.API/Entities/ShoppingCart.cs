namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string Username { get; set; } = null!;

        public List<ShoppingCartItem> shoppingCartItemsItems { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach(var item in shoppingCartItemsItems)
                {
                    totalPrice += item.Price + item.Quantity;
                }
                return totalPrice;
            }
        }

        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            Username = username;
        }
    }
}
