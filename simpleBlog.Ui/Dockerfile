#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5003
ENV ASPNETCORE_URLS http://*:5003

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["simpleBlog.Ui/simpleBlog.Ui.csproj", "simpleBlog.Ui/"]
RUN dotnet restore "simpleBlog.Ui/simpleBlog.Ui.csproj"
COPY . .
WORKDIR "/src/simpleBlog.Ui"
RUN dotnet build "simpleBlog.Ui.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "simpleBlog.Ui.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "simpleBlog.Ui.dll"]