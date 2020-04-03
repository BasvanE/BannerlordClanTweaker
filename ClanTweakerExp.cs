using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace ClanTweaker
{
    [HarmonyPatch(typeof(TroopRoster), "AddXpToTroop")]
    class ClanTweakerTroopExp
    {
        private static void Prefix(ref int xpAmount)
        {
            xpAmount = (int)Math.Ceiling(xpAmount * ClanTweakerSubModule.settings.troopXpModifier);
        }
    }
}
