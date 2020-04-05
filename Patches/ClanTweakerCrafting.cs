using System;
using System.Xml;
using System.Collections;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace ClanTweaker
{
    //[HarmonyPatch(typeof(CraftingCampaignBehavior), "GetMaxHeroCraftingStamina")]
    public class ClanTweakerCraftingMaxStamina
    {
        public static void Postfix(Hero hero, ref int __result)
		{
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("CraftingSettings");

			__result = int.Parse(settings.SelectSingleNode("MaxStamina").InnerText);
        }
    }

    //[HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    public class ClanTweakerCraftingStaminaGain
    {
        public static void Prefix(CraftingCampaignBehavior __instance)
        {
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("CraftingSettings");
			int maxStamina = int.Parse(settings.SelectSingleNode("MaxStamina").InnerText);
			int hourlyGain = int.Parse(settings.SelectSingleNode("StaminaGain").InnerText);

			IDictionary dictionary = (IDictionary)typeof(CraftingCampaignBehavior).GetField("_heroCraftingRecords", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);
            foreach (object obj in dictionary.Keys)
            {
                Hero hero = (Hero)obj;
                int heroCraftingStamina = __instance.GetHeroCraftingStamina(hero);
                if (heroCraftingStamina < maxStamina)
                    if (((hero.PartyBelongedTo != null) ? hero.PartyBelongedTo.CurrentSettlement : null) != null)
                        __instance.SetHeroCraftingStamina(hero, Math.Min(maxStamina, heroCraftingStamina + hourlyGain));
            }
        }
    }
}
