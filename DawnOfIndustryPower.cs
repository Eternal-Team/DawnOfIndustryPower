using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TheOneLibrary.Base;
using TheOneLibrary.Base.UI;
using TheOneLibrary.Utility;

namespace DawnOfIndustryPower
{
	public class DawnOfIndustryPower : Mod
	{
		[Null] public static DawnOfIndustryPower Instance;

		[Null] public static Texture2D turbineBlade;
		
		public const string PlaceholderTexturePath = "DawnOfIndustryCore/Textures/Placeholder";
		public const string TileTexturePath = "DawnOfIndustryPower/Textures/Tiles/";

		[UI("TileEntity")]
		public Dictionary<ModTileEntity, GUI> TEUI = new Dictionary<ModTileEntity, GUI>();

		public const long FluxPerHU = 1;
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

			turbineBlade = ModLoader.GetTexture("DawnOfIndustryPower/Textures/Tiles/WindTurbineBlade");
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
			this.UnloadNullableTypes();

			GC.Collect();
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

			if (MouseTextIndex != -1)
			{
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
					"DoIPower: TileEntity",
					delegate
					{
						TEUI.Values.Draw();

						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}