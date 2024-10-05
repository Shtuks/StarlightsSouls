using Fargowiltas.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Fargowiltas.Items.Summons.SwarmSummons;

namespace ssm.Content.Items.Swarm.Summons
{
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
                Main.npc[boss].GetGlobalNPC<ShtunNpcs>().SwarmActive = true;
            }

            // Kill whole stack
            player.inventory[player.selectedItem].stack = 0;

            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }
}