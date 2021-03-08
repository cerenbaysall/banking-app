FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV PORT 6000
EXPOSE 6000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["BankingApp.AccountAPI/BankingApp.AccountAPI.csproj", "BankingApp.AccountAPI/"]
RUN dotnet restore "BankingApp.AccountAPI/BankingApp.AccountAPI.csproj"
COPY . .
WORKDIR "/src/BankingApp.AccountAPI"
RUN dotnet build "BankingApp.AccountAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankingApp.AccountAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingApp.AccountAPI.dll"]
