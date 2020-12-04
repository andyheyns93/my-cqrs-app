# READ DATABASE

1. Setup the read database 
`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=7FMh-,t^qK=:#})g" -p 1433:1433 --name rent-a-car-r-db -h rent-a-car-r-db -d mcr.microsoft.com/mssql/server:2019-latest`

2. Start the read database
`docker start rent-a-car-r-db`

3. Test the read database
`sqlcmd -S localhost,1433 -U SA -P "7FMh-,t^qK=:#})g"`

4. Execute initial DB related rss
`CREATE DATABASE RentACar`

`CREATE TABLE r_cars (`
    `id INT PRIMARY KEY IDENTITY (1, 1),`
    `brand NVARCHAR (64) NOT NULL,`
    `model NVARCHAR (255) NOT NULL,`
    `year INT NOT NULL`
`);`



# WRITE DATABASE

1. Setup the read database 
`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=7FMh-,t^qK=:#})g" -p 1434:1433 --name rent-a-car-w-db -h rent-a-car-w-db -d mcr.microsoft.com/mssql/server:2019-latest`

2. Start the read database
`docker start rent-a-car-w-db`

3. Test the read database
`sqlcmd -S localhost,1434 -U SA -P "7FMh-,t^qK=:#})g"`

4. Execute initial DB related rss
`CREATE DATABASE RentACar`

`CREATE TABLE w_cars (`
    `id INT PRIMARY KEY IDENTITY (1, 1),`
    `command NVARCHAR (64) NOT NULL,`
    `payload NVARCHAR (MAX) NOT NULL`
`);`


# RABBITMQ

1. Setup the message queue
`docker run -e RABBITMQ_DEFAULT_USER=rabbitmq -e RABBITMQ_DEFAULT_PASS=xnrep23qeszChXVH -p 15672:15672 -p 5672:5672 --name rabbitmq-bus -h rabbitmq-bus -d rabbitmq:3-management`

2. Start the message queue
`docker start rabbitmq-bus`
