using CalamityMod.Cooldowns;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using ssm.Core;


namespace ssm.CooldownIndicators
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class ShtuxianGodhoodCooldown : CooldownHandler
    {
        public new static string ID => "ShtuxianGodhood";

        public override bool ShouldDisplay => true;

        public override string Texture => "ssm/Render/CooldownIndicators/ShtuxianGodhood";

        public override Color OutlineColor => new Color((int)byte.MaxValue, 194, 252);

        public override Color CooldownStartColor => new Color(0, 254, 105);

        public override Color CooldownEndColor => new Color(0, 187, 105);
    }
}