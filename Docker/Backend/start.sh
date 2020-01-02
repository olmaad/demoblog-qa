#!/bin/sh

if [ "$#" -ne 0 ]; then
  if [ "$1" = "generate" ]; then
    cd /app/builder
    dotnet BaseBuilder.dll /TestData /app/backend
	exit
  fi
fi

cd /app/backend
dotnet Backend.dll