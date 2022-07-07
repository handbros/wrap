using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Wrap.Windows
{
    /// <summary>
    /// Provides functions related to Windows user privilege.
    /// </summary>
    public static class PrivilegeHelper
    {
        /// <summary>
        /// Checks that the current process is run with administrator privileges.
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (identity != null)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return false;
        }

        /// <summary>
        /// Restarts current process as administrator privilege.
        /// </summary>
        public static void RunAsAdiministrator()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.FileName = Process.GetCurrentProcess().MainModule?.FileName;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.Verb = "runas";

                Process.Start(startInfo);

                Environment.Exit(0);
            }
            catch
            {
                throw;
            }
        }
    }
}
