dotnet clean
dotnet build /p:DebugType=Full

dotnet test ^
    /p:CollectCoverage=true ^
    /p:CoverletOutputFormat=opencover ^
    /p:Exclude=\"[CopaFilmes.Infrastructure]*,[CopaFilmes.BizLogic]*\" ^
    --no-build

dotnet reportgenerator -reports:coverage.opencover.xml -targetdir:report