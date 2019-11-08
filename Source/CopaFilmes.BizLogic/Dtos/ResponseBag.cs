namespace CopaFilmes.BizLogic.Dtos
{
    public class ResponseBag<T>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public T ObjectResponse { get; set; }
    }
}