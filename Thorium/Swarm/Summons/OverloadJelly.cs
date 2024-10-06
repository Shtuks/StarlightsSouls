//using Fargowiltas.NPCs;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.ID;
//using Terraria.Localization;
//using Terraria.ModLoader;

//namespace Fargowiltas.Items.Summons.SwarmSummons.Thorium
//{
//    public class OverloadJelly : ModItem
//    {
//        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Jellyfish Resonator 2.0");
//            Tooltip.SetDefault("Summons several Queen Jellyfish");
//        }

//        public override bool Autoload(ref string name)
//        {
//            return ModLoader.GetMod("ThoriumMod") != null;
//        }

//        public override void SetDefaults()
//        {
//            item.width = 20;
//            item.height = 20;
//            item.maxStack = 100;
//            item.value = 1000;
//            item.rare = 1;
//            item.useAnimation = 30;
//            item.useTime = 30;
//            item.useStyle = 4;
//            item.consumable = true;
//        }

//        public override bool CanUseItem(Player player)
//        {
//            return !Fargowiltas.SwarmActive;
//        }

//        public override bool UseItem(Player player)
//        {
//            Fargowiltas.SwarmActive = true;
//            Fargowiltas.SwarmTotal = 10 * player.inventory[player.selectedItem].stack;
//            Fargowiltas.SwarmKills = 0;

//            // Kill whole stack
//            player.inventory[player.selectedItem].stack = 0;

//            if (Fargowiltas.SwarmTotal <= 20)
//            {
//                Fargowiltas.SwarmSpawned = Fargowiltas.SwarmTotal;
//            }
//            else if (Fargowiltas.SwarmTotal <= 100)
//            {
//                Fargowiltas.SwarmSpawned = 20;
//            }
//            else if (Fargowiltas.SwarmTotal != 1000)
//            {
//                Fargowiltas.SwarmSpawned = 50;
//            }
//            else
//            {
//                Fargowiltas.SwarmSpawned = 60;
//            }

//            for (int i = 0; i < Fargowiltas.SwarmSpawned; i++)
//            {
//                int boss = NPC.NewNPC((int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), thorium.NPCType("QueenJelly"));
//                Main.npc[boss].GetGlobalNPC<FargoGlobalNPC>().SwarmActive = true;
//            }

//            if (Main.netMode == 2)
//            {
//                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("National Jelly Day has arrived!"), new Color(175, 75, 255));
//            }
//            else
//            {
//                Main.NewText("National Jelly Day has arrived!", 175, 75, 255);
//            }

//            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
//            return true;
//        }

//        public override void AddRecipes()
//        {
//            ModRecipe recipe = new ModRecipe(mod);
//            recipe.AddIngredient(thorium, "JellyfishResonator");
//            recipe.AddIngredient(null, "Overloader");
//            recipe.AddTile(TileID.DemonAltar);
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }
//    }
//}