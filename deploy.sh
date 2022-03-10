#!/bin/bash
set -ev

pwd

ls -la

echo NUGET_API_KEY ... contents hidden
echo NUGET_SOURCE == ${NUGET_SOURCE} 
echo TRAVIS_PULL_REQUEST == ${TRAVIS_PULL_REQUEST}
echo BUILD_CONFIG == ${BUILD_CONFIG}

echo BUILD_DIR == ${BUILD_DIR}

dotnet build -c $BUILD_CONFIG Tee

## END ##