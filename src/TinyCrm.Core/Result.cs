namespace TinyCrm.Core
{
    public class Result<T>
    {
        public string ErrorMessage { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
        public int AppEventId { get; set; }
    }
}
