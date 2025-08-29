using System.Reflection;

namespace SonJeremy.SModUtil.OxygenNotIncluded
{
    public static class SExtension
    {
        public static string GetNameSafe(this Assembly CalledAssembly) => CalledAssembly?.GetName()?.Name ?? "Unknown";
    }
}