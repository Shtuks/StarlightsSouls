using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ssm.Content.NPCs.StarlightCat
{
	public class BulletBounce : BaseProj
	{
		public Vector2 Velocity;
		public int BouncesCount;

		public BulletBounce(Vector2 position, Vector2 velocity, int bouncesCount, float size, Texture2D texture)
			: base(position, size, texture)
		{
			Velocity = velocity;
			BouncesCount = bouncesCount;
		}

		public override void Update()
		{
			Position += Velocity;
			Vector2 origin = StarlightCatBoss.Origin;
			float arenaSize = StarlightCatBoss.ArenaSize;
			if (Position.X - Size <= origin.X - arenaSize && Velocity.X < 0f)
			{
				Position.X = 2 * (origin.X - arenaSize) - (Position.X - Size) + Size;
				Velocity.X *= -1f;
                BouncesCount--;
			}
			if (Position.X + Size >= origin.X + arenaSize && Velocity.X > 0f)
			{
				Position.X = 2 * (origin.X + arenaSize) - (Position.X + Size) - Size;
				Velocity.X *= -1f;
                BouncesCount--;
			}
			if (Position.Y - Size <= origin.Y - arenaSize && Velocity.Y < 0f)
			{
				Position.Y = 2 * (origin.Y - arenaSize) - (Position.Y - Size) + Size;
				Velocity.Y *= -1f;
                BouncesCount--;
			}
			if (Position.Y + Size >= origin.Y + arenaSize && Velocity.Y > 0f)
			{
				Position.Y = 2 * (origin.Y + arenaSize) - (Position.Y + Size) - Size;
				Velocity.Y *= -1f;
                BouncesCount--;
			}
		}
	}
}