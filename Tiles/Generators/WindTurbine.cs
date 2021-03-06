﻿using DawnOfIndustryPower.TileEntities.Generators;
using DawnOfIndustryPower.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TheOneLibrary.Base;
using TheOneLibrary.Utility;

namespace DawnOfIndustryPower.Tiles.Generators
{
	public class WindTurbine : BaseTile
	{
		public override string Texture => DawnOfIndustryPower.TileTexturePath + "WindTurbine";

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

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int ID = mod.GetID<TEWindTurbine>(i, j);
			if (ID != -1) DawnOfIndustryPower.Instance.CloseUI(ID);

			Item.NewItem(i * 16, j * 16, 48, 96, mod.ItemType<Items.Generators.WindTurbine>());
			mod.GetTileEntity<TEWindTurbine>().Kill(i, j);
		}
	}
}