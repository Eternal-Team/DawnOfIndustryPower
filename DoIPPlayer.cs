using Terraria.ModLoader;

namespace DawnOfIndustryPower
{
	public class DoIPPlayer : ModPlayer
	{
		public override void PostSavePlayer()
		{
			DawnOfIndustryPower.Instance.TEUI.Clear();
		}
	}
}