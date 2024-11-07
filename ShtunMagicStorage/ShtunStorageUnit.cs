using MagicStorage;
using MagicStorage.Components;
using MagicStorage.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ssm.Core;

namespace ssm.ShtunMagicStorage
{
    [ExtendsFromMod(ModCompatibility.MagicStorage.Name)]
    [JITWhenModsEnabled(ModCompatibility.MagicStorage.Name)]
    public class ShtunStorageUnit : MagicStorage.Components.StorageComponent
	{
		public override void ModifyObjectData()
		{
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 6;
			TileObjectData.newTile.StyleWrapLimit = 6;
		}

		public override TEStorageUnit GetTileEntity() => ModContent.GetInstance<TEStorageUnit>();

		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.noThrow = 2;
		}

		public override int ItemType(int frameX, int frameY)
		{
			int style = frameY / 36;
			int type = style switch
			{
                //10 => ModContent.ItemType<StorageUnitTarragon>(), //1280
                //11 => ModContent.ItemType<StorageUnitCosmilite>(), //2560
                //12 => ModContent.ItemType<StorageUnitAuric>(), //5120
                //13 => ModContent.ItemType<StorageUnitAbom>(), //10240
                //14 => ModContent.ItemType<StorageUnitShadowspec>(), //20480
                //15 => ModContent.ItemType<StorageUnitMutant>(), //40960
                3 => ModContent.ItemType<StorageUnitShtuxian>(), //81920
				_ => ModContent.ItemType<MagicStorage.Items.StorageUnit>()
			};
			return type;
		}

		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (Main.tile[i, j].TileFrameX % 36 == 18)
				i--;
			if (Main.tile[i, j].TileFrameY % 36 == 18)
				j--;

			if (TileEntity.ByPosition.ContainsKey(new Point16(i, j)) && Main.tile[i, j].TileFrameX / 36 % 3 != 0)
				fail = true;
		}

		public override bool CanExplode(int i, int j) {
			bool fail = false, discard = false, discard2 = false;

			KillTile(i, j, ref fail, ref discard, ref discard2);

			return !fail;
		}

		public override bool RightClick(int i, int j)
		{
			if (Main.tile[i, j].TileFrameX % 36 == 18)
				i--;
			if (Main.tile[i, j].TileFrameY % 36 == 18)
				j--;
			if (TryUpgrade(i, j))
				return true;

			if (!TileEntity.ByPosition.TryGetValue(new Point16(i, j), out var te) || te is not TEStorageUnit storageUnit)
				return false;

			Main.LocalPlayer.tileInteractionHappened = true;
			string activeString = storageUnit.Inactive ? Language.GetTextValue("Mods.MagicStorage.Inactive") : Language.GetTextValue("Mods.MagicStorage.Active");
			string fullnessString = Language.GetTextValue("Mods.MagicStorage.Capacity", storageUnit.NumItems, storageUnit.Capacity);
			Main.NewText(activeString + ", " + fullnessString);
			return base.RightClick(i, j);
		}

		private static bool TryUpgrade(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Item item = player.HeldItem;
			int style = Main.tile[i, j].TileFrameY / 36;
			bool success = false;
            /*if (style == 7 && item.type == ModContent.ItemType<UpgradeTarragon>())
            {
                SetStyle(i, j, 10);
                success = true;
            }
            else if (style == 10 && item.type == ModContent.ItemType<UpgradeCosmilite>())
            {
                SetStyle(i, j, 11);
                success = true;
            }
            else if (style == 11 && item.type == ModContent.ItemType<UpgradeAuric>())
            {
                SetStyle(i, j, 12);
                success = true;
            }
            else if (style == 12 && item.type == ModContent.ItemType<UpgradeAbom>())
            {
                SetStyle(i, j, 13);
                success = true;
            }
            else if (style == 13 && item.type == ModContent.ItemType<UpgradeShadowspec>())
            {
                SetStyle(i, j, 14);
                success = true;
            }
            else if (style == 14 && item.type == ModContent.ItemType<UpgradeMutant>())
            {
                SetStyle(i, j, 15);
                success = true;
            }
            else if (style == 13 && item.type == ModContent.ItemType<UpgradeShtuxian>())
            {
                SetStyle(i, j, 16);
                success = true;
            }*/

            if (success)
			{
				if (!TileEntity.ByPosition.TryGetValue(new Point16(i, j), out var te) || te is not TEStorageUnit storageUnit)
					return false;

				storageUnit.UpdateTileFrame();
				NetMessage.SendTileSquare(Main.myPlayer, i, j, 2, 2);
				MagicStorage.Components.TEStorageHeart heart = storageUnit.GetHeart();
				if (heart is not null)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
						heart.ResetCompactStage();
					else if (Main.netMode == NetmodeID.MultiplayerClient)
						NetHelper.SendResetCompactStage(heart.Position);
				}

				item.stack--;
				if (item.stack <= 0)
					item.SetDefaults();
				if (player.selectedItem == 58)
					Main.mouseItem = item.Clone();

				SoundEngine.PlaySound(SoundID.MaxMana, storageUnit.Position.ToWorldCoordinates());
				Dust.NewDustPerfect(storageUnit.Position.ToWorldCoordinates(), DustID.PureSpray, Vector2.Zero, Scale: 2, newColor: Color.Green);

				return true;
			}

			return false;
		}

		private static void SetStyle(int i, int j, int style)
		{
			Main.tile[i, j].TileFrameY = (short) (36 * style);
			Main.tile[i + 1, j].TileFrameY = (short) (36 * style);
			Main.tile[i, j + 1].TileFrameY = (short) (36 * style + 18);
			Main.tile[i + 1, j + 1].TileFrameY = (short) (36 * style + 18);
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
			Vector2 drawPos = zero + 16f * new Vector2(i, j) - Main.screenPosition;
			Rectangle frame = new(tile.TileFrameX, tile.TileFrameY, 16, 16);
			Color lightColor = Lighting.GetColor(i, j, Color.White);
			Color color = Color.Lerp(Color.White, lightColor, 0.5f);
			spriteBatch.Draw(Mod.Assets.Request<Texture2D>("ShtunMagicStorage/ShtunStorageUnit_Glow").Value, drawPos, frame, color);
		}
	}
}
