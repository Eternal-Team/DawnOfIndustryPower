﻿using BaseLib.Items;
using Terraria;

namespace DawnOfIndustryPower.Items.Generators
{
	public class GeothermalPlant : BaseItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Geothermal Power Plant");
			Tooltip.SetDefault("Produces energy from lava");
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 0;
			item.value = Item.sellPrice(0, 0, 0, 10);
			item.createTile = mod.TileType<Tiles.Generators.GeothermalPlant>();
		}
	}
}