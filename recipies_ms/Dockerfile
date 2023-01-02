
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG github_token

WORKDIR /src
COPY recipies_ms.csproj .
RUN dotnet nuget add source --username werit \
    --password $github_token \
    --store-password-in-clear-text \
    --name github "https://nuget.pkg.github.com/werit/index.json"

RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "recipies_ms.dll"]