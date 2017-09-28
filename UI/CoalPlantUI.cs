using BaseLib.Elements;
using BaseLib.UI;
using ContainerLib2.Container;
using DawnOfIndustryPower.TileEntities.Generators;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using static BaseLib.Utility.Utility;

namespace DawnOfIndustryPower.UI
{
	public class CoalPlantUI : BaseUI, TileEntityUI
	{
		public TECoalPlant coalPlant;

		public UIText textLabel = new UIText("Coal Power Plant");
		public UIEnergyBar barEnergy = new UIEnergyBar();

		public UIPanel panelInfo = new UIPanel();
		public UIText textGeneration = new UIText("Generating:");
		public UIText textBurnTime = new UIText("Burn time:");

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

			textBurnTime.Left.Pixels = 8;
			textBurnTime.Top.Pixels = 36;
			panelInfo.Append(textBurnTime);
		}

		public override void Load()
		{
			UIContainerSlot slotFuel = new UIContainerSlot(coalPlant);
			slotFuel.Left.Pixels = 8;
			slotFuel.Top.Pixels = 64;
			panelInfo.Append(slotFuel);
		}

		public override void Update(GameTime gameTime)
		{
			barEnergy.power = coalPlant.energy.GetEnergyStored();
			barEnergy.maxPower = coalPlant.energy.GetCapacity();

			textGeneration.SetText($"Generating: {coalPlant.energyGen.ToSI()}W");
			textBurnTime.SetText($"Energy Left: {coalPlant.energyLeft.ToSI()}/{coalPlant.maxEnergy.ToSI()}");

			textGeneration.Recalculate();
			textBurnTime.Recalculate();

			base.Update(gameTime);
		}

		public void SetTileEntity(ModTileEntity tileEntity) => coalPlant = (TECoalPlant)tileEntity;
	}
}