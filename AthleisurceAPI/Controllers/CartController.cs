using AthleisurceAPI.Models;
using AthleisurceAPI.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AthleisurceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        // GET: Get Cart Items
        [HttpGet("/api/cart/{customerId}")]
        public IActionResult GetCart(string customerId)
        {
            var cartItems = _cartService.GetCartItems(customerId);
            return Ok(cartItems);
        }

        // POST: Add to Cart
        [HttpPost]
        public IActionResult AddToCart(CartItemModel item)
        {
            try
            {
                _cartService.AddToCart(item);
                return Ok("Item added to cart successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // PUT: Update Cart Item
        [HttpPut("{customerId}/{productId}")]
        public IActionResult UpdateCartItem(string customerId, string productId, int newQuantity)
        {
            try
            {
                _cartService.UpdateCartItem(customerId, productId, newQuantity);
                return Ok("Cart item updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // DELTE
        [HttpDelete("{customerId}/{productId}")]
        public IActionResult RemoveFromCart(string customerId, string productId)
        {
            try
            {
                _cartService.RemoveFromCart(customerId, productId);
                return Ok("Item removed from cart successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
