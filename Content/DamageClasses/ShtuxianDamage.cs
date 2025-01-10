using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.DamageClasses
{
    public class ShtuxianDamage : DamageClass
    {
        internal static ShtuxianDamage Instance;

        //Color ColoredDamageTypesSupport.IDamageColor.DamageColor => new Color(255, 100, 100);
        public override void Load() => Instance = this;
        public override void Unload() => Instance = null;

        public string NameText => Language.GetTextValue("Mods.ssm.ShtuxiamDamageTextContent.ShtuxianDamage.DisplayName");

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass) {
            if (damageClass == DamageClass.Generic)
            {
                return StatInheritanceData.Full;
            }
            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
            {
                return true;
            }
            return false;
        }
    }
}
