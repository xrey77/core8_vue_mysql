SPA - Single Page Application
.NET NET CORE 8, VueJS 3(typescript) and MySql start-up project

Requirements:
1. Node 18.12.0 and NPM 8.19.2
2. @vue/cli 5.0.8
3. Dotnet Core v8.0.204
4. MySql 8.0.25 Server Docker Container
5. Bootstrap 5.3.7
6. FontAwesome @fortawesome/free-solid-svg-icons@7.0.0
7. Visual Studio Code
8. Install Google Authenticator in your Mobile Phone

Features :
1. Authentication and Authorization
   BryCrypt, JWT
2. Time Based One Time Password Authenticator
3. User Profile Picture upload/update
4. Product Listings and Pagination
5. User Account Activation via Email
6. Swagger RESTful API Documentation

If you want to test, do the following:
1. Setup MySql Server 8.0.25
2. Change Username and Password in appsettings.json, MySql connection settings
3. Run, dotnet ef migrations add InitialCreate and dotnet ef database update