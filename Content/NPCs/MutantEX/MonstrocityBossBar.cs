using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.NPCs.MutantEX
{
    public class MonstrocityBossBar : ModBossBar
    {
        private int bossHeadIndex = -1;
        public override Asset<Texture2D> GetIconTexture(ref Microsoft.Xna.Framework.Rectangle? iconFrame)
        {
            if (bossHeadIndex != -1)
            {
                return TextureAssets.NpcHeadBoss[bossHeadIndex];
            }
            return null;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, NPC npc, ref BossBarDrawParams drawParams)
        {
            if (npc.ai[0] <= -1 && npc.ai[0] >= -7)
            {
                drawParams.ShowText = false;
                drawParams.BarCenter += Main.rand.NextVector2Circular(0.2f, 0.2f) * 5f;
            }
            return true;
        }

        public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
        {
            NPC npc = Main.npc[info.npcIndexToAimAt];
            if (npc.townNPC || !npc.active)
                return false;

            life = npc.life;
            lifeMax = npc.lifeMax;


            bossHeadIndex = npc.GetBossHeadTextureIndex();
            return true;
        }
    }
}
