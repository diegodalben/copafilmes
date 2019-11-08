namespace CopaFilmes.BizLogic.BizRules.Abstraction
{
    public interface IBizRule<in TDto> where TDto : class
    {
        void Execute(TDto dto);
    }
}