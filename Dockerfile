FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update && apt-get install -y libfontconfig1
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Aramis.Api.Web/Aramis.Api.Web.csproj", "Aramis.Api.Web/"]
RUN dotnet restore "Aramis.Api.Web/Aramis.Api.Web.csproj"
COPY . .
WORKDIR "/src/Aramis.Api.Web"
RUN dotnet build "Aramis.Api.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aramis.Api.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aramis.Api.Web.dll"]
