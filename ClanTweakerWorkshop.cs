using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace ClanTweaker
{
    [HarmonyPatch(typeof(DefaultWorkshopModel), "GetMaxWorkshopCountForPlayer")]
    class ClanTweakerWorkshop
    {
        public static void Postfix(ref int __result)
        {
            if (ClanTweakerSubModule.settings.workshopMode == "fixed")
                __result = ClanTweakerSubModule.settings.workshopFixed;
            else if (ClanTweakerSubModule.settings.workshopMode == "modifier")
                __result = (Clan.PlayerClan.Tier + 1) * ClanTweakerSubModule.settings.workshopModifier;
            else if (ClanTweakerSubModule.settings.workshopMode == "increase")
                __result = Clan.PlayerClan.Tier * ClanTweakerSubModule.settings.workshopIncrease;
        }
    }
}
