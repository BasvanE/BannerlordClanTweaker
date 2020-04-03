using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using SandBox.ViewModelCollection.Tournament;

namespace ClanTweaker
{
	class ClanTweakerSettings
	{
		public XmlDocument xmlSettings = new XmlDocument();

		public List<(MethodBase original, MethodInfo prefix, MethodInfo postfix)> toPatch = new List<(MethodBase original, MethodInfo prefix, MethodInfo postfix)>();

		public ClanTweakerSettings()
		{
			XmlReaderSettings readerSettings = new XmlReaderSettings();
			readerSettings.IgnoreComments = true;

			using (XmlReader reader = XmlReader.Create(BasePath.Name + "Modules/ClanTweaker/Config.xml", readerSettings))
			{
				xmlSettings.Load(reader);

				foreach (object obj in xmlSettings.SelectSingleNode("ClanTweaker").ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string text = xmlNode.Name;
					if (text != null)
					{
						#region Companions
						if (text == "CompanionSettings" && xmlNode.Attributes["enabled"].Value == "true")
						{
							var original = typeof(DefaultClanTierModel).GetMethod("GetCompanionLimitForTier");
							var postfix = typeof(ClanTweakerCompanionLimit).GetMethod("Postfix");
							toPatch.Add((original, null, postfix));
						}
						#endregion

						#region Party
						if (text == "PartySettings" && xmlNode.Attributes["enabled"].Value == "true")
						{
							var original = typeof(DefaultClanTierModel).GetMethod("GetPartyLimitForTier");
							var postfix = typeof(ClanTweakerPartyLimit).GetMethod("Postfix");
							toPatch.Add((original, null, postfix));
						}
						#endregion

						#region Workshop
						if (text == "WorkshopSettings" && xmlNode.Attributes["enabled"].Value == "true")
						{
							var original = typeof(DefaultWorkshopModel).GetMethod("GetMaxWorkshopCountForPlayer");
							var postfix = typeof(ClanTweakerWorkshop).GetMethod("Postfix");
							toPatch.Add((original, null, postfix));
						}
						#endregion

						#region Party Size
						if (text == "PartySizeSettings" && xmlNode.Attributes["enabled"].Value == "true")
						{
							var original = typeof(DefaultPartySizeLimitModel).GetMethod("CalculateMobilePartyMemberSizeLimit", BindingFlags.NonPublic | BindingFlags.Instance);
							var postfix = typeof(ClanTweakerPartySize).GetMethod("Postfix");
							toPatch.Add((original, null, postfix));

							original = typeof(DefaultPartySizeLimitModel).GetMethod("CalculateMobilePartyPrisonerSizeLimitInternal", BindingFlags.NonPublic | BindingFlags.Instance);
							postfix = typeof(ClanTweakerPrisonerSize).GetMethod("Postfix");
							toPatch.Add((original, null, postfix));
						}
						#endregion

						#region Crafting
						if (text == "CraftingSettings" && xmlNode.Attributes["enabled"].Value == "true")
						{
							var original = typeof(CraftingCampaignBehavior).GetMethod("GetMaxHeroCraftingStamina");
							var postfix = typeof(ClanTweakerCraftingMaxStamina).GetMethod("Postfix");
							toPatch.Add((original, null, postfix));

							original = typeof(CraftingCampaignBehavior).GetMethod("HourlyTick", BindingFlags.NonPublic | BindingFlags.Instance);
							var prefix = typeof(ClanTweakerCraftingStaminaGain).GetMethod("Prefix");
							toPatch.Add((original, prefix, null));
						}
						#endregion

						#region Tournament
						if (text == "TournamentSettings" && xmlNode.Attributes["enabled"].Value == "true")
						{
							var original = typeof(DefaultTournamentModel).GetMethod("GetRenownReward");
							var postfix = typeof(ClanTweakerTournamentRenown).GetMethod("Postfix");
							toPatch.Add((original, null, postfix));

							original = typeof(TournamentVM).GetMethod("RefreshBetProperties", BindingFlags.NonPublic | BindingFlags.Instance);
							postfix = typeof(ClanTweakerTournamentMaxBet).GetMethod("Postfix");
							toPatch.Add((original, null, postfix));
						}
						#endregion

						#region Xp
						if (text == "TroopXpSettings" && xmlNode.Attributes["enabled"].Value == "true")
						{
							var original = typeof(TroopRoster).GetMethod("AddXpToTroop");
							var prefix = typeof(ClanTweakerTroopExp).GetMethod("Prefix");
							toPatch.Add((original, prefix, null));
						}
						#endregion
					}
				}
			}
		}
	}
}
