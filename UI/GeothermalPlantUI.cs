using DawnOfIndustryPower.TileEntities.Generators;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using TheOneLibrary.Base.UI;
using TheOneLibrary.UI.Elements;
using TheOneLibrary.Utility;

namespace DawnOfIndustryPower.UI
{
	public class GeothermalPlantUI : BaseUI, ITileEntityUI
	{
		public TEGeothermalPlant geothermalPlant;

		public UIText textLabel = new UIText("Geothermal Power Plant");

		public UIPanel panelInfo = new UIPanel();
		public UIEnergyBar barEnergy = new UIEnergyBar();
		public UIHeatBar barHeat = new UIHeatBar();
		public UIText textGeneration = new UIText("Generating:");
		public UIText textWaterTiles = new UIText("Lava Tiles:");

		public override void OnInitialize()
		{
			panelMain.Width.Pixels = Main.screenWidth / 5f;
			panelMain.Height.Precent = 0.25f;
			panelMain.Center();
			panelMain.SetPadding(0);
			panelMain.OnMouseDown += DragStart;
			panelMain.OnMouseUp += DragEnd;
			panelMain.BackgroundColor = panelColor;
			Append(panelMain);

			textLabel.Top.Pixels = 8;
			textLabel.HAlign = 0.5f;
			panelMain.Append(textLabel);

			barEnergy.Width.Pixels = 32;
			barEnergy.Height.Set(-16, 1);
			barEnergy.Left.Set(-40, 1);
			barEnergy.Top.Pixels = 8;
			panelMain.Append(barEnergy);

			barHeat.Width.Pixels = 32;
			barHeat.Height.Set(-16, 1);
			barHeat.Left.Set(-80, 1);
			barHeat.Top.Pixels = 8;
			panelMain.Append(barHeat);

			panelInfo.Width.Set(-96, 1);
			panelInfo.Height.Set(-44, 1);
			panelInfo.Left.Pixels = 8;
			panelInfo.Top.Pixels = 36;
			panelInfo.SetPadding(0);
			panelMain.Append(panelInfo);

			textGeneration.Left.Pixels = 8;
			textGeneration.Top.Pixels = 8;
			panelInfo.Append(textGeneration);

			textWaterTiles.Left.Pixels = 8;
			textWaterTiles.Top.Pixels = 36;
			panelInfo.Append(textWaterTiles);
		}

		public override void Load()
		{
			barEnergy.energy = geothermalPlant.energy;
			barHeat.heat = geothermalPlant.heat;
		}

		public override void Update(GameTime gameTime)
		{
			textGeneration.SetText($"Generating: {geothermalPlant.energyGen.AsPower(true)}");
			textWaterTiles.SetText($"Heat: {geothermalPlant.GetHeat().ToSI()}HU");

			textGeneration.Recalculate();
			textWaterTiles.Recalculate();

			base.Update(gameTime);
		}

		public void SetTileEntity(ModTileEntity tileEntity) => geothermalPlant = (TEGeothermalPlant)tileEntity;
	}
}