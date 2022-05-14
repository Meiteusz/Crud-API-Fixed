namespace Models
{
    public class ResponseSingleData<T> : Response
    {
        public T Data { get; set; } = default(T);
    }
}
