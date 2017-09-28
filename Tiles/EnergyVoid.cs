using DawnOfIndustryPower.TileEntities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DawnOfIndustryPower.Tiles
{
	public class EnergyVoid : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = DawnOfIndustryPower.PlaceholderTexturePath;
			//texture = DawnOfIndustryPower.TileTexturePath + "EnergyVoid";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults()
		{
			Main.tileSolid[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<TEEnergyVoid>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			disableSmartCursor = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Energy Void");
			AddMapEntry(Color.Black, name);

			drop = mod.ItemType<Items.EnergyVoid>();
		}

		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			mod.GetTileEntity<TEEnergyVoid>().Kill(i, j);
		}
	}
}