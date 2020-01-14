FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /build

COPY . .

RUN dotnet publish ./Backend/Backend.csproj -c Release -o /output/backend

RUN dotnet publish ./BaseBuilder/BaseBuilder.csproj -c Release -o /output/builder

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS run

COPY ./TestData/builder.json /TestData/

COPY --from=build /output/backend /app/backend

COPY --from=build /output/builder /app/builder

WORKDIR /app

COPY ./Backend/start.sh ./

EXPOSE 80

ENTRYPOINT ["/bin/sh", "start.sh"]