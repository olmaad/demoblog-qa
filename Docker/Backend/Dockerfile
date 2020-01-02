FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

COPY builder.json /TestData/

WORKDIR /app/builder
COPY BaseBuilder/ ./

WORKDIR /app/backend
COPY Outputs/ ./

WORKDIR /app
COPY start.sh ./

EXPOSE 80

ENTRYPOINT ["/bin/sh", "start.sh"]
