dotnet clean
dotnet build /p:DebugType=Full

dotnet test ^
    /p:CollectCoverage=true ^
    /p:CoverletOutputFormat=opencover  ^
    /p:Exclude=\"[CopaFilmes.Infrastructure]*,[CopaFilmes.*]*.Dtos.*,[CopaFilmes.*]*.Entities.*,[CopaFilmes.*]*.Extensions.*,[CopaFilmes.*]CopaFilmes.BizLogic.BizRules.CompetitionBizFactory,[CopaFilmes.*]CopaFilmes.BizLogic.BizValidations.CompetitionBizValidationFactory\" ^
    --no-build

dotnet reportgenerator -reports:coverage.opencover.xml -targetdir:report