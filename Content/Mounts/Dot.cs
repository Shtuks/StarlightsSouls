using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Mounts
{
	public class Dot : ModMount
	{
		public const float speed = 20f;

		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 226;
			MountData.spawnDustNoGravity = true;
            //MountData.buff = ModContent.BuffType<DotBuff>();
            MountData.heightBoost = 0;
			MountData.flightTimeMax = Int32.MaxValue;
			MountData.fatigueMax = Int32.MaxValue;
			MountData.fallDamage = 0f;
			MountData.usesHover = true;
			MountData.runSpeed = speed;
			MountData.dashSpeed = speed;
			MountData.acceleration = speed;
			MountData.swimSpeed = speed;
			MountData.jumpHeight = 8;
			MountData.jumpSpeed = 8f;
			MountData.blockExtraJumps = true;
			MountData.totalFrames = 1;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 0;
			}
			MountData.playerYOffsets = new int[] { 0 };
			MountData.xOffset = 16;
			MountData.bodyFrame = 5;
			MountData.yOffset = 16;
			MountData.playerHeadOffset = 18;
			MountData.standingFrameCount = 0;
			MountData.standingFrameDelay = 0;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 0;
			MountData.runningFrameDelay = 0;
			MountData.runningFrameStart = 0;
			MountData.flyingFrameCount = 0;
			MountData.flyingFrameDelay = 0;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 0;
			MountData.inAirFrameDelay = 0;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 0;
			MountData.idleFrameDelay = 0;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = true;
			MountData.swimFrameCount = 0;
			MountData.swimFrameDelay = 0;
			MountData.swimFrameStart = 0;
            if (Main.netMode != NetmodeID.Server)
            {
                MountData.textureWidth = MountData.frontTexture.Width();
                MountData.textureHeight = MountData.frontTexture.Height();
            }
        }
	}
}