using DawnOfIndustryPower.TileEntities.Generators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DawnOfIndustryPower
{
	public class DOPWorld : ModWorld
	{
		private float angle;
		public override void PostDrawTiles()
		{
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen) zero = Vector2.Zero;

			int num4 = (int)((Main.screenPosition.X - zero.X) / 16f - 1f);
			int num5 = (int)((Main.screenPosition.X + Main.screenWidth + zero.X) / 16f) + 2;
			int num6 = (int)((Main.screenPosition.Y - zero.Y) / 16f - 1f);
			int num7 = (int)((Main.screenPosition.Y + Main.screenHeight + zero.Y) / 16f) + 5;
			if (num4 < 4) num4 = 4;
			if (num5 > Main.maxTilesX - 4) num5 = Main.maxTilesX - 4;
			if (num6 < 4) num6 = 4;
			if (num7 > Main.maxTilesY - 4) num7 = Main.maxTilesY - 4;

			foreach (KeyValuePair<Point16, TileEntity> kvp in TileEntity.ByPosition.Where(x => x.Value is TEWindTurbine && new Rectangle(num4 - 2, num6, num5 - num4, num7 - num6 - 4).Intersects(new Rectangle(x.Key.X, x.Key.Y, 3, 6))))
			{
				Vector2 position = new Vector2(kvp.Key.X * 16 - (int)Main.screenPosition.X + 24, kvp.Key.Y * 16 - (int)Main.screenPosition.Y + 24);
				Vector2 origin = new Vector2(10, DawnOfIndustryPower.turbineBlade.Height);

				Main.spriteBatch.Begin();
				Main.spriteBatch.Draw(DawnOfIndustryPower.turbineBlade, position, null, Color.White, MathHelper.Pi / 180f * angle, origin, Vector2.One, SpriteEffects.None, 0f);
				Main.spriteBatch.Draw(DawnOfIndustryPower.turbineBlade, position, null, Color.White, MathHelper.Pi / 180f * (angle + 120f), origin, Vector2.One, SpriteEffects.None, 0f);
				Main.spriteBatch.Draw(DawnOfIndustryPower.turbineBlade, position, null, Color.White, MathHelper.Pi / 180f * (angle + 240f), origin, Vector2.One, SpriteEffects.None, 0f);
				Main.spriteBatch.End();

				angle += Main.raining ? 5f : 2.5f;
				if (angle >= 360) angle = 0;
			}
		}
	}
}