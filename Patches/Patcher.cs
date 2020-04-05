using System;
using reflection = System.Reflection;
using System.Windows.Forms;
using HarmonyLib;

namespace ClanTweaker
{
	public static class Patcher
	{
		public static void PatchAll()
		{
			Harmony harmony = new Harmony("mod.bannerlord.tweaker");
			foreach ((reflection.MethodBase original, reflection.MethodInfo prefix, reflection.MethodInfo postfix) patch in ClanTweakerSubModule.settings.toPatch)
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
