using DawnOfIndustryPower.Tiles.Generators;
using System;
using Terraria;
using Terraria.ID;
using static BaseLib.Utility.Utility;

namespace DawnOfIndustryPower.TileEntities.Generators
{
	public class TESolarPanel : BaseGenerator
	{
		public override bool ValidTile(Tile tile) => tile.type == mod.TileType<SolarPanel>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i, j);

			NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
			return -1;
		}

		public override void OnPlace()
		{
			energy.SetCapacity(50000);
			energy.SetMaxTransfer(2500);
		}

		public override void Update()
		{
			float eff = Main.dayTime ? (float)(Main.time < 13500 ? Main.time / 13500 : 13500 / (Main.time - 13500)) : 0;
			energyGen = Math.Min((long)(1000 * eff), energy.GetCapacity() - energy.GetEnergyStored());

			energy.ModifyEnergyStored(10);

			this.HandleUIFar();
		}
	}
}