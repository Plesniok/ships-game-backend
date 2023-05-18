# Ships REST API

## Prerequisites
To run this project, you will need the following:

.NET Core SDK (version 3.1 or higher)
PostgreSQL (version 9.5 or higher)
## Getting Started
Clone this repository to your local machine.

Navigate to the project directory.

Open the database\ships.cs file and update the ConnectionString property with your own connection string. Example connection string: 
```
'Server=localhost;Port=5432;Database=ships;User Id=postgres;Password=passwd123;.'
````

Make sure if defined database exists in yous psql server

To run service, use command 
```
dotnet run
```

The API should now be available at http://localhost:3000.

## Documentation

After you run service, swagger documentatnion will be able at http://localhost:3000/swagger
