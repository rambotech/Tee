language: csharp
solution: ./Tee.sln
os:
- linux
mono: none
dotnet: 6.0
script:
- chmod +x ./deploy.sh 
- ./deploy.sh

# safelist
branches:
  only:
  - main
