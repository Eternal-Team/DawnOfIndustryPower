using BaseLib.Utility;
using DawnOfIndustryPower.Tiles.Generators;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using static BaseLib.Utility.Utility;

namespace DawnOfIndustryPower.TileEntities.Generators
{
	public class TEWaterTurbine : BaseGenerator
	{
		public int waterScanTimer;
		public int waterVolume;

		public const int MaxWaterTiles = 100;

		public override bool ValidTile(Tile tile) => tile.type == mod.TileType<WaterTurbine>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i, j - 1);

			NetMessage.SendTileSquare(Main.myPlayer, i, j - 1, 3);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j - 1, Type);
			return -1;
		}

		public override void OnPlace()
		{
			energy.SetCapacity(250000);
			energy.SetMaxTransfer(10000);
		}

		public override void Update()
		{
			if (++waterScanTimer >= 150)
			{
				Point16 start = GetDirection(Position.X, Position.Y, Main.tile[Position.X, Position.Y].type) == Terraria.Enums.TileObjectDirection.PlaceLeft ? new Point16(Position.X - 1, Position.Y + 1) : new Point16(Position.X + 3, Position.Y + 1);
				Trace(start, tile => !tile.active() && tile.liquidType() == Tile.Liquid_Water && tile.liquid > 0, tile => waterVolume += tile.liquidType() == Tile.Liquid_Water ? tile.liquid : 0);
				waterVolume = Math.Min(waterVolume, 255 * 100);
				waterScanTimer = 0;
			}

			energyGen = (long)Math.Min(waterVolume / 255f * 50, energy.GetCapacity() - energy.GetEnergyStored());

			this.HandleUIFar();
		}
	}
}