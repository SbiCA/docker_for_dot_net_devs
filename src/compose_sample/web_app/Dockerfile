# Builder
# --------------------------------------
FROM microsoft/dotnet:1.1-sdk as builder
WORKDIR /app
COPY webapp.csproj .

RUN dotnet restore 
COPY . .
RUN dotnet build -c release 
RUN dotnet publish -c release -o pubdir 

# Final Build
# --------------------------------------
FROM microsoft/dotnet:1.1-runtime
COPY --from=builder /app/pubdir /app
ENV ASPNETCORE_URLS http://+:5000
ENTRYPOINT ["dotnet", "/app/webapp.dll"]
EXPOSE 5000