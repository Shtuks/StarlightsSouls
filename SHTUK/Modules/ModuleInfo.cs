using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SteelSeries.GameSense;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SHTUK.Modules
{
    public readonly struct ModuleInfo
    {
        public readonly string Type;

        public readonly string Name;

        public readonly Action<Player> IfModule;

        public readonly Action<Player> IfUnmodule;

        public readonly Gradient Color;

        public ModuleInfo(string name, string type, Action<Player> ifModule, Action<Player> ifUnModule)
        {
            Name = name;
            Type = type;
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj.GetType() != typeof(ModuleInfo))
            {
                return false;
            }
            ModuleInfo other = (ModuleInfo)obj;
            return false;
        }
    }
    public static class ModuleHandler
    {
        public static List<ModuleInfo> Modules { get; private set; } = new List<ModuleInfo>();

        public static void RegisterModule(ModuleInfo Module)
        {
            Modules.Add(Module);
        }

        public static void UnregisterModule(ModuleInfo Module)
        {
            int index = Modules.FindIndex((ModuleInfo x) => x.Name == Module.Name);
            if (index > -1)
            {
                Modules.RemoveAt(index);
            }
        }

        public static ModuleInfo? GetModuleByName(string name)
        {
            ModuleInfo fetch = Modules.Find((ModuleInfo x) => x.Name == name);
            if (fetch.Equals(default(ModuleInfo)))
            {
                return null;
            }
            return fetch;
        }
    }
}