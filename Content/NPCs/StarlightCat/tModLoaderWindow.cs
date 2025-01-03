using System;
using System.IO;
using System.Windows.Forms;
using Terraria;

namespace ssm
{
    internal class WinForm
    {
        internal static bool Win
        {
            get
            {
                if (Main.dedServ || ssm.OS > 0 || Environment.Is64BitProcess)
                {
                    return false;
                }
                bool v = false;
                return v;
            }
        }
    }
}