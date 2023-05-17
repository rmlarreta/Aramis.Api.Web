FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Aramis.Api.Web/Aramis.Api.Web.csproj", "Aramis.Api.Web/"]
RUN dotnet restore "Aramis.Api.Web/Aramis.Api.Web.csproj"
COPY . .
WORKDIR "/src/Aramis.Api.Web"
RUN dotnet build "Aramis.Api.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aramis.Api.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet add package SkiaSharp --version 2.88.3
RUN dotnet add package SkiaSharp.NativeAssets.Linux.NoDependencies --version 2.88.3

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
RUN apt-get update && apt-get install -y libfontconfig1 libx11-dev libgl1-mesa-dev libgdiplus
WORKDIR /app
COPY --from=publish /app/publish .
RUN chown -R www-data /app
RUN chmod -R g+rwx /app
ENTRYPOINT ["dotnet", "Aramis.Api.Web.dll"]
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS=http://+:5000
