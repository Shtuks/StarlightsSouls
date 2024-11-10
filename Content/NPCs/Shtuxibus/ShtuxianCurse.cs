using ssm.Content.NPCs.Shtuxibus;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Core;
using ssm.Content.Items.Singularities.Calamity;

namespace ssm.Content.NPCs.Shtuxibus
{
    public class ShtuxianCurse : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.value = 100;
            Item.rare = -11;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup ItemGroup)
        {
            ItemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Shtuxibus>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int type = ModContent.NPCType<Shtuxibus>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod kal))
            {
                CreateRecipe()
                    .AddIngredient(kal.Find<ModItem>("ShadowspecBar").Type, 30)
                    .AddIngredient<EternalEnergy>(10)
                    .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))
                    .Register();
            }

            if (ModLoader.TryGetMod("SacredTools", out Mod soa))
            {
                CreateRecipe()
                    .AddIngredient(soa.Find<ModItem>("EmberOfOmen").Type, 30)
                    .AddIngredient<EternalEnergy>(10)
                    .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))
                    .Register();
            }

            if (ModLoader.TryGetMod("Redemption", out Mod red))
            {
                CreateRecipe()
                    .AddIngredient(red.Find<ModItem>("LifeFragment").Type, 30)
                    .AddIngredient<EternalEnergy>(10)
                    .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))
                    .Register();
            }
        }
    }
}