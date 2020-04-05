using TaleWorlds.Library;


namespace ClanTweaker.Menu
{
	class ModOptionsCreditsItemVM : ViewModel
	{
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChanged("Text");
				}
			}
		}

		[DataSourceProperty]
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != this._type)
				{
					this._type = value;
					base.OnPropertyChanged("Type");
				}
			}
		}

		[DataSourceProperty]
		public MBBindingList<ModOptionsCreditsItemVM> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				if (value != this._items)
				{
					this._items = value;
					base.OnPropertyChanged("Items");
				}
			}
		}

		public ModOptionsCreditsItemVM()
		{
			this._items = new MBBindingList<ModOptionsCreditsItemVM>();
			this.Type = "Entry";
			this.Text = "";
		}

		private string _text;

		private string _type;

		private MBBindingList<ModOptionsCreditsItemVM> _items;
	}
}
