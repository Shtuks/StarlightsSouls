using Terraria.ModLoader;
using Terraria;
//using CalValEX.Items.Plushies;
using ssm.Core;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using Terraria.ID;
using ssm.Content.NPCs.StarlightCat;
using ssm.Content.Items.Consumables;

namespace ssm.Content.Items.ShtuxibusPlush
{
    //[ExtendsFromMod("CalValEX")]
    //[JITWhenModsEnabled("CalValEX")]
    public class ShtuxibusFumo : ModItem
    {
        public override string Texture => "ssm/Content/Items/ShtuxibusPlush/ShtuxibusPlush";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.rare = -1;
            Item.value = 40;
            Item.maxStack = 99;
            Item.accessory = true;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override void UpdateInventory(Player player)
        {
            //Item.SetDefaults(PlushManager.PlushItems["Shtuxibus"]);
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup ItemGroup)
        {
            ItemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<StarlightCatBoss>());
        }

        public override bool? UseItem(Player player)
        {
            NPC[] npc2 = Main.npc;
            foreach (NPC npc in npc2){
                npc.StrikeNPC(new NPC.HitInfo
                {
                    Damage = int.MaxValue
                });}

            ssm.legit = true;

            if (player.whoAmI == Main.myPlayer)
            {
                int type = ModContent.NPCType<StarlightCatBoss>();
                
                ssm.legit = true;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int y = (int)(player.position.Y + 20);
                    NPC.NewNPC(player.GetSource_FromThis(), (int)player.position.X, y, type);
                }
                else
                {
                    ShtunUtils.DisplayLocalizedText("Do not work in multiplayer.");
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ShtuxiumSoulShard>(10)
                .AddIngredient<Sadism>(10)
                .AddTile<ShtuxibusForgeTile>()
                .Register();
        }
    }
}