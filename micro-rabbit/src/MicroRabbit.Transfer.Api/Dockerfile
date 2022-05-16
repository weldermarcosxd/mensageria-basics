FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/MicroRabbit.Transfer.Apí/MicroRabbit.Transfer.Apí.csproj", "MicroRabbit.Transfer.Apí/"]
RUN dotnet restore "src/MicroRabbit.Transfer.Apí/MicroRabbit.Transfer.Apí.csproj"
COPY . .
WORKDIR "/src/MicroRabbit.Transfer.Apí"
RUN dotnet build "MicroRabbit.Transfer.Apí.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroRabbit.Transfer.Apí.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroRabbit.Transfer.Apí.dll"]
