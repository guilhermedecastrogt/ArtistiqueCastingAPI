FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ArtistiqueCastingAPI.csproj", "ArtistiqueCastingAPI/"]
RUN dotnet restore "ArtistiqueCastingAPI/ArtistiqueCastingAPI.csproj"
COPY . .
WORKDIR "/src/ArtistiqueCastingAPI"
RUN dotnet build "ArtistiqueCastingAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArtistiqueCastingAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

ARG ConnectionStringName
ENV ConnectionStringName=$ConnectionStringName

#ARG FtpConnectionUsername
#ENV FtpConnectionUsername=$FtpConnectionUsername
#ENV FtpConnectionPassword=$FtpConnectionPassword
#ARG FtpConnectionServerUrl
#ENV FtpConnectionServerUrl=$FtpConnectionServerUrl

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArtistiqueCastingAPI.dll"]