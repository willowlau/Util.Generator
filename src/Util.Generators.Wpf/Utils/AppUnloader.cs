using System;
using System.Diagnostics;

namespace Util.Generators.Utils
{
    public static class AppUnloader
    {
        public static void Unload()
        {
            var mins = Convert.ToInt32((DateTime.Now - Process.GetCurrentProcess().StartTime).TotalMinutes);

            try
            {
            }
            catch
            {
            }
        }
    }
}