
# Be creative

- Use nginx [...](http://www.c-sharpcorner.com/article/fun-with-docker-compose-using-net-core-and-nginx/)
- Something with postgres [...](https://github.com/joncloud/dotnet-compose)
- Or maybe sql-server [...](https://docs.docker.com/compose/aspnet-mssql-compose/)
- Windows container legacy walkthrough [... (even worth checking it out)](https://github.com/sixeyed/presentations/tree/master/sdd/sdd-2017)


# Build a db container

```docker
docker run -d --name=mongo mongodb
```

You can also use RavenDb, MS-SQL, Postgres ... depending on your preference. But from a docker perspective it doesn't change too much :)

# Build your api-server image

using .net core or nodejs (express)



# Connect to two containers

## portmappings

link server => db

## start manually

... but this is tedious ..

# Docker compsose 

Compose file basics

TODO Copy definition from docker documenation

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

# Fire it up ...

```docker

docker-compose up -d .
```

When we have time left:
- https://portainer.io/install.html