using DawnOfIndustryPower.TileEntities.Generators;
using DawnOfIndustryPower.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static BaseLib.Utility.Utility;

namespace DawnOfIndustryPower.Tiles.Generators
{
	public class WindTurbine : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = DawnOfIndustryPower.TileTexturePath + "WindTurbine";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults()
		{
			Main.tileSolid[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 6;
			TileObjectData.newTile.Origin = new Point16(0, 5);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<TEWindTurbine>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			disableSmartCursor = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Wind Turbine");
			AddMapEntry(Color.LightYellow, name);
		}

		public override void RightClick(int i, int j)
		{
			int ID = mod.GetID<TEWindTurbine>(i, j);
			if (ID == -1) return;

			DawnOfIndustryPower.Instance.HandleUI<WindTurbineUI>(ID);
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			Main.specX[nextSpecialDrawIndex] = i;
			Main.specY[nextSpecialDrawIndex] = j;
			nextSpecialDrawIndex++;
		}

		private Texture2D blade = ModLoader.GetTexture("DawnOfIndustryPower/Textures/Tiles/WindTurbineBlade");
		private float angle;

		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			int x = tile.TopLeft() ? i : 0;
			int y = tile.TopLeft() ? j : 0;
			
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen) zero = Vector2.Zero;
			Vector2 position = new Vector2(x * 16 - (int)Main.screenPosition.X + 24, y * 16 - (int)Main.screenPosition.Y + 24) + zero;

			spriteBatch.Draw(blade, position, null, Color.White, MathHelper.Pi / 180f * angle, new Vector2(10, blade.Height), Vector2.One, SpriteEffects.None, 0f);
			spriteBatch.Draw(blade, position, null, Color.White, MathHelper.Pi / 180f * (angle + 120f), new Vector2(10, blade.Height), Vector2.One, SpriteEffects.None, 0f);
			spriteBatch.Draw(blade, position, null, Color.White, MathHelper.Pi / 180f * (angle + 240f), new Vector2(10, blade.Height), Vector2.One, SpriteEffects.None, 0f);

			angle += 0.3f;
			if (angle >= 360) angle = 0;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int ID = mod.GetID<TEWindTurbine>(i, j);
			if (ID != -1) DawnOfIndustryPower.Instance.CloseUI(ID);

			Item.NewItem(i * 16, j * 16, 48, 96, mod.ItemType<Items.Generators.WindTurbine>());
			mod.GetTileEntity<TEWindTurbine>().Kill(i, j);
		}
	}
}