using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ssm.Core;
using ssm.Calamity;


namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    
    /*
        public abstract class SwarmSummonBase : ModItem
        {
            //wof only
            private int counter = 0;

            private int npcType;
            private readonly int maxSpawn; //energizer swarms are this size

            protected SwarmSummonBase(int npcType, int maxSpawn)
            {
                this.npcType = npcType;
                this.maxSpawn = maxSpawn;
            }

            public override void SetDefaults()
            {
                Item.width = 20;
                Item.height = 20;
                Item.maxStack = 100;
                Item.value = 10000;
                Item.rare = ItemRarityID.Blue;
                Item.useAnimation = 30;
                Item.useTime = 30;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.consumable = true;

                if (npcType == NPCID.WallofFlesh)
                {
                    Item.useAnimation = 20;
                    Item.useTime = 2;
                    Item.consumable = false;
                }
            }

            public override bool? UseItem(Player player)
            {
                ssm.SwarmActive = true;
                ssm.SwarmTotal = 10 * player.inventory[player.selectedItem].stack;
                ssm.SwarmKills = 0;

                if (ssm.SwarmTotal < 100)
                {
                    ssm.SwarmSpawned = 10;
                }
                else
                {
                    //energizer swarms
                    ssm.SwarmSpawned = maxSpawn;
                }

                //spawn the bosses
                for (int i = 0; i < ssm.SwarmSpawned; i++)
                {
                    int boss = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), npcType);
                    Main.npc[boss].GetGlobalNPC<CalNpcs>().SwarmActive = true;
                }

                // Kill whole stack
                player.inventory[player.selectedItem].stack = 0;

                SoundEngine.PlaySound(SoundID.Roar, player.position);
                return true;
            }
        }
    }*/

    public abstract class SwarmSummonBase : ModItem
    {
        private int counter;

        private int npcType;

        private readonly int maxSpawn;

        private int material;

        protected SwarmSummonBase(int npcType, int maxSpawn, int material)
        {
            this.npcType = npcType;
            this.maxSpawn = maxSpawn;
            this.material = material;
        }

        public override void SetDefaults()
        {
            base.Item.width = 20;
            base.Item.height = 20;
            base.Item.maxStack = 100;
            base.Item.value = 10000;
            base.Item.rare = 1;
            base.Item.useAnimation = 30;
            base.Item.useTime = 30;
            base.Item.useStyle = 5;
            base.Item.consumable = true;
            if (this.npcType == 113)
            {
                base.Item.useAnimation = 20;
                base.Item.useTime = 2;
                base.Item.consumable = false;
            }
        }

        public override bool? UseItem(Player player)
        {
            ssm.SwarmSetDefaults = true;
            ssm.SwarmActive = true;
            ssm.SwarmItemsUsed = player.inventory[player.selectedItem].stack;
            ssm.SwarmNoHyperActive = ssm.SwarmItemsUsed < 5;

            int num = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), this.npcType);
            Main.npc[num].GetGlobalNPC<CalNpcs>().SwarmActive = true;

            //if (this.npcType == 125)
            //{
            //    int num2 = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), 126);
            //    Main.npc[num2].GetGlobalNPC<CalNpcs>().SwarmActive = true;
            //}
            //else
            //{
            //    _ = this.npcType;
            //    _ = 134;
            //}

            player.inventory[player.selectedItem].stack = 0;

            //if (Main.netMode == 2)
            //{
            //    ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Fargowiltas.MessageInfo." + this.spawnMessageKey), new Color(175, 75, 255));
            //    NetMessage.SendData(7);
            //}
            //else if (Main.netMode == 0)
            //{
            //    Main.NewText(Language.GetTextValue("Mods.Fargowiltas.MessageInfo." + this.spawnMessageKey), 175, 75);
            //}

            SoundEngine.PlaySound(in SoundID.Roar, player.position);
            ssm.SwarmSetDefaults = false;
            return true;
        }

        public override void AddRecipes()
        {
            base.CreateRecipe()
                .AddIngredient(this.material)
                .AddIngredient(ModCompatibility.MutantMod.Mod, "Overloader")
                .AddTile(26)
                .Register();
        }
    }
}
