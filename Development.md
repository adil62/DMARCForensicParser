## Steps to push the package

1) Change package version in .csproj
2) Create the nupkg `dotnet pack --configuration Release`
3) Push the package 
  `dotnet nuget push DMARCForensicParser.1.2.0.nupkg --api-key ${apikey} --source https://api.nuget.org/v3/index.json`
