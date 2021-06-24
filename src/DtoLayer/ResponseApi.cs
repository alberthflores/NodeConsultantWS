namespace DtoLayer
{
    public class ResponseApi<T>
    {
        public int StatusCode { get; set; }
        public T Content { get; set; }
        public string Message { get; set; }
        public ResponseApi(T content)
        {
            StatusCode = 200;
            Content = content;
            Message = "OK";
        }
    }
}
