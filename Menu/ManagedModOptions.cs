using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Options.ManagedOptions;

namespace ClanTweaker.Menu
{
	public class ManagedModOptions
	{
		public static float GetConfig(ManagedModOptions.ModOptionsType type)
		{
			return 0.0f;
		}

		public enum ModOptionsType
		{
			FeatureToggle = ManagedOptions.ManagedOptionsType.ManagedOptionTypeCount + 1,
			Selection,
			Slider,
			Percentage
		}
	}
}
