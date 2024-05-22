using AthleisurceAPI.Models;
using AthleisurceAPI.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AthleisurceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet("/api/Orders/{customerId}")]
        public IActionResult GetOrdersForCustomer(string customerId)
        {
            try
            {
                var orders = _orderService.GetOrdersForCustomer(customerId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult CreateOrder(OrderModel order)
        {
            try
            {
                _orderService.CreateOrder(order);
                return Ok("Order created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult UpdateOrder(OrderModel updatedOrder)
        {
            try
            {
                _orderService.UpdateOrder(updatedOrder);
                return Ok("Order updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(string orderId)
        {
            try
            {
                if (!ObjectId.TryParse(orderId, out var objectId))
                    return BadRequest("Invalid order ID.");

                _orderService.DeleteOrder(orderId);
                return Ok("Order deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("get/{orderId}")]
        public IActionResult GetOrderById(string orderId)
        {
            try
            {
                if (!ObjectId.TryParse(orderId, out var objectId))
                    return BadRequest("Invalid order ID.");

                var order = _orderService.GetOrderById(orderId);
                if (order == null)
                    return NotFound("Order not found.");

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
