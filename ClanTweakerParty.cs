using System;
using System.Xml;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace ClanTweaker
{
    //[HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    public class ClanTweakerPartySize
    {
        public static void Postfix(MobileParty party, StatExplainer explanation, ref int __result)
        {
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("PartySizeSettings");

			SkillObject leadership = SkillObject.FindFirst((SkillObject x) => x.Name.ToString() == "Leadership");
            SkillObject steward = SkillObject.FindFirst((SkillObject x) => x.Name.ToString() == "Steward");

			bool flag;
			if (settings.SelectSingleNode("AffectClanParties").InnerText == "true")
				flag = party.LeaderHero != null && (party.LeaderHero == Hero.MainHero || party.LeaderHero.Clan.Name == Hero.MainHero.Clan.Name);
			else
				flag = party.LeaderHero != null && party.LeaderHero == Hero.MainHero;

			if (flag)
            {
                int num = (int)Math.Ceiling((double)party.LeaderHero.GetSkillValue(leadership) * float.Parse(settings.SelectSingleNode("LeadershipBonus").InnerText));
                __result += num;
                if (explanation != null)
                {
                    explanation.AddLine("Leadership bonus", (float)num, StatExplainer.OperationType.Add);
                }
                num = (int)Math.Ceiling((double)party.LeaderHero.GetSkillValue(steward) * float.Parse(settings.SelectSingleNode("StewardBonus").InnerText));
                __result += num;
                if (explanation != null)
                {
                    explanation.AddLine("Steward bonus", (float)num, StatExplainer.OperationType.Add);
                }
            }
        }
    }

    //[HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyPrisonerSizeLimitInternal")]
    public class ClanTweakerPrisonerSize
    {
        public static void Postfix(PartyBase party, StatExplainer explanation, ref int __result)
		{
			XmlNode settings = ClanTweakerSubModule.settings.xmlSettings.ChildNodes[1].SelectSingleNode("PartySizeSettings");

			SkillObject leadership = SkillObject.FindFirst((SkillObject x) => x.Name.ToString() == "Leadership");
			SkillObject steward = SkillObject.FindFirst((SkillObject x) => x.Name.ToString() == "Steward");

			bool flag;
			if (settings.SelectSingleNode("AffectClanParties").InnerText == "true")
				flag = party.LeaderHero != null && (party.LeaderHero == Hero.MainHero || party.LeaderHero.Clan.Name == Hero.MainHero.Clan.Name);
			else
				flag = party.LeaderHero != null && party.LeaderHero == Hero.MainHero;


			if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                int num = (int)Math.Ceiling((double)party.LeaderHero.GetSkillValue(leadership) * float.Parse(settings.SelectSingleNode("LeadershipBonus").InnerText) * 0.25f);
                __result += num;
                if (explanation != null)
                {
                    explanation.AddLine("Leadership bonus", (float)num, StatExplainer.OperationType.Add);
                }
                num = (int)Math.Ceiling((double)party.LeaderHero.GetSkillValue(steward) * float.Parse(settings.SelectSingleNode("StewardBonus").InnerText) * 0.25f);
                __result += num;
                if (explanation != null)
                {
                    explanation.AddLine("Steward bonus", (float)num, StatExplainer.OperationType.Add);
                }
            }
        }
    }
}
