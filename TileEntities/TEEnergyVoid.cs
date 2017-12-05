using DawnOfIndustryPower.Tiles;
using Terraria;
using Terraria.ID;
using TheOneLibrary.Base;
using TheOneLibrary.Energy.Energy;
using TheOneLibrary.Utility;

namespace DawnOfIndustryPower.TileEntities
{
	public class TEEnergyVoid : BaseTE, IEnergyReceiver
	{
		public EnergyStorage energyStorage = new EnergyStorage(long.MaxValue, long.MaxValue);

		public override bool ValidTile(Tile tile) => tile.type == mod.TileType<EnergyVoid>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i, j);

			NetMessage.SendTileSquare(Main.myPlayer, i, j, 1);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
			return -1;
		}

		public override void Update()
		{
			energyStorage.ExtractEnergy(energyStorage.GetMaxExtract());
		}

		public EnergyStorage GetEnergyStorage() => energyStorage;

		public long ReceiveEnergy(long maxReceive) => energyStorage.ReceiveEnergy(maxReceive);

		public long GetEnergy() => energyStorage.GetEnergy();

		public long GetCapacity() => energyStorage.GetCapacity();
	}
}