using ssm.Content.NPCs.Shtuxibus;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Core;
using ssm.Content.Tiles;
using ssm.Content.Items.Materials;

namespace ssm.Content.NPCs.Chtuxlagor
{
    public class ShtuxianCurseEX : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
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
            return !NPC.AnyNPCs(ModContent.NPCType<Echdeath>());
        }

        public override bool? UseItem(Player player)
        {
            if (ShtunConfig.Instance.ExtraContent)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    int type = ModContent.NPCType<Echdeath>();

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, type);
                    }
                    else
                    {
                        NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                    }
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ShtuxiumSoulShard>(10)
                .AddIngredient<EternalEnergy>(10)
                .AddTile<ShtuxibusForgeTile>()
                .Register();
        }
    }
}