using Microsoft.AspNetCore.Mvc;
using OrderApi.Models;

namespace OrderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    
    private static List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Laptop", Stock = 10 },
        new Product { Id = 2, Name = "Mouse", Stock = 50 },
        new Product { Id = 3, Name = "Keyboard", Stock = 30 }
    };

    private static List<Order> Orders = new();

    
    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        foreach (var item in order.Items)
        {
            var product = Products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product == null || product.Stock < item.Quantity)
                return BadRequest($"Yeterli stok yok: {item.ProductId}");

            product.Stock -= item.Quantity;
        }

        order.Id = Orders.Count + 1;
        order.CreatedAt = DateTime.Now;
        Orders.Add(order);

        return Ok(order);
    }

    
    [HttpGet("user/{userId}")]
    public IActionResult GetUserOrders(string userId)
    {
        var userOrders = Orders.Where(o => o.UserId == userId).ToList();
        return Ok(userOrders);
    }

    
    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        var order = Orders.FirstOrDefault(o => o.Id == id);
        if (order == null) return NotFound();
        return Ok(order);
    }

   
    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        var order = Orders.FirstOrDefault(o => o.Id == id);
        if (order == null) return NotFound();

        // Stoklarý geri ekle
        foreach (var item in order.Items)
        {
            var product = Products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product != null)
                product.Stock += item.Quantity;
        }

        Orders.Remove(order);
        return NoContent();
    }
}

/*
{
  "id": 0,
  "userId": "user1",
  "createdAt": "2025-08-29T14:00:32.146Z",
  "items": [
    {
      "id": 0,
      "quantity": 2,
      "productId": 1,
      "product": {
        "id": 1,
        "name": "Laptop",
        "stock": 10
      }
    }
  ]
}
---
{
    "id": 0,
  "userId": "user2",
  "createdAt": "2025-08-29T14:05:00.000Z",
  "items": [
    {
        "id": 0,
      "quantity": 1,
      "productId": 1,
      "product": {
            "id": 1,
        "name": "Laptop",
        "stock": 10
      }
    },
    {
        "id": 0,
      "quantity": 3,
      "productId": 2,
      "product": {
            "id": 2,
        "name": "Mouse",
        "stock": 50
      }
    }
  ]
}
---
{
    "id": 0,
  "userId": "user3",
  "createdAt": "2025-08-29T14:10:00.000Z",
  "items": [
    {
        "id": 0,
      "quantity": 100,
      "productId": 1,
      "product": {
            "id": 1,
        "name": "Laptop",
        "stock": 10
      }
    }
  ]
}
---
{
    "id": 0,
  "userId": "user4",
  "createdAt": "2025-08-29T14:15:00.000Z",
  "items": [
    {
        "id": 0,
      "quantity": 2,
      "productId": 2,
      "product": {
            "id": 2,
        "name": "Mouse",
        "stock": 50
      }
    },
    {
        "id": 0,
      "quantity": 1,
      "productId": 3,
      "product": {
            "id": 3,
        "name": "Keyboard",
        "stock": 30
      }
    },
    {
        "id": 0,
      "quantity": 1,
      "productId": 1,
      "product": {
            "id": 1,
        "name": "Laptop",
        "stock": 10
      }
    }
  ]
}
---
{
    "id": 0,
  "userId": "user5",
  "createdAt": "2025-08-29T14:20:00.000Z",
  "items": [
    {
        "id": 0,
      "quantity": 10,
      "productId": 1,
      "product": {
            "id": 1,
        "name": "Laptop",
        "stock": 10
      }
    }
  ]
}
*/