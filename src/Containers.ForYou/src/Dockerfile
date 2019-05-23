FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln ./
COPY src/*.csproj ./src/
COPY src/*.config ./src/
COPY tests/*.csproj ./tests/
RUN nuget restore

# copy everything else and build app
COPY src/. ./src/
COPY tests/. ./tests/

WORKDIR /app/src/
RUN msbuild /p:Configuration=Release


FROM build AS testrunner
WORKDIR /app/tests
# Hacky but it works for the moment ...
RUN nuget restore containers.foryou.home.tests.csproj
RUN msbuild /p:Configuration=Debug

RUN dotnet vstest bin/Debug/net48/containers.foryou.home.tests.dll

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/src/. ./

EXPOSE 80
ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]