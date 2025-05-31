using System.Diagnostics;
using System.Text;

namespace ExplorerLibrary.Services
{
    public class BatService
    {
        public void ExecuteBat(string batFilePath)
        {
            if (!File.Exists(batFilePath))
                throw new FileNotFoundException("バッチファイルが見つかりません", batFilePath);

            var output = new StringBuilder();
            var error = new StringBuilder();

            var startInfo = new ProcessStartInfo
            {
                FileName = batFilePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(batFilePath)!
            };

            using var process = new Process { StartInfo = startInfo };
            process.OutputDataReceived += (_, e) => { if (e.Data != null) output.AppendLine(e.Data); };
            process.ErrorDataReceived += (_, e) => { if (e.Data != null) error.AppendLine(e.Data); };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (process.ExitCode != 0)
                throw new InvalidOperationException($"バッチが異常終了しました。ExitCode={process.ExitCode}");

            //return new BatExecutionResult(process.ExitCode, output.ToString(), error.ToString());
        }


        public void GenerateAndExecuteBat(string batTemplateName, int printId)
        {
            try
            {
                // テンプレートファイルのパス
                string templateFilePath = $@"C:\drive\work\batch\{batTemplateName}";
                if (!File.Exists(templateFilePath))
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Error: テンプレートバッチファイルが見つかりません -> {templateFilePath}");
                    return;
                }

                // 一時的なバッチファイルのパス
                string tempDir = @"C:\drive\work\task-schedular\temp";
                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }

                string newFilePath = Path.Combine(tempDir, $"{DateTime.Now:yyyyMMddHHmmss}.bat");

                // テンプレートバッチを読み込んでプレースホルダーを置換
                string scriptContent = File.ReadAllText(templateFilePath);
                scriptContent = scriptContent.Replace("{ID}", printId.ToString());

                    // ✅ Shift-JIS (ANSI) で保存
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    Encoding shiftJisEncoding = Encoding.GetEncoding("Shift_JIS");
                File.WriteAllText(newFilePath, scriptContent, shiftJisEncoding);

                // バッチを実行（ExecuteBat を呼び出す）
                ExecuteBat(newFilePath);

                // 実行後にバッチファイルを削除
                File.Delete(newFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 実行中にエラーが発生しました: {ex.Message}");
            }
        }
    }
}
