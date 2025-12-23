FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./DinDin.Domain/*.csproj ./DinDin.Domain/
COPY ./DinDin.Services/*.csproj ./DinDin.Services/
COPY ./DinDin.Infra/*.csproj ./DinDin.Infra/
COPY ./DinDin.Web/*.csproj ./DinDin.Web/

RUN dotnet restore ./DinDin.Web.csproj

COPY . .

RUN dotnet publish ./DinDin.Web.csproj \
    -c Release \
    -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "DinDin.Web.dll"]