using System.Runtime.InteropServices;
using TempriDomain.Entity;
using TempriDomain.Interfaces;
using PrintGenerater.Interfaces.Infrastructure;

namespace ConsoleLibrary.Repository
{
    public class ConsoleRepository : IConsoleRepository
    {
        private static readonly object _lock = new object();
        private int _count;
        private string _lastId;

        public void WriteLog(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string logMessage = $"[{timestamp}] {message}";

            // コンソール出力のみ
            lock (_lock)
            {
                Console.WriteLine(logMessage);
            }
        }
        public void ActivateConsole()
        {
            SetForegroundWindow(GetConsoleWindow());
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public async Task TaskLog(Func<Task> task)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string taskName = task.Method.Name;
            WriteLog($"[Task][{timestamp}] {taskName} ...");
            try
            {
                await task();
            }
            catch (Exception ex)
            {
                WriteLog($"[Task][{timestamp}] {taskName} : {ex.Message}");
            }
        }

        public void TaskLog(Action<IPrintMasterEntity> task, IPrintMasterEntity entity)
        {
            string taskName = task.Method.Name;
            WriteLog($"[{entity.PrintId}] {taskName} ...");
            try
            {
                task(entity);
            }
            catch (Exception ex)
            {
                WriteLog($"[{entity.PrintId}][error] {taskName} : {ex.Message}");
            }
        }
        public void TaskLog(Action<IPrintMasterEntity, bool> task, IPrintMasterEntity entity, bool IsTestCase = false)
        {
            string taskName = task.Method.Name;
            WriteLog($"[{entity.PrintId}] {taskName} ...");
            try
            {
                task(entity, IsTestCase);
            }
            catch (Exception ex)
            {
                WriteLog($"[{entity.PrintId}][error] {taskName} : {ex.Message}");
            }
        }
        public async Task TaskLog(Func<IPrintMasterEntity, Task> task, IPrintMasterEntity entity)
        {
            string taskName = task.Method.Name;
            WriteLog($"[{entity.PrintId}] {taskName} ...");
            try
            {
                await task(entity);
            }
            catch (Exception ex)
            {
                WriteLog($"[{entity.PrintId}][error] {taskName} : {ex.Message}");
            }
        }
        public async Task TaskLog(Func<IPrintMasterEntity, bool, Task> task, IPrintMasterEntity entity, bool IsTestCase = false)
        {
            string taskName = task.Method.Name;
            string ExecutionMode = IsTestCase ? "[test]" : "";
            WriteLog($"[{entity.PrintId}]{ExecutionMode} {taskName} ...");
            try
            {
                await task(entity, IsTestCase);
            }
            catch (Exception ex)
            {
                WriteLog($"[{entity.PrintId}][error] {taskName} : {ex.Message}");
            }
        }
        public void ExceptionLog(string message, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
        {
            WriteLog($"[Error] {methodName}: {message}");
        }

        public void LoopLog(string loopId)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            lock (_lock)
            {
                if (loopId == _lastId)
                {
                    _count++;
                }
                else
                {
                    _lastId = loopId;
                    _count = 1;
                }
                WriteLog($"[Loop] {_count}/X {loopId}");
            }
        }

        public void LoopLog(string loopId, int loopCount)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            lock (_lock)
            {
                if (loopId == _lastId)
                {
                    _count++;
                }
                else
                {
                    _lastId = loopId;
                    _count = 1;
                }
                WriteLog($"[Loop]{_count}/{loopCount} {loopId}");
            }
        }

        public void LoopLog(string loopId, int loopCount, string additionalInfo)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            lock (_lock)
            {
                if (loopId == _lastId)
                {
                    _count++;
                }
                else
                {
                    _lastId = loopId;
                    _count = 1;
                }
                WriteLog($"[Loop][{timestamp}] {_count}/{loopCount} {loopId}: {additionalInfo}");
            }
        }
        public string? WriteAndRead(IEnumerable<string> lines, bool isReadLine)
        {
            lock (_lock)
            {
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }

                if (isReadLine)
                {
                    return Console.ReadLine();
                }

                return null;
            }
        }
        public void ExceptionLog(string message)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}
