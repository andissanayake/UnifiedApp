namespace ApiTest
{
    //public class AppResponse<T>
    //{
    //    public bool IsSucceed { get; private set; } = true;
    //    public Dictionary<string, string[]> Messages { get; private set; } = [];

    //    public T? Data { get; private set; }
    //}

    //original class has private setters for AppResponse class due to that, creating new class
    //to serialize and deserialize purpose 
    public class AppResponse<T>
    {
        public bool IsSucceed { get; set; } = true;
        public Dictionary<string, string[]> Messages { get; set; } = [];

        public T? Data { get; set; }
    }
}
