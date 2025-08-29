

# OrderApi

Basit bir ASP.NET Core Web API projesidir. Bu proje, e-ticaret platformları için temel bir sipariş yönetim sistemi örneği sunar.

Siparişler in-memory listeler üzerinde tutulur. Bu sayede herhangi bir veritabanı kurmaya gerek yoktur ve Swagger üzerinden doğrudan test edilebilir.

---

## Özellikler

* Yeni sipariş ekleme (POST /api/orders)
* Kullanıcının tüm siparişlerini listeleme (GET /api/orders/user/{userId})
* Sipariş detayını getirme (GET /api/orders/{id})
* Sipariş silme (DELETE /api/orders/{id})
* Stok kontrolü yapılır, yeterli stok yoksa hata döner.

---

## Model Yapısı

### Product
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
}

### OrderItem
public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}

### Order
public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}

---

## Kurulum

1. Projeyi klonlayın veya zip olarak indirin.
2. .NET 8 SDK yüklü olmalı.
3. Terminal veya Visual Studio'dan proje klasörüne gidin.
4. Çalıştırmak için:
dotnet restore
dotnet run

Tarayıcıda aç: http://localhost:5000/
Swagger UI açılacak, tüm endpoint'leri test edebilirsiniz.

---

## Kullanım

### 1. Yeni Sipariş Ekleme
POST /api/orders
Yeni sipariş ekler. Yeterli stok yoksa 400 Bad Request döner.

Örnek JSON (tek ürün):
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

### 2. Kullanıcının Siparişlerini Listeleme
GET /api/orders/user/{userId}
Kullanıcının tüm siparişlerini listeler.
GET /api/orders/user/user1

### 3. Sipariş Detayını Getirme
GET /api/orders/{id}
Belirli bir siparişin detaylarını getirir.
GET /api/orders/1

### 4. Sipariş Silme
DELETE /api/orders/{id}
Siparişi siler ve stokları geri ekler.
DELETE /api/orders/1

---

## Örnek Siparişler
Swagger'da POST için kullanabileceğiniz 5 örnek JSON:
---
{
  "id": 0,
  "userId": "user1",
  "createdAt": "2025-08-29T14:00:32.146Z",
  "items": [
    { "id": 0, "quantity": 2, "productId": 1, "product": { "id": 1, "name": "Laptop", "stock": 10 } }
  ]
}
---
{
  "id": 0,
  "userId": "user2",
  "createdAt": "2025-08-29T14:05:00.000Z",
  "items": [
    { "id": 0, "quantity": 1, "productId": 1, "product": { "id": 1, "name": "Laptop", "stock": 10 } },
    { "id": 0, "quantity": 3, "productId": 2, "product": { "id": 2, "name": "Mouse", "stock": 50 } }
  ]
}
---
{
  "id": 0,
  "userId": "user3",
  "createdAt": "2025-08-29T14:10:00.000Z",
  "items": [
    { "id": 0, "quantity": 100, "productId": 1, "product": { "id": 1, "name": "Laptop", "stock": 10 } }
  ]
}
---
{
  "id": 0,
  "userId": "user4",
  "createdAt": "2025-08-29T14:15:00.000Z",
  "items": [
    { "id": 0, "quantity": 2, "productId": 2, "product": { "id": 2, "name": "Mouse", "stock": 50 } },
    { "id": 0, "quantity": 1, "productId": 3, "product": { "id": 3, "name": "Keyboard", "stock": 30 } },
    { "id": 0, "quantity": 1, "productId": 1, "product": { "id": 1, "name": "Laptop", "stock": 10 } }
  ]
}
---
{
  "id": 0,
  "userId": "user5",
  "createdAt": "2025-08-29T14:20:00.000Z",
  "items": [
    { "id": 0, "quantity": 10, "productId": 1, "product": { "id": 1, "name": "Laptop", "stock": 10 } }
  ]
}
