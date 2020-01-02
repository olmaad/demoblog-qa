FROM microsoft/dotnet:2.2-sdk

WORKDIR /app

COPY Outputs/ ./
COPY TestData/ ./TestData/
COPY DockerTestData/ ./TestData/
COPY Docker.runsettings ./

ENTRYPOINT ["dotnet", "vstest", "--Settings:Docker.runsettings", "--logger:nunit", "--logger:trx", "--ResultsDirectory:/results", "Tests.dll"]
