#!/bin/bash
set -ev

pwd

ls -la

echo dotnet build -c $BUILD_CONFIG Tee.sln

dotnet build -c $BUILD_CONFIG Tee.sln

## END ##