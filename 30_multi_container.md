
# Be creative

Use a scenario with 2-3 different containers e.g. web-app, database and cache, or load-balancer.

- Use nginx [...](http://www.c-sharpcorner.com/article/fun-with-docker-compose-using-net-core-and-nginx/)
- Something with postgres [...](https://github.com/joncloud/dotnet-compose)
- Or maybe sql-server [...](https://docs.docker.com/compose/aspnet-mssql-compose/)
- Windows container legacy walkthrough [... (even worth checking it out)](https://github.com/sixeyed/presentations/tree/master/sdd/sdd-2017)

# First try to connect the containers manually 

Your procedure could look as follows

1. Build a db container in other words choose your image

```docker
docker run -d --name=mongo mongodb
```

  - You can also use RavenDb, MS-SQL, Postgres ... depending on your preference. But from a docker perspective it doesn't change too much :)

2. Build your api-server image

  - using .net core or nodejs (express) or what ever you like.

3. Connect to two containers
  - Define port mappings and link the two containers.

4. But you will notice ... manual work is tedious .. but compose to the rescue :)

# Docker compsose 

[Docker-compose overview](https://docs.docker.com/compose/overview/)

```docker
# basic compose cli

# building a compose file (using docker-compose.yml)
docker-compose build .
# build using different file name
docker-compose build -f my-compose.yml .

# starts your services interactive
docker-compose up .
# detached mode
docker-compose up -d .

# Stops containers and removes containers, networks, volumes, and images created by up
docker-compose down

# Removes stopped service containers
docker-compose rm

# for inspecting your service
docker-compose top


```
This is an example of compose file using windows-containers and a dedicated network.

```docker-compose

version: '3.1'

services:
  
  mta-db:
    image: microsoft/mssql-server-windows-express
    ports:
      - "1433:1433"
    environment: 
      - ACCEPT_EULA=Y
    env_file:
      - db-credentials.env
    networks:
      - app-net

  mta-app:
    image: sixeyed/sdd2017-web:v2
    ports:
      - "80:80"
    environment:
      - HOMEPAGE_URL=http://homepage
    env_file:
      - db-credentials.env
    depends_on:
      - mta-db
      - message-queue
    networks:
      - app-net

  message-queue:
    image: nats:nanoserver
    networks:
      - app-net

  mta-save-handler:
    image: sixeyed/sdd2017-save-handler
    env_file:
      - db-credentials.env
    depends_on:
      - mta-db
      - message-queue
    networks:
      - app-net

  elasticsearch:
    image: sixeyed/elasticsearch:nanoserver
    environment:
      - ES_JAVA_OPTS=-Xms512m -Xmx512m
    networks:
      - app-net

  kibana:
    image: sixeyed/kibana:nanoserver
    ports:
      - "5601:5601"
    depends_on:
      - elasticsearch
    networks:
      - app-net

  mta-index-handler:
    image: sixeyed/sdd2017-index-handler
    depends_on:
      - elasticsearch
      - message-queue
    networks:
      - app-net

  homepage:
    image: sixeyed/sdd2017-homepage
    networks:
      - app-net

networks:
  app-net:
    external:
      name: nat

```

# Next "UI" to try out (portainer.io)

- https://portainer.io/install.html

1. Run the portainer container
2. demonstrate your service to a colleague :)