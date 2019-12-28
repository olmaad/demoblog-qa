FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

WORKDIR /app
COPY Outputs/ ./

EXPOSE 80

ENTRYPOINT ["dotnet", "Backend.dll"]
