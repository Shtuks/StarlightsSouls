using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Core
{
    internal sealed class ColoredDamageTypesSupport : CrossModHandler
    {
        internal interface IDamageColor
        {
            Color DamageColor { get; }

            Color CritDamageColor
            {
                get
                {
                    Vector3 hslVector = Main.rgbToHsl(DamageColor);
                    hslVector.Y = MathHelper.Lerp(hslVector.Y, 1f, 0.6f);
                    return Main.hslToRgb(hslVector);
                }
            }

            Color TooltipColor => DamageColor;
        }

        protected override string ModName => "ColoredDamageTypes";

        internal override void PostSetupContent()
        {
            foreach (DamageClass item in from d in Mod.GetContent<DamageClass>()
                                         where d is IDamageColor
                                         select d)
            {
                IDamageColor damageColor = item as IDamageColor;
                AddDamageType(item, damageColor.TooltipColor, damageColor.DamageColor, damageColor.CritDamageColor);
            }
        }

        private void AddDamageType(DamageClass damageType, Color tooltipColor, Color damageColor, Color critDamageColor)
        {
            CrossMod.Call("AddDamageType", damageType, tooltipColor, damageColor, critDamageColor);
        }
    }
}