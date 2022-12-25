namespace Utils
{
    public abstract class Singleton<T>  where T : class, new()
    {
        protected static T Instance { get; private set; }

        public static void Initialize()
        {
            Instance = new T();
        }
    }
}