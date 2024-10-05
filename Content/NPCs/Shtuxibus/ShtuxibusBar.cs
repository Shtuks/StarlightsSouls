using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Shtuxibus
{
    public class ShtuxibusBar : ModBossBar
    {
        private int bossHeadIndex = -1;

        public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
        {
            return this.bossHeadIndex != -1 ? TextureAssets.NpcHeadBoss[this.bossHeadIndex] : (Asset<Texture2D>)null;
        }

        public override bool? ModifyInfo(
          ref BigProgressBarInfo info,
          ref float life,
          ref float lifeMax,
          ref float shield,
          ref float shieldMax)
        {
            NPC npc = Main.npc[info.npcIndexToAimAt];
            if (!((Entity)npc).active)
                return new bool?(false);
            this.bossHeadIndex = npc.GetBossHeadTextureIndex();
            life = (float)npc.life;
            lifeMax = (float)npc.lifeMax;
            return new bool?(true);
        }
    }
}