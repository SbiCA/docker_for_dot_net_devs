
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


# "UI" to try out (portainer.io)

- https://portainer.io/install.html
