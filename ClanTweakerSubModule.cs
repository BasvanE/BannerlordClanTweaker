using System;
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
            try
            {
                Harmony harmony = new Harmony("mod.bannerlord.tweaker");
                harmony.PatchAll();
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
