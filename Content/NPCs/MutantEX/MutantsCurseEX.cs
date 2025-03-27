using FargowiltasSouls;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.DataStructures;
using FargowiltasSouls.Content.Items;
using ssm.CrossMod.CraftingStations;

namespace ssm.Content.NPCs.MutantEX
{
    public class MutantsCurseEX : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 11));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        public override int NumFrames => 11;
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 48;
            Item.rare = ItemRarityID.Purple;
            Item.maxStack = 20;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }
        public override bool? UseItem(Player player)
        {
            FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<MutantEX>());
            return true;
        }

        public override bool CanUseItem(Player player) => player.Center.Y / 16 < Main.worldSurface;
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<PhantasmalEnergy>();
            recipe.AddIngredient<EternalEnergy>(30);
            recipe.AddIngredient<AbomEnergy>(30);
            recipe.AddIngredient<DeviatingEnergy>(30);
            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
