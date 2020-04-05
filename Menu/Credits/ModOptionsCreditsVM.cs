using System;
using System.IO;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace ClanTweaker.Menu
{
	class ModOptionsCreditsVM : ViewModel
	{
		public ModOptionsCreditsVM()
		{
			this.EscapeText = new TextObject("{=06V7fEV4}Press ESC to go back", null).ToString();
		}

		public void FillFromFile(string path)
		{
			try
			{
				if (File.Exists(path))
				{
					XmlDocument xmlDocument = new XmlDocument();
					using (XmlReader xmlReader = XmlReader.Create(path, new XmlReaderSettings
					{
						IgnoreComments = true
					}))
					{
						xmlDocument.Load(xmlReader);
					}
					ModOptionsCreditsItemVM rootItem = this.CreateItem(xmlDocument.FirstChild);
					this._rootItem = rootItem;
				}
			}
			catch (Exception ex)
			{
				Debug.Print("Could not load Credits xml. Exception: " + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
			}
		}

		public ModOptionsCreditsItemVM CreateItem(XmlNode node)
		{
			ModOptionsCreditsItemVM modOptionsCreditItemVM = new ModOptionsCreditsItemVM();
			modOptionsCreditItemVM.Type = node.Name;
			if (node.Attributes["Text"] != null)
			{
				modOptionsCreditItemVM.Text = node.Attributes["Text"].Value;
			}
			else
			{
				modOptionsCreditItemVM.Text = "";
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode node2 = (XmlNode)obj;
				ModOptionsCreditsItemVM item = this.CreateItem(node2);
				modOptionsCreditItemVM.Items.Add(item);
			}
			return modOptionsCreditItemVM;
		}

		[DataSourceProperty]
		public ModOptionsCreditsItemVM RootItem
		{
			get
			{
				return this._rootItem;
			}
			set
			{
				if (value != this._rootItem)
				{
					this._rootItem = value;
					base.OnPropertyChanged("RootItem");
				}
			}
		}

		[DataSourceProperty]
		public string EscapeText
		{
			get
			{
				return this._escapeText;
			}
			set
			{
				if (value != this._escapeText)
				{
					this._escapeText = value;
					base.OnPropertyChanged("EscapeText");
				}
			}
		}

		public ModOptionsCreditsItemVM _rootItem;

		private string _escapeText;
	}
}
