FROM microsoft/dotnet:2.2-aspnetcore-runtime

COPY builder.json /TestData/

WORKDIR /app/builder
COPY BaseBuilderPublish/ ./

WORKDIR /app/backend
COPY BackendPublish/ ./

WORKDIR /app
COPY start.sh ./

EXPOSE 80

ENTRYPOINT ["/bin/sh", "start.sh"]
