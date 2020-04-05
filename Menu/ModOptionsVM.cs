using System;
using System.Reflection;
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
using System.Collections.Generic;
using TaleWorlds.Engine.Options;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GamepadOptions;
using TaleWorlds.MountAndBlade.Options.ManagedOptions;

namespace ClanTweaker.Menu
{
	class ModOptionsVM : OptionsVM
	{
		public ModOptionsVM(bool autoHandleClose, bool openedFromMultiplayer, Action<GameKeyOptionVM> onKeybindRequest, Action onBrightnessExecute = null)
			: base(autoHandleClose, openedFromMultiplayer, onKeybindRequest, onBrightnessExecute)
		{
			this._autoHandleClose = autoHandleClose;
			this._genericCategories = new List<ModOptionsCategoryVM>();
			this._modOptionCategory = new ModOptionsCategoryVM(this, new TextObject("{=JKCounzF}Test", null), ModOptionsList);
			this._genericCategories.Add(this._modOptionCategory);
			this.RefreshValues();
			this._isInitialized = true;
		}

		public override void RefreshValues()
		{
			//this.OptionsLbl = new TextObject("{=NqarFr4P}Mod Options", null).ToString();
			//this.CancelLbl = new TextObject("{=3CpNUnVl}Cancel", null).ToString();
			//this.DoneLbl = new TextObject("{=WiNRdfsm}Done", null).ToString();
			//this.ResetLbl = new TextObject("{=mAxXKaXp}Reset", null).ToString();

			this.ModOptions.RefreshValues();
		}

		private IEnumerable<IOptionData> ModOptionsList
		{
			get
			{
				ManagedBooleanOptionData foo = new ManagedBooleanOptionData((ManagedOptions.ManagedOptionsType)ManagedModOptions.ModOptionsType.FeatureToggle);
				yield return foo;
			}
		}

		[DataSourceProperty]
		public ModOptionsCategoryVM ModOptions
		{
			get
			{
				return this._modOptionCategory;
			}
		}

		protected void ExecuteDone()
		{
			base.ExecuteCloseOptions();
			if (this._autoHandleClose)
			{
				if (Game.Current != null)
				{
					Game.Current.GameStateManager.ActiveStateDisabledByUser = this.OldGameStateManagerDisabledStatus;
				}
				ScreenManager.PopScreen();
			}
			return;
		}

		[DataSourceProperty]
		public string OptionsLbl
		{
			get
			{
				return this._optionsLbl;
			}
			set
			{
				if (value != this._optionsLbl)
				{
					this._optionsLbl = value;
					base.OnPropertyChanged("OptionsLbl");
				}
			}
		}

		[DataSourceProperty]
		public string CancelLbl
		{
			get
			{
				return this._cancelLbl;
			}
			set
			{
				if (value != this._cancelLbl)
				{
					this._cancelLbl = value;
					base.OnPropertyChanged("CancelLbl");
				}
			}
		}

		[DataSourceProperty]
		public string DoneLbl
		{
			get
			{
				return this._doneLbl;
			}
			set
			{
				if (value != this._doneLbl)
				{
					this._doneLbl = value;
					base.OnPropertyChanged("DoneLbl");
				}
			}
		}

		[DataSourceProperty]
		public string ResetLbl
		{
			get
			{
				return this._resetLbl;
			}
			set
			{
				if (value != this._resetLbl)
				{
					this._resetLbl = value;
					base.OnPropertyChanged("ResetLbl");
				}
			}
		}

		private readonly Action _onClose;

		private bool _autoHandleClose;

		private bool _isInitialized;

		private bool _isCancelling;

		private string _optionsLbl;

		private string _cancelLbl;

		private string _doneLbl;

		private string _resetLbl;

		private readonly ModOptionsCategoryVM _modOptionCategory;

		private readonly List<ModOptionsCategoryVM> _genericCategories;
	}
}
