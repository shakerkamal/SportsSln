# SportsStore - A simple e-Commerce Application

An e-commerce application build with ASP.NET Core MVC 6.0.

## Features
 - Products overview
 - Categories Products
 - Cart Feature
 - Placing Order
 - Admin Panel
 - Order Processing

## Built with
- Dotnet 6.0<br>
- Razor Pages<br>
- Repository Pattern <br>
- ASP.NET Identity <br>
- Docker

## Pre-requisite
To run locally, check if dotnet version 6.0 is installed by running this command:
> dotnet --version

If not installed then please install dotnet 6.0 following this link:<br>
[https://dotnet.microsoft.com/en-us/download/dotnet/6.0]()

## Using the project

Either download the project as ZIP or clone the repository using the following command.

```https://github.com/shakerkamal/SportsSln.git```

### Using the IDE
For Visual Studio build the project with `Crtl+Shift+B` command or for VS Code `dotnet build`. Once the build is done. Run the project by pressing `F5` or typing `dotnet run`.

Launch the browser and paste this link or click on this link `http://localhost:5002`.

### Using docker
If docker is install in the local environment please follow the following steps to run the application:
1. Go to SportsStore directory <br>
2. type `ls` to verify if these files are available or not
   1. Dockerfile
   2. docker-compose.yml
3. Upon availability run the following commands chronogically
   1. `docker-compose build`
   2. `docker-compose up`
4. It will take some time and the application will run on the above mentioned port
5. To stop the application type the following command
   1. `docker-compose down`

Home screen of the application
>![Home Screen](/assets/Home.png)

On the left side product categories are dynamically rendered.
>![Product categories](/assets/Category.png)

Adding products to cart.
>![Cart](/assets/Cart.png)

Placing order with shipment details and completing the order.
>![Placing order](/assets/CompletingOrder.png)
>![Placing order](/assets/OrderPlacedConfirm.png)

## Admin Section
Seperate route for admin to login into the system and managing products list.
>![Admin Login](/assets/AdminLogin.png)

Admin Home screen with Create, Edit and Delete product features.
>![Admin Home](/assets/AdminHome.png)

Admin with placed orders with details on shipped and unshipped products.
>![Orders Screen](/assets/ProcessingOrder.png)