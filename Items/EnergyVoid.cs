using BaseLib.Items;
using Terraria.ModLoader;

namespace DawnOfIndustryPower.Items
{
	public class EnergyVoid : BaseItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy Void");
			Tooltip.SetDefault("Voids energy");
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
			item.createTile = mod.TileType<Tiles.EnergyVoid>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}