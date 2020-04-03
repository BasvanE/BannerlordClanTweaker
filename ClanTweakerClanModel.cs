using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Library;

namespace ClanTweaker
{
    [HarmonyPatch(typeof(DefaultClanTierModel), "GetCompanionLimitForTier")]
    class ClanTweakerCompanionLimit
    {
        private static void Postfix(int clanTier, ref int __result)
        {
            if (ClanTweakerSubModule.settings.companionMode == "fixed")
                __result = ClanTweakerSubModule.settings.companionFixed;
            else if (ClanTweakerSubModule.settings.companionMode == "modifier")
                __result = (clanTier + 3) * ClanTweakerSubModule.settings.companionModifier;
            else if (ClanTweakerSubModule.settings.companionMode == "increase")
                __result = clanTier * ClanTweakerSubModule.settings.companionIncrease;
        }
    }

    [HarmonyPatch(typeof(DefaultClanTierModel), "GetPartyLimitForTier")]
    class ClanTweakerPartyLimit
    {
        private static void Postfix(Clan clan, int clanTierToCheck, ref int __result)
        {
            if (ClanTweakerSubModule.settings.partyMode == "fixed")
                __result = ClanTweakerSubModule.settings.partyFixed;
            else if (ClanTweakerSubModule.settings.partyMode == "increase")
                __result = clanTierToCheck * ClanTweakerSubModule.settings.partyIncrease;

            int result;
            if (!clan.IsMinorFaction)
            {
                if (clanTierToCheck < 3) { result = 1; }
                else if (clanTierToCheck < 5) { result = 2; }
                else { result = 3; }
            }
            else
            {
                result = (int)MathF.Clamp(clanTierToCheck, 1f, 4f);
            }

            if (ClanTweakerSubModule.settings.partyMode == "modifier")
                __result = result * ClanTweakerSubModule.settings.partyModifier;
        }
    }
}
