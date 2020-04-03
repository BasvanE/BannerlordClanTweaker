using System;
using System.Reflection;
using System.Windows.Forms;
using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace ClanTweaker
{
    class ClanTweakerSubModule : MBSubModuleBase
    {
        public static ClanTweakerSettings settings = new ClanTweakerSettings();

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
			Harmony.DEBUG = true;
			FileLog.Reset();
			Harmony harmony = new Harmony("mod.bannerlord.tweaker");
			foreach ((MethodBase original, MethodInfo prefix, MethodInfo postfix) patch in settings.toPatch)
			{
				try
				{
					if (patch.original == null)
						continue;
					if (patch.prefix == null)
						harmony.Patch(patch.original, postfix: new HarmonyMethod(patch.postfix));
					else if (patch.postfix == null)
						harmony.Patch(patch.original, prefix: new HarmonyMethod(patch.prefix));
					else
						harmony.Patch(patch.original, postfix: new HarmonyMethod(patch.postfix), prefix: new HarmonyMethod(patch.prefix));
				}
				catch (Exception ex)
				{
					string str = "Error patching:\n";
					string message = ex.Message;
					string str2 = " \n\n";
					Exception innerException = ex.InnerException;
					MessageBox.Show(str + message + str2 + ((innerException != null) ? innerException.Message : null));

				}
			}
		}
    }
}
