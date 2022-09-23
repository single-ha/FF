using UnityEngine;

namespace Assets.Script
{
    public static class Debuger
    {
        public static void Log(object s)
        {
            Debug.Log(s);
        }

        public static void Log(object s,Object contex)
        {
            Debug.Log(s, contex);
        }
        public static void LogWarning(object s)
        {
            Debug.LogWarning(s);
        }

        public static void LogWarning(object s, Object contex)
        {
            Debug.LogWarning(s, contex);
        }

        public static void LogError(object s)
        {
            Debug.LogError(s);
        }

        public static void LogError(object s, Object contex)
        {
            Debug.LogError(s, contex);
        }
    }
}