FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln ./
COPY src/*.csproj ./src/
COPY src/*.config ./src/
RUN nuget restore

# copy everything else and build app
COPY src/. ./src/
WORKDIR /app/src/
RUN msbuild /p:Configuration=Release


FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/src/. ./

EXPOSE 80
ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]