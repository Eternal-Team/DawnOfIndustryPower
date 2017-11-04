using BaseLib.UI;
using DawnOfIndustryPower.TileEntities.Generators;
using EnergyLib;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using static BaseLib.Utility.Utility;

namespace DawnOfIndustryPower.UI
{
	public class WaterTurbineUI : BaseUI, TileEntityUI
	{
		public TEWaterTurbine turbine;

		public UIText textLabel = new UIText("Water Turbine");

		public UIPanel panelInfo = new UIPanel();
		public UIEnergyBar barEnergy = new UIEnergyBar();
		public UIText textGeneration = new UIText("Generating:");
		public UIText textWaterTiles = new UIText("Water Tiles:");

		public override void OnInitialize()
		{
			panelMain.Width.Pixels = Main.screenWidth / 7f;
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
			barEnergy.Left.Set(-barEnergy.Width.Pixels - 8, 1);
			barEnergy.Top.Pixels = 8;
			panelMain.Append(barEnergy);

			panelInfo.Width.Set(-56, 1);
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
			barEnergy.energy = turbine.energy;
		}

		public override void Update(GameTime gameTime)
		{
			textGeneration.SetText($"Generating: {turbine.energyGen.AsPower(true)}");
			textWaterTiles.SetText($"Water Tiles: {Math.Round(turbine.waterVolume / 255f, 0)}/{TEWaterTurbine.MaxWaterTiles}");

			textGeneration.Recalculate();
			textWaterTiles.Recalculate();

			base.Update(gameTime);
		}

		public void SetTileEntity(ModTileEntity tileEntity) => turbine = (TEWaterTurbine)tileEntity;
	}
}