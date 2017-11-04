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
	public class SolarPanelUI : BaseUI, TileEntityUI
	{
		public TESolarPanel panel;

		public UIText textLabel = new UIText("Solar Panel");
		public UIEnergyBar barEnergy = new UIEnergyBar();

		public UIPanel panelInfo = new UIPanel();
		public UIText textGeneration = new UIText("Generating:");
		public UIText textEfficiency = new UIText("Efficiency:");

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

			textEfficiency.Left.Pixels = 8;
			textEfficiency.Top.Pixels = 36;
			panelInfo.Append(textEfficiency);
		}

		public override void Load()
		{
			barEnergy.energy = panel.energy;
		}

		public override void Update(GameTime gameTime)
		{
			textGeneration.SetText($"Generating: {panel.energyGen.AsPower(true)}");
			float eff = Main.dayTime ? (float)(Main.time < 13500 ? Main.time / 13500 : 13500 / (Main.time - 13500)) * 100 : 0;
			textEfficiency.SetText($"Efficiency: {Math.Round(eff, 2)}%");

			textGeneration.Recalculate();
			textEfficiency.Recalculate();

			base.Update(gameTime);
		}

		public void SetTileEntity(ModTileEntity tileEntity) => panel = (TESolarPanel)tileEntity;
	}
}