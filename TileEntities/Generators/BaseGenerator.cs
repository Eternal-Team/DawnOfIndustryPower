using BaseLib;
using ContainerLib2;
using ContainerLib2.Container;
using EnergyLib.Energy;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DawnOfIndustryPower.TileEntities.Generators
{
	public abstract class BaseGenerator : BaseTE, IContainerTile, IEnergyProvider
	{
		public IList<Item> Items = new List<Item>();
		public EnergyStorage energy = new EnergyStorage(1000);

		public long energyGen;

		public override TagCompound Save() => new TagCompound
		{
			["Items"] = Items.Save(),
			["Energy"] = energy
		};

		public override void Load(TagCompound tag)
		{
			energy = tag.Get<EnergyStorage>("Energy");
			Items = Utility.Load(tag);
		}

		public override void NetSend(BinaryWriter writer, bool lightSend) => TagIO.Write(Save(), writer);

		public override void NetReceive(BinaryReader reader, bool lightReceive) => Load(TagIO.Read(reader));

		public override void OnNetPlace() => OnPlace();

		public IList<Item> GetItems() => Items;

		public Item GetItem(int slot) => Items[slot];

		public void SetItem(Item item, int slot) => Items[slot] = item;

		public ModTileEntity GetTileEntity() => this;

		public EnergyStorage GetEnergyStorage() => energy;
	}
}