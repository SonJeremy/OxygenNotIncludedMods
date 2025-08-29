using System;
using Newtonsoft.Json;

#if AskUsePLib
using PeterHan.PLib;
using PeterHan.PLib.Options;

#endif
namespace SonJeremy.ModTemplate
{
    #if AskUsePLib
    [Serializable]
    [RestartRequired]
    [ConfigFile(IndentOutput: true, SharedConfigLocation: true)]
    public sealed class ModOptions : SingletonOptions<ModOptions>
    {
        
    }
    #else
    public sealed class ModOptions
    {
        
    }
    #endif
}