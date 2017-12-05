using DawnOfIndustryPower.Tiles.Generators;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using TheOneLibrary.Storage;
using TheOneLibrary.Utility;

namespace DawnOfIndustryPower.TileEntities.Generators
{
	public class TECoalPlant : BaseGenerator, ISidedContainer
	{
		public long energyLeft;
		public long maxEnergy;

		public override bool ValidTile(Tile tile) => tile.type == mod.TileType<CoalPlant>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i, j - 3);

			NetMessage.SendTileSquare(Main.myPlayer, i, j - 3, 5);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j - 3, Type);
			return -1;
		}

		public override void OnPlace()
		{
			Items.Add(new Item());
			energy.SetCapacity(100000);
			energy.SetMaxTransfer(1000);
		}

		public override void OnKill() => this.DropItems(new Rectangle(Position.X, Position.Y, 80, 64));

		public override void Update()
		{
			if (energyLeft == 0)
			{
				if (DawnOfIndustryPower.Instance.burnValues.ContainsKey(Items[0].type))
				{
					maxEnergy = DawnOfIndustryPower.Instance.burnValues[Items[0].type];
					energyLeft = maxEnergy;
					Items[0].stack--;
					if (Items[0].stack == 0) Items[0].TurnToAir();
				}
				else
				{
					maxEnergy = 0;
					energyGen = 0;
				}
			}
			else
			{
				energyGen = Math.Min(Math.Min(energyLeft, 1000), energy.GetCapacity() - energy.GetEnergy());
				energyLeft -= energyGen;
				energy.ModifyEnergyStored(energyGen);
			}
		}

		public IList<Item> GetInputSlots() => Items;

		public IList<Item> GetOutputSlots() => new List<Item>();

		public IList<Item> GetItems() => Items;
	}
}