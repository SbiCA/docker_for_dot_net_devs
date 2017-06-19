# Understanding a dockerfile

```Dockerfile
# defines base image
FROM microsoft/dotnet:1.1-sdk
# set the working directory for following commands
WORKDIR /app

# copy csproj and restore as distinct layers
COPY net_core_console.csproj ./
# restore nuget packages
RUN dotnet restore

# copy and build everything else
COPY . ./
# run publish
RUN dotnet publish -c Release -o out

# define the entry point when the container gets executed
ENTRYPOINT ["dotnet", "out/net_core_console.dll"]

```
[Recommendations by the docker community](https://docs.docker.com/engine/userguide/eng-image/dockerfile_best-practices/#general-guidelines-and-recommendations)


# Create your own image

What could be useful

- **[FROM](https://docs.docker.com/engine/userguide/eng-image/dockerfile_best-practices/#from)**
- [RUN](https://docs.docker.com/engine/reference/builder/#run)
- **[CMD](https://docs.docker.com/engine/userguide/eng-image/dockerfile_best-practices/#cmd)**
- **[ENTRYPOINT](https://docs.docker.com/engine/reference/builder/#entrypoint)**
- [CMD vs. ENTRYPOINT](https://docs.docker.com/engine/reference/builder/#understand-how-cmd-and-entrypoint-interact)
- [WORKDIR](https://docs.docker.com/engine/userguide/eng-image/dockerfile_best-practices/#workdir)
- **[ADD or COPY](https://docs.docker.com/engine/userguide/eng-image/dockerfile_best-practices/#add-or-copy)**
- [EXPOSE](https://docs.docker.com/engine/userguide/eng-image/dockerfile_best-practices/#expose)
- [VOLUME](https://docs.docker.com/engine/userguide/eng-image/dockerfile_best-practices/#volume)
- [HEALTHCHECK](https://docs.docker.com/engine/reference/builder/#healthcheck)

but wait there are more ... [Dockerfile reference](https://docs.docker.com/engine/reference/builder/)

and here are some more commands regarding images ... [Image commands](https://docs.docker.com/engine/reference/commandline/image/)

Tip:

- structure an image around change use layering e.g. only restore nuget packages if you changed something
- use trusted registry / official images as base images

Security concerns:

- use vulnerability scans (docker offers out of the box checks)
- check out docker notary (image signing + trusted registry)

# Build the image

```Docker

# using a Dockerfile in the current directory
docker build -t myimage:0.1 .

# or specifiy the dockerfile using -f
docker build -t myimage:0.1 -f ./MySuperDockerfile

```

# Run your image

```Docker
docker run --rm -it myimage:0.1

```

# Working with your local registry
```Docker
# check your local registry
docker images

# remove an image
docker rmi {image id}
docker image rm {image id}

# see what happend
docker image history

# and the other commands
# ----------------------------------
docker image --help

Usage:  docker image COMMAND

Manage images

Options:
      --help   Print usage

Commands:
  build       Build an image from a Dockerfile
  history     Show the history of an image
  import      Import the contents from a tarball to create a filesystem image
  inspect     Display detailed information on one or more images
  load        Load an image from a tar archive or STDIN
  ls          List images
  prune       Remove unused images
  pull        Pull an image or a repository from a registry
  push        Push an image or a repository to a registry
  rm          Remove one or more images
  save        Save one or more images to a tar archive (streamed to STDOUT by default)
  tag         Create a tag TARGET_IMAGE that refers to SOURCE_IMAGE

Run 'docker image COMMAND --help' for more information on a command.
```


# Make some code changes

1. Edit your Progam.cs
2. Build again
3. run it 

```docker
# here is how it could look like :)

D:\Git\docker_for_dot_net_devs\src\net_core_console (master)
λ docker build -t sibica/console:0.1 .
Sending build context to Docker daemon 7.168 kB
Step 1/7 : FROM microsoft/dotnet:1.1-sdk
 ---> 5ff94bceebcf
Step 2/7 : WORKDIR /app
 ---> Using cache
 ---> 4971e1153302
Step 3/7 : COPY net_core_console.csproj ./
 ---> Using cache
 ---> 51
Step 4/7 : RUN dotnet restore
 ---> Using cache
 ---> 9b1734a1dd94
Step 5/7 : COPY . ./
 ---> 3d547c2cf65e
Removing intermediate container 670da65e1f40
Step 6/7 : RUN dotnet publish -c Release -o out
 ---> Running in c07e72b0664a
Microsoft (R) Build Engine version 15.1.1012.6693
Copyright (C) Microsoft Corporation. All rights reserved.

  net_core_console -> /app/bin/Release/netcoreapp1.1/net_core_console.dll
 ---> 13b538987226
Removing intermediate container c07e72b0664a
Step 7/7 : ENTRYPOINT dotnet out/net_core_console.dll
 ---> Running in 496ffc57d054
 ---> 42
Removing intermediate container 496ffc57d054
Successfully built 42
SECURITY WARNING: You are building a Docker image from Windows against a non-Windows Docker host. All files and directories added to build context will have '-rwxr-xr-x' permissions. It is recommended to double check and reset permissions for sensitive files and directories.

D:\Git\docker_for_dot_net_devs\src\net_core_console (master)
λ docker run --rm -it sibica/console:0.1
Hello World! From Docker

```

# Tagging

why it's useful?
[tagging documentation (How to tag an image )](https://docs.docker.com/engine/reference/commandline/tag/#examples)

Some examples for tagging
- [.Net CORE docker samples](https://github.com/dotnet/dotnet-docker-samples)
- [.Net Full frameworks samples](https://github.com/microsoft/dotnet-framework-docker-samples)

[When working with windows use other escape character !](https://docs.docker.com/engine/reference/builder/#escape)


# Create new web app

1. File new => .net core wep app mvc (no auth), but feel free to choose your stack.
2. Create your Dockerfile and build it
3. Run container in the background

```Docker
docker run -d myimage

```

# Multi stage containers

** If your running docker for windows => you need the edge channel !**

- [Mulit stage official documentation](https://docs.docker.com/engine/userguide/eng-image/multistage-build/)
- [How to setup a mulit stage build with .net core](https://erwinsteffens.com/post/net-core-docker-multi-stage-builds/)
 
http://training.play-with-docker.com/multi-stage/ 

Here is an example for the console app

```docker
#Builder
FROM microsoft/dotnet:1.1-sdk

# copy all source
COPY . /myapp
RUN dotnet restore ./myapp && \
    dotnet build -c release ./myapp && \
    dotnet publish -c release -o pubdir ./myapp

#Final Build
FROM microsoft/dotnet:1.1-runtime
COPY --from=0 /myapp/pubdir /myapp

ENTRYPOINT ["dotnet", "/app/net_core_console.dll"]
```


<!--# defines base image
FROM microsoft/dotnet:1.1-sdk
# set the working directory for following commands
WORKDIR /app

# copy csproj and restore as distinct layers
COPY net_core_console.csproj ./
# restore nuget packages
RUN dotnet restore

# copy and build everything else
COPY . ./
# run publish
RUN dotnet publish -c Release -o out

# define the entry point when the container gets executed
ENTRYPOINT ["dotnet", "out/net_core_console.dll"]-->

<!--```docker
# use the builder image including the sdk
FROM aspnetcore-builder sdk AS builder

# build the source code
dotnet build

# run the unit tests
dotnet test

# target container base image (runtime only)
FROM aspnetcore runtime

# copy the builder output to target
COPY --from=builder /out/myapp /myapp

#set the working dir
WORKDIR /myapp

# define entry point
dotnet myservice.dll

# and the health check
HealthCheck --interval=3s

```-->

