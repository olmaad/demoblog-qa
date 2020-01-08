#!/bin/sh

if [ "$#" -ne 0 ] && [ "$1" = "generate" ]; then
  rm /app/backend/blog.db
  cd /app/builder
  dotnet BaseBuilder.dll /TestData /app/backend
  exit
fi

if [ ! -f /app/backend/blog.db ]; then
  cd /app/builder
  dotnet BaseBuilder.dll /TestData /app/backend
fi

cd /app/backend
dotnet Backend.dll