#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5002
ENV ASPNETCORE_URLS http://*:5002

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["simpleBlog.Api/simpleBlog.Api.csproj", "simpleBlog.Api/"]
RUN dotnet restore "simpleBlog.Api/simpleBlog.Api.csproj"
COPY . .
WORKDIR "/src/simpleBlog.Api"
RUN dotnet build "simpleBlog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "simpleBlog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "simpleBlog.Api.dll"]