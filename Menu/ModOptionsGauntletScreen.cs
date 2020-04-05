using System;
using System.Linq;
using reflection = System.Reflection;
using HarmonyLib;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.GauntletUI;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GameKeys;
using TaleWorlds.GauntletUI.Data;

namespace ClanTweaker.Menu
{
	[OverrideView(typeof(ModOptionsScreen))]
	public class ModOptionsGauntletScreen : ScreenBase
	{
		protected override void OnInitialize()
		{
			base.OnInitialize();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._dataSource = new ModOptionsVM(true, false, new Action<GameKeyOptionVM>(this.OnKeybindRequest), null);
			this._gauntletLayer = new GauntletLayer(4000, "GauntletLayer");
			this._gauntletMovie = this._gauntletLayer.LoadMovie("ModOptions", this._dataSource);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.IsFocusLayer = true;
			base.AddLayer(this._gauntletLayer);
			ScreenManager.TrySetFocus(this._gauntletLayer);
		}

		private void OnKeybindRequest(GameKeyOptionVM requestedHotKeyToChange)
		{
			this._currentGameKey = requestedHotKeyToChange;
			this._keybindingPopup.OnToggle(true);
		}

		private GauntletLayer _gauntletLayer;

		private ModOptionsVM _dataSource;

		private GauntletMovie _gauntletMovie;

		private KeybindingPopup _keybindingPopup;

		private GameKeyOptionVM _currentGameKey;
	}
}
