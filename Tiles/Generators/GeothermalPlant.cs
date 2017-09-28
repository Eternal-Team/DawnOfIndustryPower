using DawnOfIndustryPower.TileEntities.Generators;
using DawnOfIndustryPower.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static BaseLib.Utility.Utility;

namespace DawnOfIndustryPower.Tiles.Generators
{
	public class GeothermalPlant : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = DawnOfIndustryPower.PlaceholderTexturePath;
			//texture = DawnOfIndustryPower.TileTexturePath + "GeothermalPlant";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.Width = 5;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<TEGeothermalPlant>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			disableSmartCursor = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Geothermal Power Plant");
			AddMapEntry(Color.LightYellow, name);
		}

		public override void RightClick(int i, int j)
		{
			int ID = mod.GetID<TEGeothermalPlant>(i, j);
			if (ID == -1) return;

			DawnOfIndustryPower.Instance.HandleUI<GeothermalPlantUI>(ID);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int ID = mod.GetID<TEGeothermalPlant>(i, j);
			if (ID != -1) DawnOfIndustryPower.Instance.CloseUI(ID);

			Item.NewItem(i * 16, j * 16, 80, 48, mod.ItemType<Items.Generators.GeothermalPlant>());
			mod.GetTileEntity<TEGeothermalPlant>().Kill(i, j);
		}
	}
}