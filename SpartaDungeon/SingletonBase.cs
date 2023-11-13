
namespace Framework
{
    public class SingletonBase<T> where T : class, new()
    {
        private static readonly object lockObject = new object();
        private static T? instance;

        public static T Instance
        {
            get
            {
                // 혹시 모를 다중 스레드에서 안전하게 인스턴스를 생성
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new T();
                }

                return instance;
            }
        }
    }
}