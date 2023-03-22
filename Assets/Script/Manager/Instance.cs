public class Instance<T> where T : new()
{
    private static T _inst;

    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = new T();
            }

            return _inst;
        }
    }
}