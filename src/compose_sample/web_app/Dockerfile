#Builder
FROM microsoft/dotnet:1.1-sdk as builder
WORKDIR /app
COPY ./web_app.csproj .

RUN dotnet restore /app/web_app.csproj
RUN dotnet build -c release .
RUN dotnet publish -c release -o pubdir .

#Final Build
FROM microsoft/dotnet:1.1-runtime
COPY --from=builder /app/pubdir /app
ENV ASPNETCORE_URLS http://+:5000
ENTRYPOINT ["dotnet", "/app/web_app.dll"]
EXPOSE 5000