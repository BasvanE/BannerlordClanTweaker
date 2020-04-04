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
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("TroopXpSettings");

			xpAmount = (int)Math.Ceiling(xpAmount * float.Parse(settings.SelectSingleNode("XpModifier").InnerText));
		}
    }

	//[HarmonyPatch(typeof(DefaultCombatXpModel), "GetXpFromHit")]
	public class ClanTweakerTroopXpFromHit
	{
		public static void Prefix(CharacterObject attackerTroop, CharacterObject attackedTroop, int damage, bool isFatal, bool isSimulated, ref int xpAmount)
		{
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("TroopXpSettings");

			xpAmount = (int)(xpAmount * float.Parse(settings.SelectSingleNode("XpModifier").InnerText));
		}
	}
}
