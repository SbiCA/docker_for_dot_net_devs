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