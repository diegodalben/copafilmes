dotnet clean
dotnet build /p:DebugType=Full

dotnet test ./Test/CopaFilmes.BizLogic.Test/CopaFilmes.BizLogic.Test.csproj ^
    /p:CollectCoverage=true ^
    /p:CoverletOutputFormat=opencover  ^
    /p:Exclude=\"[CopaFilmes.Infrastructure]*,[CopaFilmes.*]*.Dtos.*,[CopaFilmes.*]*.Entities.*,[CopaFilmes.*]*.Extensions.*,[CopaFilmes.*]CopaFilmes.BizLogic.BizRules.CompetitionBizFactory,[CopaFilmes.*]CopaFilmes.BizLogic.BizValidations.CompetitionBizValidationFactory\" ^
    --no-build

dotnet test ./Test/CopaFilmes.DataAccess.Test/CopaFilmes.DataAccess.Test.csproj ^
    /p:CollectCoverage=true ^
    /p:CoverletOutputFormat=opencover ^
    /p:Exclude=\"[CopaFilmes.Infrastructure]*,[CopaFilmes.BizLogic]*\" ^
    --no-build

dotnet test ./Test/CopaFilmes.WebApi.Test/CopaFilmes.WebApi.Test.csproj ^
    /p:CollectCoverage=true ^
    /p:CoverletOutputFormat=opencover  ^
    /p:Exclude=\"[CopaFilmes.Infrastructure]*,[CopaFilmes.BizLogic]*,[CopaFilmes.DataAccess]*,[CopaFilmes.*]*.Filters.*,[CopaFilmes.*]CopaFilmes.WebApi.Program,[CopaFilmes.*]CopaFilmes.WebApi.Startup\" ^
    --no-build