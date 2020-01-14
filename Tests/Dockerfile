FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /build

COPY . ./

RUN dotnet publish ./Tests/Tests.csproj -c Release -o /output

FROM microsoft/dotnet:2.2-sdk AS run

WORKDIR /app

COPY --from=build /output ./
COPY ./TestData/ ./TestData/
COPY ./Tests/Docker.runsettings ./

ENTRYPOINT ["dotnet", "vstest", "--Settings:Docker.runsettings", "--logger:nunit", "--logger:trx", "--ResultsDirectory:/results", "Tests.dll"]
