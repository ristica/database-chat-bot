
****************************************************
Data Source=127.0.0.1,1433;Initial Catalog=sakila;User Id=sakila;Password=p_ssW0rd;TrustServerCertificate=true;

docker run -p 1433:1433 -d sakiladb/sqlserver:latest

https://github.com/sakiladb/sqlserver
****************************************************
Server=127.0.0.1;Port=3306;Database=sakila;Uid=sakila;Pwd=p_ssW0rd;

docker run -p 3306:3306 -d sakiladb/mysql:latest

https://github.com/sakiladb/mysql
****************************************************
Host=127.0.0.1;Port=5432;Database=sakila;Username=sakila;Password=p_ssW0rd;

docker run -p 5432:5432 -d sakiladb/postgres:latest

https://github.com/sakiladb/postgres
****************************************************



##################
http://localhost:5041/api/database

{
  "connectionString": "Data Source=127.0.0.1,1433;Database=sakila;User Id=sakila;Password=p_ssW0rd;TrustServerCertificate=true;",
  "databaseType": "MSSQL"
}

{
  "connectionString": "Server=127.0.0.1;Port=3306;Database=sakila;Uid=sakila;Pwd=p_ssW0rd;",
  "databaseType": "MYSQL"
}

{
  "connectionString": "Host=127.0.0.1;Port=5432;Database=sakila;Username=sakila;Password=p_ssW0rd;",
  "databaseType": "POSTGRESSQL"
}
