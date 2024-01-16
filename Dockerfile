# Use the runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy only the project file to restore dependencies
COPY ["ArtistiqueCastingAPI.csproj", "."]
RUN dotnet restore "ArtistiqueCastingAPI.csproj"

# Copy the entire project and build it
COPY . .
WORKDIR "/src"
RUN dotnet publish "ArtistiqueCastingAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

ARG ConnectionStringName
ENV ConnectionStringName=$ConnectionStringName

ARG SecretKeyToWorkWithJWT
ENV SecretKeyToWorkWithJWT=$SecretKeyToWorkWithJWT

# Use the runtime image again for the final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ArtistiqueCastingAPI.dll"]