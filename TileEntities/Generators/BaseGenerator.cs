using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TheOneLibrary.Base;
using TheOneLibrary.Energy.Energy;
using TheOneLibrary.Utility;

namespace DawnOfIndustryPower.TileEntities.Generators
{
	public abstract class BaseGenerator : BaseTE, IEnergyProvider
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

		public ModTileEntity GetTileEntity() => this;

		public long GetEnergy() => energy.GetEnergy();

		public long GetCapacity() => energy.GetCapacity();

		public EnergyStorage GetEnergyStorage() => energy;

		public long ExtractEnergy(long maxExtract) => energy.ExtractEnergy(maxExtract);
	}
}