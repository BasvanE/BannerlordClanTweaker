using System;
using System.Reflection;
using System.Xml;
using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace ClanTweaker
{
    //[HarmonyPatch(typeof(TroopRoster), "AddXpToTroop")]
    public class ClanTweakerTroopExp
    {
        public static void Prefix(TroopRoster __instance, ref int xpAmount)
		{
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("PartySizeSettings");

			xpAmount = (int)Math.Ceiling(xpAmount * float.Parse(settings.SelectSingleNode("XpModifier").InnerText));
        }
    }
}
