using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gunrightsmod.Items;
using ssm.Core;
using Terraria.DataStructures;
using gunrightsmod.Textures;

namespace ssm.gunrightmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class UraniumEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.gunrightsmod;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<UraniumAuraEffect>().auraActive = true;
        }

        public class UraniumAuraEffect : ModPlayer
        {
            public bool auraActive;

            public override void ResetEffects()
            {
                auraActive = false;
            }

            public override void PostUpdate()
            {
                if (!auraActive)
                    return;

                int radius = 160;
                int damage = (int)(50 * Player.GetDamage(DamageClass.Generic).Additive);

                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Distance(Player.Center) < radius)
                    {
                        npc.StrikeNPCNoInteraction(damage, 0f, 0, false, false, false);
                    }
                }
            }

            public override void PostDraw()
            {
                if (!auraActive)
                    return;

                Texture2D texture = ModContent.Request<Texture2D>("gunrightsmod/Textures/UraniumAuraCircle").Value;
                Vector2 position = Player.Center - Main.screenPosition;
                Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

                Main.spriteBatch.Draw(
                    texture,
                    position,
                    null,
                    Color.Green * 0.3f,
                    0f,
                    origin,
                    1f,
                    SpriteEffects.None,
                    0f
                );

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<UraniumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<UraniumChestplate>());
            recipe.AddIngredient(ModContent.ItemType<UraniumLeggings>());
            recipe.AddIngredient(ModContent.ItemType<UraniumSword>());
            recipe.AddIngredient(ModContent.ItemType<PlasmoidWand>());
            recipe.AddIngredient(ModContent.ItemType<ParticleGun>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }

}
