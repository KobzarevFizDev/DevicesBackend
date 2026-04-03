<h1 align="center">DevicesBackend </h1>

<h2 align="left">Task: </h2>

Using .NET 5+ (or higher), write a small web server (ASP.Net Core) capable of handling 4 requests:
**(GET)** : /api/devices - get list of devices
**(POST)** : /api/devices/{id} - add a new device
**(DELETE)** : /api/devices/{id} - delete an existing device by id
**(DELETE)** : /api/devices/type/{type} - delete existing devices by type

Data model to be stored:

```
{
    “Id”:1,
    “Type”: ”Type_1”
}
```

The server must run on port 12300 and operate within HTTP 1.x.
Authentication model: Basic authentication
Data storage is implemented using SQLite. For data access, either EF Core or Dapper can be used.

<h2 align="left">Solution: </h2>

The task is implemented on .NET 8 using EF Core for SQLite access.
Basic authentication model is used.
**Password: admin**
**Login: admin**
To access the resource, the Authorization header of the HTTP request must have the value: **Basic YWRtaW46YWRtaW4=**

<h2 align="left"> Example requests in Postman: </h2>

GET request to retrieve all devices
![Interface](https://github.com/KobzarevFizDev/DevicesBackend/raw/main/images/1.png)

POST request to create a new device
![Interface](https://github.com/KobzarevFizDev/DevicesBackend/raw/main/images/2.png)

DELETE request to delete a device by ID
![Interface](https://github.com/KobzarevFizDev/DevicesBackend/raw/main/images/3.png)

DELETE request to delete devices by type
![Interface](https://github.com/KobzarevFizDev/DevicesBackend/raw/main/images/4.png)
