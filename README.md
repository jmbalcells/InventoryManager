# **InventoryManager API**
---
A general purpose inventory management API. Built in C# NET6 with Entity Framework in memory repository, secured with JWT and described with Swagger.

>### **Endpoints**
---
+ (POST) `/Auth` - Get the authorization Token to use the endpoints.
+ (GET)  `/Items` - Get all the items from the inventory manager.
+ (POST) `/Items` - Create a new item to inventory manager.
+ (GET)  `/Items/Detail` - Get the details from an item by name.
+ (POST) `/Items/Take` - Take an item from the inventory manager.

>### **Instructions to run locally**
---
1. Download/Clone the repository
2. Start the InventoryManager Kestrel server
3. View in browser at https://localhost:5001/swagger/index.html

>### **Instructions to Authenticate**
---
1. Execute the `/Auth` endpoint using this json:
```
{
  "userName": "admin",
  "password": "admin"
}
```
2. Get the bearer token and put into the Swagger Authorize popUp.

>### **Required Features**
---
1. ADD AN ITEM TO THE INVENTORY - Implemented
2. REMOVE AN ITEM FROM THE INVENTORY BY NAME - Implemented
3. NOTIFY THAT AN ITEM HAS BEEN REMOVED FROM THE INVENTORY - Implemented (see assumptions )
4. NOTIFY WHEN AN ITEM EXPIRES - Not Implemented

>### **Assumptions**
---
We assume that we have configured a Kafka instance for creating events. In the appsettings.json there are the connection parameters and in the code the producer of the event is called.

>### **Addicional content**
---
I attach the Postman collection to use the developed endpoints.
