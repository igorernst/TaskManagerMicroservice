FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY *.csproj .
RUN dotnet restore

COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

ENV ASPNETCORE_URLS=http://+:5209
EXPOSE 5209

ENTRYPOINT ["dotnet", "Logger.dll"]
