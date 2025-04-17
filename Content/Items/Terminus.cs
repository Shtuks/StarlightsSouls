using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;

namespace ssm.Items;
[ExtendsFromMod(ModCompatibility.Calamity.Name)]
[JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
[LegacyName(new string[] { "BossRush" })]
public class ShtunTerminus : ModItem, ILocalizedModType, IModType
{
    public override string Texture => "CalamityMod/Items/SummonItems/Terminus";
    public override void SetDefaults()
    {
        Item.width = (Main.zenithWorld ? 54 : 28);
        Item.height = (Main.zenithWorld ? 78 : 28);
        Item.rare = 1;
        Item.useAnimation = 45;
        Item.useTime = 45;
        Item.channel = true;
        Item.noUseGraphic = true;

        if (!ModCompatibility.WrathoftheGods.Loaded)
        {
            Item.shoot = ModContent.ProjectileType<TerminusHoldout>();
        }
        else
        {
            ModCompatibility.WrathoftheGods.Mod.TryFind<ModProjectile>("TerminusProj", out ModProjectile terminusProj);
            Item.shoot = terminusProj.Type;
        }

        Item.useStyle = 4;
        Item.consumable = false;
    }

    public override void UpdateInventory(Player player)
    {
        if (Main.zenithWorld)
        {
            Item.SetNameOverride(this.GetLocalizedValue("GFBName"));
        }
    }

    public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frameI, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        if (Main.zenithWorld)
        {
            Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/Terminus_GFB").Value;
            Color white = Color.White;
            spriteBatch.Draw(value, position, null, white, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
        }

        return true;
    }

    public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        if (Main.zenithWorld)
        {
            Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/Terminus_GFB").Value;
            spriteBatch.Draw(value, Item.position - Main.screenPosition, null, lightColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            return false;
        }

        return true;
    }

    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.EventItem;
    }
}