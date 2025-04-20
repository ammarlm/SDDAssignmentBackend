namespace SDDAssignmentBackend.DTO
{
    public class GenResponse<T>
    {
        public T? Data { get; set; }
        //public Dictionary<string, string> Msg { get; set; }
        public string Msg { get; set; }
        public bool Success { get; set; }
    }
}
