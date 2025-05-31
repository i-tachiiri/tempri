using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintSiteBuilder.Models;


namespace PrintSiteBuilder.Utilities
{
    public class Bat
    {

        public void RunBat(string fileName, bool IsWinScp)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = $@"C:\drive\work\task-schedular\{fileName}";
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.CreateNoWindow = false;
            if (IsWinScp)
            {
                processStartInfo.StandardOutputEncoding = Encoding.UTF8;
                processStartInfo.StandardErrorEncoding = Encoding.UTF8;
            }
            using (Process process = Process.Start(processStartInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (!string.IsNullOrEmpty(error))
                {
                    output = error;
                }
                var directory = $@"{GlobalConfig.LogDir}\{fileName}";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText($@"{directory}\{DateTime.Now.ToString("yyyyMMdd-hhmmss")}.txt", output);
            }
        }

    }
}
