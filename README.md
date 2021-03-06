# READ DATABASE

1. Setup the read database 

* `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=7FMh-,t^qK=:#})g" -p 1433:1433 --name rent-a-car-r-db -h rent-a-car-r-db -d mcr.microsoft.com/mssql/server:2019-latest`

2. Start the read database

* `docker start rent-a-car-r-db`

3. Test the read database

* `sqlcmd -S localhost,1433 -U SA -P "7FMh-,t^qK=:#})g"`

4. Execute initial DB related rss

```
CREATE DATABASE RentACar

CREATE TABLE R_Cars (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    AggregateId UNIQUEIDENTIFIER  NOT NULL,
    Brand NVARCHAR (64) NOT NULL,
    Model NVARCHAR (255) NOT NULL,
    Year INT NOT NULL
);
```


# WRITE DATABASE

1. Setup the read database 

* `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=7FMh-,t^qK=:#})g" -p 1435:1433 --name rent-a-car-w-db -h rent-a-car-w-db -d mcr.microsoft.com/mssql/server:2019-latest`

2. Start the read database

* `docker start rent-a-car-w-db`

3. Test the read database

* `sqlcmd -S localhost,1435 -U SA -P "7FMh-,t^qK=:#})g"`

4. Execute initial DB related rss

```
CREATE DATABASE RentACar

CREATE TABLE W_Cars (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    AggregateId UNIQUEIDENTIFIER  NOT NULL,
    Command NVARCHAR (64) NOT NULL,
    Payload NVARCHAR (MAX) NOT NULL
);
```

# RABBITMQ

1. Setup the message queue

* `docker run -e RABBITMQ_DEFAULT_USER=rabbitmq -e RABBITMQ_DEFAULT_PASS=xnrep23qeszChXVH -p 15672:15672 -p 5672:5672 --name rabbitmq-bus -h rabbitmq-bus -d rabbitmq:3-management` 

2. Start the message queue

* `docker start rabbitmq-bus`



# ElasticSearch - Kibana

1. Setup the message queue

* `kubectl create namespace logging`

2. Deploy 'ElasticSearch' to kubernetes cluster

* `kubectl apply -f kubernetes/elastic.yml -n logging`

3. Deploy 'Kibana' to kubernetes cluster

* `kubectl apply -f kubernetes/kibana.yml -n logging`


# DOCKER COMPOSE

1. Create a local network

* `docker network create localnet`

2. Run the compose command

* `docker-compose up`

```
Note:
'docker-compose.yml' uses 'hostname: rabbitmq' to expose rabbitmq to the car-catalog-api 

by sending an 'ASPNETCORE_ENVIRONMENT=Docker' environment variable to the api container, this container will load the 'appsettings.Docker.json' file.
This will set the HostName for RabbitMq from 'localhost' to 'rabbitmq' which is specified in the docker-compose.yml file.

Same goes for the sql server hostnames and ports.
```

# KUBERNETES

1. Deploy RabbitMQ 

* `kubectl apply -f .\rabbitmq\rabbitmq.yml`

2. Deploy Sql Server 

``` 
kubectl apply -f .\sqlserver\local-storage-volume-read-temp.yml
kubectl apply -f .\sqlserver\local-storage-volume-write-temp.yml
kubectl apply -f .\sqlserver\local-storage-volume.yml
kubectl apply -f .\sqlserver\sql-read-server.yml
kubectl apply -f .\sqlserver\sql-write-server.yml
```

3. Deploy App 

* `kubectl apply -f .\app.yml`

4. Navigate to ready path

* `http://localhost:5000/health/ready`

```
Note:

by sending an 'ASPNETCORE_ENVIRONMENT=Docker' environment variable to the api container, this container will load the 'appsettings.Docker.json' file.
This will set the HostName for RabbitMq from 'localhost' to '198.162.0.159' (INTERNAL IP)

Same goes for the sql server hostnames and ports.
```