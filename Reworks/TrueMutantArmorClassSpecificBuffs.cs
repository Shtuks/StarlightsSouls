using BombusApisBee.BeeDamageClass;
using ClickerClass.Utilities;
using FargowiltasSouls;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Utilities;

namespace ssm.Reworks
{
    public class TrueMutantArmorClassSpecificBuffs : GlobalItem
    {
        public override void UpdateEquip(Item Item, Player player)
        {
            if (player.FargoSouls().MutantSetBonusItem != null)
            {
                player.Shtun().throwerVelocity += 0.2f;
                if (ModCompatibility.Thorium.Loaded) { BardAndHealer(player, 20); }
                if (ModCompatibility.ClikerClass.Loaded) { Clicker(player, 0.5f); }
                if (ModCompatibility.BeekeeperClass.Loaded) { Beekeeper(player, 30); }
            }

            if (player.FargoSouls().StyxSet == true)
            {
                player.Shtun().throwerVelocity += 0.1f;
                if (ModCompatibility.Thorium.Loaded) { BardAndHealer(player, 10); }
                if (ModCompatibility.ClikerClass.Loaded) { Clicker(player, 0.25f); }
                if (ModCompatibility.BeekeeperClass.Loaded) { Beekeeper(player, 15); }
            }
        }

        [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
        public void BardAndHealer(Player player, int bonus)
        {
            player.GetThoriumPlayer().healBonus += bonus;
            player.GetThoriumPlayer().bardResourceMax2 += bonus;
        }

        [JITWhenModsEnabled(ModCompatibility.BeekeeperClass.Name)]
        public void Beekeeper(Player player, int bonus)
        {
            player.GetModPlayer<BeeDamagePlayer>().BeeResourceMax2 += bonus;
        }

        [JITWhenModsEnabled(ModCompatibility.ClikerClass.Name)]
        public void Clicker(Player player, float bonus)
        {
            player.GetClickerPlayer().clickerBonusPercent += bonus;
        }
    }
}
