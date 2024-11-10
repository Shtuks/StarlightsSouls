using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;

namespace ssm.Content.Tiles
{
    public class ShtuxiumOreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileOreFinderPriority[Type] = 10000; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(0, 255, 0), name);

            DustType = 84;
            HitSound = SoundID.Tink;
            MineResist = 10f;
            MinPick = 900;
        }

        // Shtuxium of how to enable the Biome Sight buff to highlight this tile. Biome Sight is technically intended to show "infected" tiles, so this Shtuxium is purely for demonstration purposes.
        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.Green;
            return true;
        }
    }
    public class ShtuxiumOreSystem : ModSystem
    {
        public static LocalizedText ShtuxiumOrePassMessage { get; private set; }
        public static LocalizedText BlessedWithShtuxiumOreMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            ShtuxiumOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(ShtuxiumOrePassMessage)}");
            BlessedWithShtuxiumOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithShtuxiumOreMessage)}");
        }

        // This method is called from Shtuxibus.OnKill the first time the boss is killed.
        // The logic is located here for organizational purposes.
        public void BlessWorldWithShtuxiumOre()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (Main.netMode == NetmodeID.SinglePlayer) { Main.NewText(BlessedWithShtuxiumOreMessage.Value, 0, 255, 0); }
                else if (Main.netMode == NetmodeID.Server) { ChatHelper.BroadcastChatMessage(BlessedWithShtuxiumOreMessage.ToNetworkText(), new Color(0, 255, 0)); }
                int splotches = 100;
                int highestY = (int)Utils.Lerp(Main.rockLayer, Main.UnderworldLayer, 0.5);
                for (int iteration = 0; iteration < splotches; iteration++)
                {
                    int i = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                    int j = WorldGen.genRand.Next(highestY, Main.UnderworldLayer);
                    WorldGen.OreRunner(i, j, WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(5, 9), (ushort)ModContent.TileType<ShtuxiumOreTile>());
                }
            });
        }

        public class ShtuxiumOrePass : GenPass
        {
            public ShtuxiumOrePass(string name, float loadWeight) : base(name, loadWeight)
            {
            }
            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = ShtuxiumOreSystem.ShtuxiumOrePassMessage.Value;
                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY);
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<ShtuxiumOreTile>());
                }
            }
        }
    }
}
