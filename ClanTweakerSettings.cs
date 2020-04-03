using System.Xml;
using TaleWorlds.Library;

namespace ClanTweaker
{
    class ClanTweakerSettings
    {
        public string companionMode;
        public int companionModifier, companionIncrease, companionFixed;

        public string partyMode;
        public int partyModifier, partyIncrease, partyFixed;

        public string workshopMode;
        public int workshopModifier, workshopIncrease, workshopFixed;

        public string partySizeMode;
        public float leadershipModifier, stewardModifier;

        public int craftingMaxStamina, craftingStaminaGain;

        public int tournamentRenown, tournamentMaxBet;

        public float troopXpModifier;

        public ClanTweakerSettings()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(BasePath.Name + "Modules/ClanTweaker/Config.xml");

            foreach (object obj in xmlDocument.SelectSingleNode("ClanTweaker").ChildNodes)
            {
                XmlNode xmlNode = (XmlNode)obj;
                string text = xmlNode.Name.ToLower();
                if (text != null)
                {
                    #region Companions
                    if (text == "companionmode")
                    {
                        if (xmlNode.InnerText == "modifier") { this.companionMode = "modifier"; }
                        else if (xmlNode.InnerText == "increase") { this.companionMode = "increase"; }
                        else if (xmlNode.InnerText == "fixed") { this.companionMode = "fixed"; }
                        else { this.companionMode = "default"; }
                    }
                    if (text == "companionlimitmodifier") { this.companionModifier = int.Parse(xmlNode.InnerText); }
                    else if (text == "companionlimitincrease") { this.companionIncrease = int.Parse(xmlNode.InnerText); }
                    else if (text == "companionlimitfixed") { this.companionFixed = int.Parse(xmlNode.InnerText); }
                    #endregion

                    #region Party
                    if (text == "partymode")
                    {
                        if (xmlNode.InnerText == "modifier") { this.partyMode = "modifier"; }
                        else if (xmlNode.InnerText == "increase") { this.partyMode = "increase"; }
                        else if (xmlNode.InnerText == "fixed") { this.partyMode = "fixed"; }
                        else { this.partyMode = "default"; }
                    }
                    if (text == "partylimitmodifier") { this.partyModifier = int.Parse(xmlNode.InnerText); }
                    else if (text == "partylimitincrease") { this.partyIncrease = int.Parse(xmlNode.InnerText); }
                    else if (text == "partylimitfixed") { this.partyFixed = int.Parse(xmlNode.InnerText); }
                    #endregion

                    #region Workshop
                    if (text == "workshopmode")
                    {
                        if (xmlNode.InnerText == "modifier") { this.workshopMode = "modifier"; }
                        else if (xmlNode.InnerText == "increase") { this.workshopMode = "increase"; }
                        else if (xmlNode.InnerText == "fixed") { this.workshopMode = "fixed"; }
                        else { this.workshopMode = "default"; }
                    }
                    if (text == "workshoplimitmodifier") { this.workshopModifier = int.Parse(xmlNode.InnerText); }
                    else if (text == "workshoplimitincrease") { this.workshopIncrease = int.Parse(xmlNode.InnerText); }
                    else if (text == "workshoplimitfixed") { this.workshopFixed = int.Parse(xmlNode.InnerText); }
                    #endregion

                    #region Party Size
                    if (text == "partysizemode")
                    {
                        if (xmlNode.InnerText == "modified") { this.partySizeMode = "modified"; }
                    }
                    if (text == "leadershipbonus") { this.leadershipModifier = float.Parse(xmlNode.InnerText); }
                    else if (text == "stewardbonus") { this.stewardModifier = float.Parse(xmlNode.InnerText); }
                    #endregion

                    #region Crafting
                    if (text == "craftingmaxstamina") { this.craftingMaxStamina = int.Parse(xmlNode.InnerText); }
                    if (text == "craftingstaminagain") { this.craftingStaminaGain = int.Parse(xmlNode.InnerText); }
                    #endregion

                    #region Tournament
                    if (text == "tournamentrenowngain") { this.tournamentRenown = int.Parse(xmlNode.InnerText); }
                    if (text == "tournamentmaxbet") { this.tournamentMaxBet = int.Parse(xmlNode.InnerText); }
                    #endregion

                    #region Xp
                    if (text == "troopxpmodifier") { this.troopXpModifier = float.Parse(xmlNode.InnerText); }
                    #endregion 
                }
            }
        }
    }
}
