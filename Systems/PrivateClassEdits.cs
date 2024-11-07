using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using ssm.Content.NPCs.StarlightCat;

namespace ssm.Systems
{
    public class HookEdit
    {
        public bool loaded = false;

        public void Load()
        {
            if (!loaded)
            {
                LoadInternal();
                loaded = true;
            }
        }
        public void Unload()
        {
            if (loaded)
            {
                UnloadInternal();
                loaded = false;
            }
        }

        protected virtual void LoadInternal() { }
        protected virtual void UnloadInternal() { }
        internal HookEdit()
        {
            Load();
        }
    }

    internal class PrivateClassEdits
    {
        internal static List<HookEdit> hooksList;

        internal static void ApplyPatches()
        {
            hooksList = new List<HookEdit>();

            Assembly ass = ssm.Instance.Code;
            foreach (Type typeoff in ass.GetTypes())
            {
                Type hooktype = typeof(HookEdit);
                if (typeoff != hooktype && typeoff.IsSubclassOf(hooktype))
                {
                    HookEdit instancedHook = (ass.CreateInstance(typeoff.FullName) as HookEdit);
                    hooksList.Add(instancedHook);
                }
            }
        }

        internal static void RemovePatches()
        {
            foreach (HookEdit hook in hooksList)
            {
                hook.Unload();
            }
        }


        public static void LoadAntiCheats()
        {
            var _ = godmodePatch;
        }

        internal static bool godmodePatch
        {
            get
            {
                if (ModCompatibility.CheatSheet.Loaded)
                {
                    Assembly assybcl = ModCompatibility.CheatSheet.Mod.GetType().Assembly;
                    string typeofit = "";
                    Type godModeCS = null;

                    foreach (Type typea in assybcl.GetTypes())
                    {
                        if (typea.Name == "GodMode")
                            godModeCS = typea;
                    }
                }

                if (ModCompatibility.HEROSMod.Loaded)
                {
                    Assembly assybcl = ModCompatibility.HEROSMod.Mod.GetType().Assembly;
                    string typeofit = "";
                    Type godModeCS = null;

                    foreach (Type typea in assybcl.GetTypes())
                    {
                        if (typea.Name == "GodModeService")
                            godModeCS = typea;
                    }

                    StarlightCatBoss.CSGodmodeOn = godModeCS.GetProperty("Enabled", ssm.UniversalBindingFlags).GetMethod;
                }

                return false;
            }
        }

        private delegate bool orig_CSGodModeDetour();
        private delegate bool hook_CSGodModeDetour(orig_CSGodModeDetour orig);
    }
}
