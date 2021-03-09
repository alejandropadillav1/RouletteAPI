FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
MAINTAINER Alejandro Padilla Valderrama "alejandropadillav@yahoo.es"
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TestMasiv/TestMasiv.csproj", "TestMasiv/"]
RUN dotnet restore "TestMasiv/TestMasiv.csproj"
COPY . .
WORKDIR "/src/TestMasiv"
RUN dotnet build "TestMasiv.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestMasiv.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestMasiv.dll"]