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
ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM build AS test
WORKDIR /app/tests
RUN dotnet build
RUN dotnet vstest bin/Debug/Home.Tests.dll


#FROM build AS publish
#WORKDIR /app/dotnetapp
#RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/src/. ./

EXPOSE 80
ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]