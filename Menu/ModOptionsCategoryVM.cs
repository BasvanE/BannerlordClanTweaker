using System;
using System.Collections.Generic;
using HarmonyLib;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;

namespace ClanTweaker.Menu
{
	class ModOptionsCategoryVM : ViewModel
	{
		public ModOptionsCategoryVM(ModOptionsVM options, TextObject name, IEnumerable<IOptionData> targetList)
		{
			this._options = new MBBindingList<GenericOptionDataVM>();
			this._nameObj = name;

			foreach(IOptionData optionData in targetList)
			{

			}

			//this.RefreshValues();
		}

		public override void RefreshValues()
		{
			//base.RefreshValues();
			//this.Name = this._nameObj.ToString();
			//FileLog.Log(this.Name);
			//this.Options.ApplyActionOnAllItems(delegate (GenericOptionDataVM x)
			//{
			//	FileLog.Log(x.Name);
			//	x.RefreshValues();
			//});
		}

		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChanged("Name");
				}
			}
		}

		[DataSourceProperty]
		public MBBindingList<GenericOptionDataVM> Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != this._options)
				{
					this._options = value;
					base.OnPropertyChanged("Options");
				}
			}
		}

		private readonly TextObject _nameObj;

		public readonly bool IsNative;

		private string _name;

		private MBBindingList<GenericOptionDataVM> _options;
	}
}
