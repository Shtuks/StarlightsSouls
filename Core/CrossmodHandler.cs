using System.Collections.Generic;
using Terraria.ModLoader;

namespace ssm.Core
{
    internal abstract class CrossModHandler : ILoadable
    {
        protected Mod Mod { get; private set; }

        protected Mod CrossMod
        {
            get
            {
                if (!ModLoader.TryGetMod(ModName, out var result))
                {
                    return null;
                }

                return result;
            }
        }

        internal bool IsModLoaded => CrossMod != null;

        protected abstract string ModName { get; }

        internal virtual void OnModLoad()
        {
        }

        internal virtual void SetupContent()
        {
        }

        internal virtual void PostSetupContent()
        {
        }

        internal virtual void PostSetupEverything()
        {
        }

        void ILoadable.Load(Mod mod)
        {
            Mod = mod;
            CrossModSystem._handlers.Add(this);
        }

        void ILoadable.Unload()
        {
        }
    }

    public sealed class CrossModSystem : ModSystem
    {
        internal static readonly List<CrossModHandler> _handlers = new List<CrossModHandler>();

        public override void OnModLoad()
        {
            foreach (CrossModHandler handler in _handlers)
            {
                if (handler.IsModLoaded)
                {
                    handler.OnModLoad();
                }
            }
        }

        public override void SetupContent()
        {
            foreach (CrossModHandler handler in _handlers)
            {
                if (handler.IsModLoaded)
                {
                    handler.SetupContent();
                }
            }
        }

        public override void PostSetupContent()
        {
            foreach (CrossModHandler handler in _handlers)
            {
                if (handler.IsModLoaded)
                {
                    handler.PostSetupContent();
                }
            }
        }

        public override void PostAddRecipes()
        {
            foreach (CrossModHandler handler in _handlers)
            {
                if (handler.IsModLoaded)
                {
                    handler.PostSetupEverything();
                }
            }
        }

        public override void Unload()
        {
            _handlers.Clear();
        }
    }
}