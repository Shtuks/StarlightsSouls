using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ssm.Content.Buffs;
using ssm;
using FargowiltasSouls.Content.Items;
using Terraria.Audio;
using Terraria.Localization;
using CalamityMod.CalPlayer;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ssm.Content.Projectiles.Deathrays;
using FargowiltasSouls;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using ssm.Content.Projectiles.Weapons;
using ssm.Content.Projectiles;
using ssm.Content.Projectiles.Deathrays;
using ssm.Content.Projectiles.Minions;
using ssm.Content.Buffs.Minions;
using Luminance.Core.Graphics;

namespace ssm.Content.Items.Weapons
{
	public class ShtuxibusSword : SoulsItem
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        public override void SetStaticDefaults()
        {
            //Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 10));
            //ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 17000;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.shootSpeed = 6f;
            Item.knockBack = 7f;
            Item.width = 72;
            Item.height = 72;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.Find<ModProjectile>(fargosouls.Name, "HentaiSpear").Type;
            Item.value = Item.sellPrice(745, 0, 0, 0);
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override bool AltFunctionUse(Player player) => true;

        int forceSwordTimer;

        public override bool CanUseItem(Player player)
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTurn = false;

            if (forceSwordTimer > 0)
            {
                Item.shoot = ModContent.ProjectileType<ShtuxibusSwordAttack>();
                Item.shootSpeed = 6f;

                Item.useAnimation = 16;
                Item.useTime = 16;

                Item.useStyle = ItemUseStyleID.Swing;
                Item.DamageType = DamageClass.Melee;
            }
            else if (player.altFunctionUse == 2 && !player.controlUp)
            {
                {
                    Item.DamageType = DamageClass.Ranged;
                    Item.shoot = ModContent.ProjectileType<HentaiSpearThrown>();
                    Item.shootSpeed = 20f;
                    Item.useAnimation = 85;
                    Item.useTime = 85;
                }
            }
            else if (player.altFunctionUse == 2 && player.controlDown)
            {
                Item.DamageType = DamageClass.Magic;
                //Item.shoot = ModContent.ProjectileType<ShtuxibusSoulMinion>();
                Item.shootSpeed = 1f;
                Item.useAnimation = 16;
                Item.useTime = 16;
                Item.useTurn = true;
            }
            else if (player.altFunctionUse == 2 && player.controlUp)
            {
                Item.DamageType = DamageClass.Summon;
                Item.shoot = ModContent.ProjectileType<ShtuxibusSoulMinion>();
                Item.shootSpeed = 1f;
                Item.useAnimation = 16;
                Item.useTime = 16;
                Item.useTurn = true;
            }
            else
            {
                if (player.controlUp && !player.controlDown)
                {
                    Item.shoot = ModContent.ProjectileType<HentaiSpearSpin>();
                    Item.shootSpeed = 1f;
                    Item.useTurn = true;
                }
                else if (!player.controlUp && player.controlDown)
                {
                    Item.shoot = ModContent.ProjectileType<HentaiSpearSpin>();
                    Item.shootSpeed = 1f;
                    Item.useTurn = true;
                }
                else
                {
                    Item.shoot = ModContent.ProjectileType<ShtuxibusSwordAttack>();
                    Item.shootSpeed = 6f;
                    Item.useStyle = ItemUseStyleID.Swing;
                }

                Item.useAnimation = 16;
                Item.useTime = 16;

                Item.DamageType = DamageClass.Melee;
            }

            return true;
        }

        public override void UpdateInventory(Player player)
        {
            if (forceSwordTimer > 0)
                forceSwordTimer -= 1;

            if (player.ownedProjectileCounts[ModContent.ProjectileType<HentaiSword>()] > 0)
                forceSwordTimer = 3;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (forceSwordTimer > 0 || (player.altFunctionUse != 2 && !player.controlUp && !player.controlDown))
            {
                velocity = new Vector2(velocity.X < 0 ? 1 : -1, -1);
                velocity.Normalize();
                velocity *= HentaiSword.MUTANT_SWORD_SPACING;
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, -Math.Sign(velocity.X));
                return false;
            }
            else{return true;}}

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(28, 222, 28));
                shader.TrySetParameter("secondaryColor", new Color(168, 245, 168));
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
    }
}