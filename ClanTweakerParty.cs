using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;

namespace ClanTweaker
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    class ClanTweakerPartySize
    {
        private static void Postfix(MobileParty party, StatExplainer explanation, ref int __result)
        {
            float leadershipBonus = 0;
            float stewardBonus = 0;

            if (ClanTweakerSubModule.settings.partySizeMode == "modified")
            {
                leadershipBonus = ClanTweakerSubModule.settings.leadershipModifier;
                stewardBonus = ClanTweakerSubModule.settings.stewardModifier;
            }

            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                SkillObject skillObject = SkillObject.FindFirst((SkillObject x) => x.Name.ToString() == "Leadership");
                int num = (int)Math.Ceiling((double)party.LeaderHero.GetSkillValue(skillObject) * leadershipBonus);
                __result += num;
                if (explanation != null)
                {
                    explanation.AddLine("Leadership bonus", (float)num, StatExplainer.OperationType.Add);
                }
                skillObject = SkillObject.FindFirst((SkillObject x) => x.Name.ToString() == "Steward");
                num = (int)Math.Ceiling((double)party.LeaderHero.GetSkillValue(skillObject) * stewardBonus);
                __result += num;
                if (explanation != null)
                {
                    explanation.AddLine("Steward bonus", (float)num, StatExplainer.OperationType.Add);
                }
            }
        }
    }

    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyPrisonerSizeLimitInternal")]
    class ClanTweakerPrisonerSize
    {
        private static void Postfix(PartyBase party, StatExplainer explanation, ref int __result)
        {
            float leadershipBonus = 0;
            float stewardBonus = 0;

            if (ClanTweakerSubModule.settings.partySizeMode == "modified")
            {
                leadershipBonus = ClanTweakerSubModule.settings.leadershipModifier * 0.25f;
                stewardBonus = ClanTweakerSubModule.settings.stewardModifier * 0.25f;
            }

            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                SkillObject skillObject = SkillObject.FindFirst((SkillObject x) => x.Name.ToString() == "Leadership");
                int num = (int)Math.Ceiling((double)party.LeaderHero.GetSkillValue(skillObject) * leadershipBonus);
                __result += num;
                if (explanation != null)
                {
                    explanation.AddLine("Leadership bonus", (float)num, StatExplainer.OperationType.Add);
                }
                skillObject = SkillObject.FindFirst((SkillObject x) => x.Name.ToString() == "Steward");
                num = (int)Math.Ceiling((double)party.LeaderHero.GetSkillValue(skillObject) * stewardBonus);
                __result += num;
                if (explanation != null)
                {
                    explanation.AddLine("Steward bonus", (float)num, StatExplainer.OperationType.Add);
                }
            }
        }
    }
}
