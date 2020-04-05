using System;
using System.Xml;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using SandBox.ViewModelCollection.Tournament;

namespace ClanTweaker
{
    //[HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    public class ClanTweakerTournamentRenown
    {
        public static void Postfix(Hero winner, Town town, ref int __result)
        {
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("TournamentSettings");

			__result = int.Parse(settings.SelectSingleNode("RenownGain").InnerText);
        }
    }

    //[HarmonyPatch(typeof(TournamentVM), "RefreshBetProperties")]
    public class ClanTweakerTournamentMaxBet
    {
        public static void Postfix(TournamentVM __instance)
		{
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("TournamentSettings");

			typeof(TournamentVM).GetProperty("MaximumBetValue").SetValue(__instance, Math.Min(int.Parse(settings.SelectSingleNode("MaximumBet").InnerText), Hero.MainHero.Gold));
        }
    } 
}
