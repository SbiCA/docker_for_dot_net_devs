FROM microsoft/aspnetcore-build:1.1 as publish
WORKDIR /publish
COPY .bowerrc bower.json ./
RUN bower install
COPY my-app.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish --output ./out

FROM microsoft/dotnet:1.1-runtime
WORKDIR /dotnetapp
COPY --from=publish /publish/out .
ENV ASPNETCORE_URLS "http://0.0.0.0:5000/"
ENTRYPOINT ["dotnet", "web_app.dll"]
