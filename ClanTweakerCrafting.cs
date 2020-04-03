using System;
using System.Collections;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace ClanTweaker
{
    [HarmonyPatch(typeof(CraftingCampaignBehavior), "GetMaxHeroCraftingStamina")]
    class ClanTweakerCraftingMaxStamina
    {
        private static void Postfix(Hero hero, ref int __result)
        {
            __result = ClanTweakerSubModule.settings.craftingMaxStamina;
        }
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    class ClanTweakerCraftingStaminaGain
    {
        private static void Prefix(CraftingCampaignBehavior __instance)
        {
            IDictionary dictionary = (IDictionary)typeof(CraftingCampaignBehavior).GetField("_heroCraftingRecords", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);
            foreach (object obj in dictionary.Keys)
            {
                Hero hero = (Hero)obj;
                int heroCraftingStamina = __instance.GetHeroCraftingStamina(hero);
                if (heroCraftingStamina < ClanTweakerSubModule.settings.craftingMaxStamina)
                    if (((hero.PartyBelongedTo != null) ? hero.PartyBelongedTo.CurrentSettlement : null) != null)
                        __instance.SetHeroCraftingStamina(hero, Math.Min(ClanTweakerSubModule.settings.craftingMaxStamina, heroCraftingStamina + ClanTweakerSubModule.settings.craftingStaminaGain));
            }
        }
    }
}
