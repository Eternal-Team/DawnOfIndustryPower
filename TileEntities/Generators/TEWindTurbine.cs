using DawnOfIndustryPower.Tiles.Generators;
using System;
using Terraria;
using Terraria.ID;
using static BaseLib.Utility.Utility;

namespace DawnOfIndustryPower.TileEntities.Generators
{
	public class TEWindTurbine : BaseGenerator
	{
		public override bool ValidTile(Tile tile) => tile.type == mod.TileType<WindTurbine>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i, j - 5);

			NetMessage.SendTileSquare(Main.myPlayer, i, j - 5, 6);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j - 5, Type);
			return -1;
		}

		public override void OnPlace()
		{
			energy.SetCapacity(100000);
			energy.SetMaxTransfer(5000);
		}

		public override void Update()
		{
			int reverseHeight = Main.maxTilesY - Position.Y + 1;
			energyGen = Math.Min(reverseHeight, energy.GetCapacity() - energy.GetEnergyStored());

			//energy.ModifyEnergyStored(energyGen);

			this.HandleUIFar();
		}
	}
}