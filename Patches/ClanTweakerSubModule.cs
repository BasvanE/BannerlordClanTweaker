using System;
using HarmonyLib;
using ClanTweaker.Menu;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View.Missions;


namespace ClanTweaker
{
    class ClanTweakerSubModule : MBSubModuleBase
    {
		public static ClanTweakerSettings settings = new ClanTweakerSettings();

		protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

			Harmony.DEBUG = true;

			if (Module.CurrentModule.StartupInfo.StartupType == GameStartupType.Singleplayer)
			{
				Module.CurrentModule.AddInitialStateOption(new InitialStateOption("Mod Settings", new TextObject("{=NqarFr4P}Mod Settings", null), 9998, delegate ()
				{
					ScreenManager.PushScreen(ViewCreatorManager.CreateScreenView<ModOptionsScreen>());
				}, false));
			}
		}
	}
}
