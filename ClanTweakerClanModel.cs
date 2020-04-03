using HarmonyLib;
using System.Xml;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace ClanTweaker
{
    //[HarmonyPatch(typeof(DefaultClanTierModel), "GetCompanionLimitForTier")]
    public class ClanTweakerCompanionLimit
    {
        public static void Postfix(int clanTier, ref int __result)
        {
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("CompanionSettings");
			string mode = settings.SelectSingleNode("Mode").InnerText;

            if (mode == "modifier")
				__result = (clanTier + 3) * int.Parse(settings.SelectSingleNode("ModifierValue").InnerText);
			else if (mode == "increase")
				__result = clanTier * int.Parse(settings.SelectSingleNode("IncreaseValue").InnerText);
			else if (mode == "fixed")
                __result = int.Parse(settings.SelectSingleNode("FixedValue").InnerText);
        }
    }

    //[HarmonyPatch(typeof(DefaultClanTierModel), "GetPartyLimitForTier")]
    public class ClanTweakerPartyLimit
    {
		public static void Postfix(Clan clan, int clanTierToCheck, ref int __result)
		{
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("PartySettings");
			string mode = settings.SelectSingleNode("Mode").InnerText;
			
            if (mode == "increase")
				__result = clanTierToCheck * int.Parse(settings.SelectSingleNode("IncreaseValue").InnerText);
			else if (mode == "fixed")
                __result = int.Parse(settings.SelectSingleNode("FixedValue").InnerText);

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

            if (mode == "modifier")
                __result = result * int.Parse(settings.SelectSingleNode("ModifierValue").InnerText);
        }
    }
}
