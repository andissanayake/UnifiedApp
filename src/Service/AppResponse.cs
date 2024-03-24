namespace Service
{
    public class AppResponse<T>
    {
        public bool IsSucceed { get; private set; } = true;
        public Dictionary<string, string[]> Messages { get; private set; } = [];

        public T? Data { get; private set; }
        public static AppResponse<T> SuccessResponse(T data)
        {
            return new AppResponse<T>() { Data = data, IsSucceed = true, };
        }
        public static AppResponse<T> SuccessResponse(T data, string key, string value)
        {
            var res = new AppResponse<T>() { Data = data, IsSucceed = true };
            res.Messages.Add(key, [value]);
            return res;
        }
        public static AppResponse<T> SuccessResponse(T data, Dictionary<string, string[]> message)
        {
            return new AppResponse<T>() { Data = data, IsSucceed = true, Messages = message };
        }
        public static AppResponse<T> SuccessResponse(T data, string key, string[] value)
        {
            var res = new AppResponse<T>() { Data = data, IsSucceed = true };
            res.Messages.Add(key, value);
            return res;
        }
        public static AppResponse<T> ErrorResponse(string key, string value)
        {
            var res = new AppResponse<T>() { Data = default, IsSucceed = false };
            res.Messages.Add(key, [value]);
            return res;
        }
        public static AppResponse<T> ErrorResponse(string key, string[] value)
        {
            var res = new AppResponse<T>() { Data = default, IsSucceed = false };
            res.Messages.Add(key, value);
            return res;
        }
        public static AppResponse<T> ErrorResponse(Dictionary<string, string[]> messages)
        {
            var res = new AppResponse<T>() { Data = default, IsSucceed = false, Messages = messages };
            return res;
        }
    }
}
