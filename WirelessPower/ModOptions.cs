using System;
using Newtonsoft.Json;

using PeterHan.PLib;
using PeterHan.PLib.Options;

namespace SonJeremy.WirelessPower
{
    [Serializable]
    [RestartRequired]
    [ConfigFile(IndentOutput: true, SharedConfigLocation: true)]
    public sealed class ModOptions : SingletonOptions<ModOptions>
    {
    }
}