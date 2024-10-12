using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using ssm.Core;
using ThoriumMod.Projectiles.Bard;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BardSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            //general
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            //player.GetDamage(DamageClass.Symp) += 0.3f;
            //thoriumPlayer.symphonicSpeed += .2f;
            //thoriumPlayer.symphonicCrit += 15;
            //thoriumPlayer.bardResourceMax2 += 20;

            //epic mouthpiece
            thoriumPlayer.accWindHoming = true;
            //thoriumPlayer.bardHomingBonus = 5f;

            //straight mute
            thoriumPlayer.accBrassMute2 = true;
            //digital tuner
            thoriumPlayer.accPercussionTuner2 = true;
            //guitar pick claw
            thoriumPlayer.bardBounceBonus = 5;
        }
    }
}
