using System;
using System.Reflection;

namespace SonJeremy.SModUtil.OxygenNotIncluded
{
    public static class SUtil
    {
        public static readonly string NewLine = Environment.NewLine;
        public static void LogDebug(object Message) 
            => Debug.LogFormat("[{0}] {1}", Assembly.GetCallingAssembly().GetNameSafe(), Message);
        
        public static void LogError(object Message)
            => Debug.LogErrorFormat("[{0}] {1}", Assembly.GetCallingAssembly().GetNameSafe(), Message);
        
        public static void LogWarning(object Message)
            => Debug.LogWarningFormat("[{0}] {1}", Assembly.GetCallingAssembly().GetNameSafe(), Message);
        
        public static void LogException(Exception ThrownException)
        => Debug.LogErrorFormat(
            "[{0}] {1} {2} {3}", Assembly.GetCallingAssembly().GetNameSafe(), 
            ThrownException.GetType(), ThrownException.Message, ThrownException.StackTrace);
    }
}