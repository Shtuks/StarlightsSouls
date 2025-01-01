using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ssm.Content.NPCs.StarlightCat
{
	public abstract class BaseProj
	{
		public Vector2 Position;
		public float Size;
		public Texture2D Texture;
		public float Damage;
		public bool Active;

		public BaseProj(Vector2 position, float size, Texture2D texture)
		{
			this.Position = position;
			this.Size = size;
			this.Texture = texture;
			this.Damage = 0.1f;
			this.Active = true;
		}

		public abstract void Update();

		public virtual bool Remove()
		{
			return (Position.X + Size < StarlightCatBoss.Origin.X - StarlightCatBoss.ArenaSize)
				|| (Position.X - Size > StarlightCatBoss.Origin.X + StarlightCatBoss.ArenaSize)
				|| (Position.Y + Size < StarlightCatBoss.Origin.Y - StarlightCatBoss.ArenaSize)
				|| (Position.Y - Size > StarlightCatBoss.Origin.Y + StarlightCatBoss.ArenaSize);
		}
	}
}