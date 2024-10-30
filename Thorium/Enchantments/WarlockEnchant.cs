using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.NPCs;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Tracker;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class WarlockEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //set bonus
            thoriumPlayer.warlockSet = true;

            //demon tongue
            thoriumPlayer.darkAura = true;
            thoriumPlayer.radiantLifeCost = 2;

            //dark effigy
            //for (int i = 0; i < 200; i++)
            //{
            //    NPC npc = Main.npc[i];
            //    if (npc.active && !npc.friendly && (npc.shadowFlame || npc.GetGlobalNPC<ThoriumGlobalNPC>().lightLament) && npc.DistanceSQ(player.Center) < 1000000f)
            //    {
            //        thoriumPlayer.effigy++;
            //    }
            //}
            //if (thoriumPlayer.effigy > 0)
            //{
            //    player.AddBuff(thorium.BuffType("EffigyRegen"), 2, true);
            //}
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WarlockHood>());
            recipe.AddIngredient(ModContent.ItemType<WarlockGarb>());
            recipe.AddIngredient(ModContent.ItemType<WarlockLeggings>());
            recipe.AddIngredient(ModContent.ItemType<EbonEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DemonTongue>());
            recipe.AddIngredient(ModContent.ItemType<DarkEffigy>());


            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
