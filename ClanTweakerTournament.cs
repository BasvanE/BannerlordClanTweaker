using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using SandBox.ViewModelCollection.Tournament;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace ClanTweaker
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    class ClanTweakerTournamentRenown
    {
        private static void Postfix(Hero winner, Town town, ref int __result)
        {
            __result = ClanTweakerSubModule.settings.tournamentRenown;
        }
    }

    [HarmonyPatch(typeof(TournamentVM), "RefreshBetProperties")]
    class ClanTweakerTournamentMaxBet
    {
        static void Postfix(TournamentVM __instance)
        {
            typeof(TournamentVM).GetProperty("MaximumBetValue").SetValue(__instance, Math.Min(ClanTweakerSubModule.settings.tournamentMaxBet, Hero.MainHero.Gold));
        }
    } 
}
