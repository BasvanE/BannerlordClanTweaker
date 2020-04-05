using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.GauntletUI.Data;

namespace ClanTweaker.Menu
{
	[OverrideView(typeof(ModOptionsCreditsScreen))]
	public class ModOptionsCreditsGauntletScreen : ScreenBase
	{
		protected override void OnInitialize()
		{
			base.OnInitialize();
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._dataSource = new ModOptionsCreditsVM();
			string path = BasePath.Name + "Modules/ClanTweaker/ModuleData/" + "modTest.xml";
			this._dataSource.FillFromFile(path);

			this._gauntletLayer = new GauntletLayer(100, "GauntletLayer");
			this._gauntletLayer.IsFocusLayer = true;
			base.AddLayer(this._gauntletLayer);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			ScreenManager.TrySetFocus(this._gauntletLayer);

			this._gauntletMovie = this._gauntletLayer.LoadMovie("CreditsScreen", this._dataSource);
			ScreenManager.TrySetFocus(this._gauntletLayer);
		}

		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (Input.IsKeyReleased(InputKey.Escape))
			{
				ScreenManager.PopScreen();
			}
			if (base.DebugInput.IsHotKeyPressed("F1"))
			{
				InformationManager.AddQuickInformation(new TextObject("{=!}Deneme 1 2 3", null), 0, null, "");
			}
			if (base.DebugInput.IsHotKeyPressed("F2"))
			{
				InformationManager.AddQuickInformation(new TextObject("{=!}sth sgege4t 34t2r3adswd QDef sadgbdd fhfgbfg h 1 2 3", null), 0, null, "");
			}
			if (base.DebugInput.IsHotKeyPressed("F3"))
			{
				SoundEvent.PlaySound2D("event:/ui/mission/deploy");
			}
			if (base.DebugInput.IsHotKeyPressed("F4"))
			{
				SoundEvent.PlaySound2D("event:/ui/default");
			}
		}

		protected override void OnDeactivate()
		{
			LoadingWindow.EnableGlobalLoadingWindow(false);
		}

		private GauntletLayer _gauntletLayer;

		private ModOptionsCreditsVM _dataSource;

		private GauntletMovie _gauntletMovie;
	}
}
