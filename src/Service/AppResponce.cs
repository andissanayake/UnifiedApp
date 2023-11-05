namespace Service
{
    public class AppResponse<T>
    {
        public bool IsSucceed { get; private set; } = true;
        public Dictionary<string, string[]> Messages { get; private set; } = new Dictionary<string, string[]>();

        public T? Data { get; private set; }
        public AppResponse<T> SetSuccessResponce(T data)
        {
            Data = data;
            return this;
        }
        public AppResponse<T> SetSuccessResponce(T data, string key, string value)
        {
            Data = data;
            Messages.Add(key, [value]);
            return this;
        }
        public AppResponse<T> SetSuccessResponce(T data, Dictionary<string, string[]> message)
        {
            Data = data;
            Messages = message;
            return this;
        }
        public AppResponse<T> SetSuccessResponce(T data, string key, string[] value)
        {
            Data = data;
            Messages.Add(key, value);
            return this;
        }
        public AppResponse<T> SetErrorResponce(string key, string value)
        {
            IsSucceed = false;
            Messages.Add(key, [value]);
            return this;
        }
        public AppResponse<T> SetErrorResponce(string key, string[] value)
        {
            IsSucceed = false;
            Messages.Add(key, value);
            return this;
        }
        public AppResponse<T> SetErrorResponce(Dictionary<string, string[]> message)
        {
            IsSucceed = false;
            Messages = message;
            return this;
        }
    }
}
