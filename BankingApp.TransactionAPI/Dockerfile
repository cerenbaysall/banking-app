FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV PORT 5000
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["BankingApp.TransactionAPI/BankingApp.TransactionAPI.csproj", "BankingApp.TransactionAPI/"]
RUN dotnet restore "BankingApp.TransactionAPI/BankingApp.TransactionAPI.csproj"
COPY . .
WORKDIR "/src/BankingApp.TransactionAPI"
RUN dotnet build "BankingApp.TransactionAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankingApp.TransactionAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingApp.TransactionAPI.dll"]
