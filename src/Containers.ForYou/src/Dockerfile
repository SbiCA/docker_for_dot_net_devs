FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS runtime
ARG source
WORKDIR /inetpub/wwwroot
COPY ${source:-bin/Release/Publish} .

EXPOSE 80