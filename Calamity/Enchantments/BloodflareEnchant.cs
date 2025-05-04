using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Bloodflare;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using ssm.Content.SoulToggles;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class BloodflareEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod calamitybardhealer = ModLoader.GetMod("CalamityBardHealer");

        private readonly Mod ragnarok = ModLoader.GetMod("RagnarokMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(219, 18, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<AfflictionEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "Affliction").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<PhantomicEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "PhantomicArtifact").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BloodflareEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "BloodflareCore").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CruelSigilEffect>(Item))
            {
                ModContent.Find<ModItem>(this.ragnarok.Name, "SigilOfACruelWorld").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "BloodflareHeadMelee").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "BloodflareHeadRanged").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "BloodflareHeadMagic").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "BloodflareHeadRogue").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "BloodflareHeadSummon").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "BloodflareRitualistMask").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "BloodflareSirenSkull").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "BloodflareHeadBard").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "BloodflareHeadHealer").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "BloodflareBodyArmor").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "BloodflareCuisses").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareCuisses>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareCore>());
            recipe.AddIngredient(ModContent.ItemType<PhantomicArtifact>());
            recipe.AddIngredient(ModContent.ItemType<Affliction>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class AfflictionEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();
        }

        public class PhantomicEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();
        }
        public class BloodflareEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();
        }
        public class CruelSigilEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();
        }
    }
}
