using ssm.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Mounts
{
	public class Dot : ModMount
	{
		public const float MovementSpeed = 20f;

		public override void SetStaticDefaults()
		{
            MountData.buff = ModContent.BuffType<DotBuff>();
            MountData.heightBoost = 0;
            MountData.flightTimeMax = int.MaxValue;
            MountData.fatigueMax = int.MaxValue;
            MountData.fallDamage = 0f;
            MountData.usesHover = true;
            MountData.runSpeed = MovementSpeed;
            MountData.dashSpeed = MovementSpeed;
            MountData.acceleration = MovementSpeed;
            MountData.swimSpeed = MovementSpeed;
            MountData.jumpHeight = 8;
            MountData.jumpSpeed = 8f;
            MountData.blockExtraJumps = true;

            int[] verticalOffsets = new int[MountData.totalFrames];
            for (int l = 0; l < verticalOffsets.Length; l++)
                verticalOffsets[l] = 0;

            MountData.playerYOffsets = verticalOffsets;
            MountData.xOffset = 2;
            MountData.bodyFrame = 5;
            MountData.yOffset = 28;
            MountData.playerHeadOffset = 3;
            MountData.standingFrameCount = 5;
            MountData.standingFrameDelay = 5;
            MountData.standingFrameStart = 0;
            MountData.runningFrameCount = 5;
            MountData.runningFrameDelay = 5;
            MountData.runningFrameStart = 0;
            MountData.flyingFrameCount = 5;
            MountData.flyingFrameDelay = 5;
            MountData.flyingFrameStart = 0;
            MountData.inAirFrameCount = 5;
            MountData.inAirFrameDelay = 5;
            MountData.inAirFrameStart = 0;
            MountData.idleFrameCount = 5;
            MountData.idleFrameDelay = 5;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = true;
            MountData.swimFrameCount = 5;
            MountData.swimFrameDelay = 5;
            MountData.swimFrameStart = 0;
            if (Main.netMode != NetmodeID.Server)
            {
                MountData.textureWidth = MountData.frontTexture.Width();
                MountData.textureHeight = MountData.frontTexture.Height();
            }
        }
	}
}