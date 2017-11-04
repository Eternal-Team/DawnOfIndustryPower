using BaseLib.Utility;
using DawnOfIndustryCore.Heat.HeatStorage;
using DawnOfIndustryPower.Tiles.Generators;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace DawnOfIndustryPower.TileEntities.Generators
{
	public class TEGeothermalPlant : BaseGenerator, IHeatReceiver
	{
		public HeatStorage heat = new HeatStorage(1000000, 5000);

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
			energyGen = Math.Min(Math.Min(heat.GetHeat(), heat.GetMaxExtract()) * DawnOfIndustryPower.FluxPerHU, energy.GetCapacity() - energy.GetEnergy());

			heat.ModifyHeatStored(-energyGen / DawnOfIndustryPower.FluxPerHU);
			energy.ModifyEnergyStored(energyGen);

			this.HandleUIFar();
		}

		public override TagCompound Save()
		{
			TagCompound tag = base.Save();
			tag["Heat"] = heat;
			return tag;
		}

		public override void Load(TagCompound tag)
		{
			base.Load(tag);
			heat = tag.Get<HeatStorage>("Heat");
		}

		public long GetHeat() => heat.GetHeat();

		public HeatStorage GetHeatStorage() => heat;

		public long ReceiveHeat(long maxReceive) => heat.ReceiveHeat(maxReceive);
	}
}