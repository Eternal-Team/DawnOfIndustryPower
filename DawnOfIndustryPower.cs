using BaseLib;
using BaseLib.UI;
using BaseLib.Utility;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace DawnOfIndustryPower
{
	public class DawnOfIndustryPower : Mod, IMod
	{
		public static DawnOfIndustryPower Instance;

		public const string PlaceholderTexturePath = "DawnOfIndustryCore/Textures/Placeholder";
		public const string TileTexturePath = "DawnOfIndustryPower/Textures/Tiles/";

		public IDictionary<ModTileEntity, GUI> TEUI = new Dictionary<ModTileEntity, GUI>();

		public Dictionary<int, long> burnValues = new Dictionary<int, long>();

		public DawnOfIndustryPower()
		{
			Properties = new ModProperties
			{
				Autoload = true,
				AutoloadBackgrounds = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

		public override void PreSaveAndQuit()
		{
			TEUI.Clear();
		}

		public override void Load()
		{
			Instance = this;
		}

		public override void PostSetupContent()
		{
			burnValues.Add(ItemID.Wood, 1000);
			burnValues.Add(ItemID.BorealWood, 1250);
			burnValues.Add(ItemID.DynastyWood, 2000);
			burnValues.Add(ItemID.PalmWood, 1250);
			burnValues.Add(ItemID.SpookyWood, 5000);
			burnValues.Add(ItemID.Coal, 25000);
			burnValues.Add(ItemID.LivingFireBlock, 50000);
		}

		public override void Unload()
		{
			Instance = null;
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

			if (MouseTextIndex != -1)
			{
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
					"StorageRefinements: UI",
					delegate
					{
						TEUI.Values.Draw();

						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}

		public IDictionary<ModTileEntity, GUI> GetTEUIs() => TEUI;
	}
}