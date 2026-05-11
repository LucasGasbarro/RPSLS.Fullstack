FROM node:22-alpine AS web-build
WORKDIR /src
COPY src/RPSLS.Fullstack.Web/package.json src/RPSLS.Fullstack.Web/
WORKDIR /src/src/RPSLS.Fullstack.Web
RUN npm install
COPY src/RPSLS.Fullstack.Web/ ./
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
COPY --from=web-build /src/src/RPSLS.Fullstack.Web/dist ./src/RPSLS.Fullstack.Api/wwwroot
RUN dotnet publish src/RPSLS.Fullstack.Api/RPSLS.Fullstack.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "RPSLS.Fullstack.Api.dll"]
