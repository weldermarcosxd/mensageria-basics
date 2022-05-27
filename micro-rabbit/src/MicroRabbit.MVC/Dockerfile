FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/MicroRabbit.MVC/MicroRabbit.MVC.csproj", "MicroRabbit.MVC/"]
RUN dotnet restore "src/MicroRabbit.MVC/MicroRabbit.MVC.csproj"
COPY . .
WORKDIR "/src/MicroRabbit.MVC"
RUN dotnet build "MicroRabbit.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroRabbit.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroRabbit.MVC.dll"]
