using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using ssm.Content.Projectiles.Weapons;
using ssm.CrossMod.CraftingStations;

namespace ssm.Content.Items.Weapons.Shtuxium
{
    public class ShtuxiumKunai : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.damage = 4200;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.crit = 50;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(copper: 100);
            Item.rare = ItemRarityID.Purple;
            Item.shoot = ModContent.ProjectileType<ShtuxiumKunaiProj>();
            Item.shootSpeed = 22f;
            Item.DamageType = DamageClass.Throwing;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float knifeSpeed = Item.shootSpeed;
            Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, true);
            float mouseXDist = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
            float mouseYDist = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
            if (player.gravDir == -1f)
            {
                mouseYDist = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - realPlayerPos.Y;
            }
            float mouseDistance = (float)Math.Sqrt((double)(mouseXDist * mouseXDist + mouseYDist * mouseYDist));
            if ((float.IsNaN(mouseXDist) && float.IsNaN(mouseYDist)) || (mouseXDist == 0f && mouseYDist == 0f))
            {
                mouseXDist = (float)player.direction;
                mouseYDist = 0f;
                mouseDistance = knifeSpeed;
            }
            else
            {
                mouseDistance = knifeSpeed / mouseDistance;
            }
            mouseXDist *= mouseDistance;
            mouseYDist *= mouseDistance;
            int knifeAmt = 6;
            if (Main.rand.NextBool())
            {
                knifeAmt++;
            }
            if (Main.rand.NextBool(4))
            {
                knifeAmt++;
            }
            if (Main.rand.NextBool(8))
            {
                knifeAmt++;
            }
            if (Main.rand.NextBool(16))
            {
                knifeAmt++;
            }
            for (int i = 0; i < knifeAmt; i++)
            {
                float knifeSpawnXPos = mouseXDist;
                float knifeSpawnYPos = mouseYDist;
                float randOffsetDampener = 0.05f * (float)i;
                knifeSpawnXPos += (float)Main.rand.Next(-25, 26) * randOffsetDampener;
                knifeSpawnYPos += (float)Main.rand.Next(-25, 26) * randOffsetDampener;
                mouseDistance = (float)Math.Sqrt((double)(knifeSpawnXPos * knifeSpawnXPos + knifeSpawnYPos * knifeSpawnYPos));
                mouseDistance = knifeSpeed / mouseDistance;
                knifeSpawnXPos *= mouseDistance;
                knifeSpawnYPos *= mouseDistance;
                float x4 = realPlayerPos.X;
                float y4 = realPlayerPos.Y;
                Projectile.NewProjectile(source, x4, y4, knifeSpawnXPos, knifeSpawnYPos, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
        }


        public override void AddRecipes()
        {
            CreateRecipe(100).
                AddIngredient<ShtuxiumBar>(1).
                AddTile<MutantsForgeTile>().
                Register();
        }
    }
}
