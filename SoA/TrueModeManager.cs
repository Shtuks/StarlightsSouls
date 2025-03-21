using SacredTools.Common.Systems;
using SacredTools.Items.Tools;
using ssm.Core;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public static class TrueModeManager
    {
        public static void setTrueMode(bool state)
        {
            Type trueModeType = typeof(TrueModeSystem);

            FieldInfo trueModeField = trueModeType.GetField(
                "<TrueMode>k__BackingField",
                BindingFlags.Static | BindingFlags.Public
            );

            if (trueModeField != null)
            {
                trueModeField.SetValue(null, state);
            }
            else
            {
                throw new Exception("TrueMode field not found!");
            }
        }
    }
}
