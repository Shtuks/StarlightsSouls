using Terraria;
using ssm.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using ssm.Content.Buffs;
using ssm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;
using Terraria.Localization;
using ssm.Content.Projectiles;
using Microsoft.Xna.Framework;
using ssm.Content.Buffs.Minions;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ssm.Content.Projectiles.Deathrays;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Projectiles.BossWeapons;


namespace ssm.Content.Items.Weapons
{
	public class UltimateFargoWeapon : ModItem
	{
		private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        private int forceSwordTimer;
        public bool flip;
		public override void SetDefaults()
		{
			Item.damage = 2500;
			Item.DamageType = DamageClass.Melee;
			Item.width = 160;
			Item.height = 160;
			Item.useTime = 27;
			Item.useStyle = 1;
			Item.knockBack = 7;
			Item.value = int.MaxValue;
			Item.rare = 11;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item113;
			Item.shoot = ModContent.ProjectileType<FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpear>();
            Item.shootSpeed = 30f;
		}
		public override bool CanUseItem(Player player)
		{
			Item.useTurn = false;
			if (player.altFunctionUse == 2)
			{
				if (player.controlUp)
				{
					this.Item.shoot = ModContent.ProjectileType<SparklingDevi>();
                    this.Item.useStyle = 1;
                    this.Item.DamageType = DamageClass.Summon;
                    this.Item.noUseGraphic = false;
                    this.Item.noMelee = false;
                    this.Item.useAnimation = 66;
                    this.Item.useTime = 66;
                    this.Item.mana = 100;
				}
				else
				{
                    this.Item.shootSpeed = 25f;
                    this.Item.useAnimation = 85;
                    this.Item.useTime = 85;
					Item.noMelee = true;
					Item.DamageType = DamageClass.Ranged;
					Item.useTime = 26;
                    Item.mana = 0;
					Item.shoot = ModContent.ProjectileType<HentaiSpearThrown>();
					Item.noUseGraphic = true;
					Item.useTurn = false;
				}
			}
			else
			{
				if (player.controlUp)
				{
					this.Item.shoot = ModContent.ProjectileType<StyxGazer>();
                    this.Item.useStyle = 5;
                    this.Item.DamageType = DamageClass.Magic;
                    this.Item.noUseGraphic = true;
                    this.Item.noMelee = true;
                    this.Item.mana = 200;
				}
				else
				{
                    this.Item.useAnimation = 16;
                    this.Item.useTime = 16;
                    this.Item.mana = 0;
                    this.Item.DamageType = DamageClass.Melee;
                    this.Item.shoot = ModContent.ProjectileType<HentaiSpearSpin>();
                    this.Item.shootSpeed = 1f;
                    this.Item.useTurn = true;
                }
			}
			return true;
            }	
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    public override void AddRecipes()
        {
            
        }
        public virtual bool Shoot(
        Player player,
        EntitySource_ItemUse_WithAmmo source,
        Vector2 position,
        Vector2 velocity,
        int type,
        int damage,
        float knockback){
        this.flip = !this.flip;
        if (player.altFunctionUse == 2 && player.controlUp)
        {
        velocity = Utils.RotatedBy(velocity, Math.PI / 2.0 * (this.flip ? 1.0 : -1.0), new Vector2());
        Projectile.NewProjectile((IEntitySource) source, position, velocity, type, damage, knockback, ((Entity) player).whoAmI, (float) (0.02617993950843811 * (this.flip ? -1.0 : 1.0)), 0.0f, 0.0f);
        }
        return false;}
}}