#!/bin/bash
set -ev

pwd

ls -la

echo dotnet build -c $BUILD_CONFIG Tee

dotnet build -c $BUILD_CONFIG Tee

## END ##