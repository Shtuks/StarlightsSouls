using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gunrightsmod;
using ssm.Core;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using gunrightsmod.Content.Items;
using gunrightsmod.Buffs;

namespace ssm.gunrightmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class PlasticEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.gunrightsmod;
        }

        private readonly Mod gunrightsmod = ModLoader.GetMod("gunrightsmod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 200000;
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<PlasticEnchant>()))
            {
                if (Main.rand.NextFloat() < 0.2f)
                {
                    target.AddBuff(ModContent.BuffType<MicroplasticPoisoning>(), 180);
                }

        public override Color nameColor => new(255, 128, 0);

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<PlasticHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<PlasticChestplate>());
            recipe.AddIngredient(ModContent.ItemType<PlasticLeggings>());
            recipe.AddIngredient(ModContent.ItemType<PlasticSpork>());
            recipe.AddIngredient(ModContent.ItemType<BrickPick>());
            recipe.AddIngredient(ModContent.ItemType<NerfGun>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
