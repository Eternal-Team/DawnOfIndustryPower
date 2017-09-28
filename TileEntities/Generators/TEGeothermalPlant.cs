using BaseLib.Utility;
using DawnOfIndustryCore.Tiles;
using DawnOfIndustryPower.Tiles.Generators;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace DawnOfIndustryPower.TileEntities.Generators
{
	public class TEGeothermalPlant : BaseGenerator
	{
		public int lavaScanTimer;
		public int lavaVolume;

		public const int MaxLavaTiles = 150;

		public override bool ValidTile(Tile tile) => tile.type == mod.TileType<GeothermalPlant>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i, j - 2);

			NetMessage.SendTileSquare(Main.myPlayer, i, j - 2, 5);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j - 2, Type);
			return -1;
		}

		public override void OnPlace()
		{
			energy.SetCapacity(1000000);
			energy.SetMaxTransfer(25000);
		}

		public override void OnNetPlace() => OnPlace();

		public override void Update()
		{
			if (++lavaScanTimer >= 150)
			{
				lavaVolume = GetLavaVolume(Position + new Point16(2, 2));
				lavaScanTimer = 0;
			}

			energyGen = (long)Math.Min(lavaVolume / 255f * 150, energy.GetCapacity() - energy.GetEnergyStored());

			//energy.ModifyEnergyStored(generation);

			this.HandleUIFar();
		}

		public static IEnumerable<Point16> CheckNeighbours(Point16 point)
		{
			List<Point16> points = new List<Point16>();

			IEnumerable<Point16> neighbours = Utility.CheckNeighbours();

			foreach (Point16 add in neighbours)
			{
				int checkX = point.X + add.X;
				int checkY = point.Y + add.Y;
				Tile tile = Main.tile[checkX, checkY];
				if (!tile.active() && tile.liquidType() == Tile.Liquid_Lava && tile.liquid > 0 || tile.active() && tile.type == DawnOfIndustryCore.DawnOfIndustryCore.Instance.TileType<HeatPipe>()) points.Add(new Point16(checkX, checkY));
			}
			return points;
		}

		public static int GetLavaVolume(Point16 start)
		{
			int volume = 0;

			HashSet<Point16> explored = new HashSet<Point16>();
			explored.Add(start);
			Queue<Point16> toExplore = new Queue<Point16>();
			foreach (Point16 point in CheckNeighbours(start)) toExplore.Enqueue(point);

			while (toExplore.Count > 0)
			{
				Point16 explore = toExplore.Dequeue();
				if (!explored.Contains(explore))
				{
					explored.Add(explore);

					Tile tile = Main.tile[explore.X, explore.Y];
					if (tile.liquidType() == Tile.Liquid_Lava && tile.liquid > 0) volume += tile.liquid;
					else if (tile.active() && tile.type == DawnOfIndustryCore.DawnOfIndustryCore.Instance.TileType<HeatPipe>()) foreach (Point16 point in CheckNeighbours(explore)) toExplore.Enqueue(point);
				}
			}

			return volume;
		}
	}
}