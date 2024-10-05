using ssm.Content.NPCs.Shtuxibus;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Materials;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.Content.NPCs.Shtuxibus
{
    // This is the item used to summon a boss, in this case the modded Minion Boss from Example Mod. For vanilla boss summons, see comments in SetStaticDefaults
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

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
        }

        public override bool CanUseItem(Player player)
        {
            // If you decide to use the below UseItem code, you have to include !NPC.AnyNPCs(id), as this is also the check the server does when receiving MessageID.SpawnBoss.
            // If you want more constraints for the summon item, combine them as boolean expressions:
            //    return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<MinionBossBody>()); would mean "not daytime and no MinionBossBody currently alive"
            return !NPC.AnyNPCs(ModContent.NPCType<Shtuxibus>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int type = ModContent.NPCType<Shtuxibus>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the player is not in multiplayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    // If the player is in multiplayer, request a spawn
                    // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            this.CreateRecipe(1)
            .AddIngredient<ShadowspecBar>(10)
          .AddIngredient<MiracleMatter>(10)
          .AddIngredient<EternalEnergy>(10)
          .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
        }
    }
}