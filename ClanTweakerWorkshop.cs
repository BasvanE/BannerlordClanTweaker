using HarmonyLib;
using System.Xml;
using TaleWorlds.CampaignSystem;

namespace ClanTweaker
{
    //[HarmonyPatch(typeof(DefaultWorkshopModel), "GetMaxWorkshopCountForPlayer")]
    public class ClanTweakerWorkshop
    {
        public static void Postfix(ref int __result)
		{
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("WorkshopSettings");
			string mode = settings.SelectSingleNode("Mode").InnerText;

			if (mode == "modifier")
                __result = (Clan.PlayerClan.Tier + 1) * int.Parse(settings.SelectSingleNode("ModifierValue").InnerText);
            else if (mode == "increase")
                __result = Clan.PlayerClan.Tier * int.Parse(settings.SelectSingleNode("IncreaseValue").InnerText);
			else if (mode == "fixed")
                __result = int.Parse(settings.SelectSingleNode("FixedValue").InnerText);
		}
    }
}
