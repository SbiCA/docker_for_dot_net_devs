FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS runtime
WORKDIR /inetpub/wwwroot
COPY bin/Release/Publish .

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]