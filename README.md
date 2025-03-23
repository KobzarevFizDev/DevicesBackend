<h1 align="center">DevicesBackend </h1>

<h2 align="left">Задача: </h2>

Используя .net 5+ написать небольшой веб сервер (ASP.Net Core), способный принимать 4 
запроса:
<br/> **(GET)** : /api/devices - получение списка устройств
<br/> **(POST)** : /api/devices/{id} - добавление нового устройства
<br/> **(DELETE)** : /api/devices/{id} - удаление существующего устройства по id
<br/> **(DELETE)** : /api/devices/type/{type} - удаление существующих устройств по типу
Модель сохраняемых данных:
```
{
“Id”:1,
“Type”: ”Type_1”
}
```
Cервер должен запустить на 12300 порту и работать в рамках HTTP 1.x:
Модель аутентификация: Basic authentication
Сохранение данных организовать в SQLite. Для доступа к данным можно использовать 
или EF Core или Dapper



<h2 align="left">Решение: </h2> 

<br/> Задача выполнена на net 8 с использованием EF Core для доступа SQLite. 
<br/> Проект развернут с помощью Docker по адресу: **http://87.228.38.57:12300**
<br/> Используется Basic authentication модель авторизации
<br/> **Пароль: admin** 
<br/> **Логин: admin**
<br/> Для доступа к ресурсу нужно чтобы в заголовке Authorization HTTP запроса было значение: **Basic YWRtaW46YWRtaW4=**
 
<h2 align="left"> Пример запросов в Posman: </h2> 

Get запрос на получение всех устройств
![Interface](https://github.com/KobzarevFizDev/DevicesBackend/raw/main/images/1.png)

Post запрос на создание нового устройства
![Interface](https://github.com/KobzarevFizDev/DevicesBackend/raw/main/images/2.png)

Delete запрос на удаление устройства по ID
![Interface](https://github.com/KobzarevFizDev/DevicesBackend/raw/main/images/3.png)

Delete запрос на удаление устройства по типу
![Interface](https://github.com/KobzarevFizDev/DevicesBackend/raw/main/images/4.png)
