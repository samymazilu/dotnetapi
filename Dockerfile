FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore 

# copy everything else and build app
COPY . .
RUN dotnet ef database update 
RUN dotnet publish --self-contained false  -o /app


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MyAPI.dll"]