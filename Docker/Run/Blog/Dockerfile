FROM olmaad/demoblogbackend

WORKDIR /app/builder

RUN dotnet BaseBuilder.dll /TestData /app/backend

WORKDIR /app
