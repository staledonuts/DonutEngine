using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace DonutEngine.Utilities
{
    /// <summary>
    /// Defines the severity levels for log messages.
    /// </summary>
    public enum LogLevel
    {
        Debug,   // Detailed information for developers.
        Info,    // General information about application progress.
        Warning, // Indicates a potential issue that doesn't prevent operation.
        Error    // An error that prevents a specific operation from completing.
    }
    
    /// <summary>
    /// Represents a single, structured log entry for serialization to JSON.
    /// </summary>
    internal class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string MemberName { get; set; }
        public string FilePath { get; set; }
        public int LineNumber { get; set; }
        public object ExceptionInfo { get; set; }
    }

    /// <summary>
    /// A static logger system for writing timestamped logs to the console and a structured JSON file.
    /// </summary>
    public static class Logger
    {
        private static readonly object _fileLock = new object();
        private static string _logFilePath;
        private static LogLevel _consoleLogLevel = LogLevel.Debug;
        private static LogLevel _fileLogLevel = LogLevel.Info;

        /// <summary>
        /// Initializes the logger system. Must be called once at application startup.
        /// </summary>
        /// <param name="logDirectory">The directory where the log file will be created.</param>
        /// <param name="consoleLogLevel">The minimum level of logs to show in the console.</param>
        /// <param name="fileLogLevel">The minimum level of logs to write to the file.</param>
        public static void Initialize(string logDirectory = "Logs", LogLevel consoleLogLevel = LogLevel.Debug, LogLevel fileLogLevel = LogLevel.Info)
        {
            try
            {
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                // The log file is now a .jsonl file (JSON Lines)
                _logFilePath = Path.Combine(logDirectory, $"DonutEngine_Log_{timestamp}.jsonl");
                
                _consoleLogLevel = consoleLogLevel;
                _fileLogLevel = fileLogLevel;

                Info("--- Logger Initialized ---");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FATAL ERROR] Failed to initialize logger: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Logs a standard informational message.
        /// </summary>
        public static void Info(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) 
            => Log(LogLevel.Info, message, memberName, sourceFilePath, sourceLineNumber);

        /// <summary>
        /// Logs a warning message. Indicates a potential issue.
        /// </summary>
        public static void Warning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => Log(LogLevel.Warning, message, memberName, sourceFilePath, sourceLineNumber);
        
        /// <summary>
        /// Logs a debug message. Only shown if the log level is set to Debug.
        /// </summary>
        public static void Debug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => Log(LogLevel.Debug, message, memberName, sourceFilePath, sourceLineNumber);


        /// <summary>
        /// Logs an error message.
        /// </summary>
        public static void Error(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => Log(LogLevel.Error, message, memberName, sourceFilePath, sourceLineNumber);

        /// <summary>
        /// Logs an error message from an Exception, automatically including the stack trace.
        /// </summary>
        public static void Error(Exception ex, string message = "", [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            // For exceptions, we create a more detailed, structured object for serialization.
            var exceptionInfo = new
            {
                Type = ex.GetType().FullName,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            };
            
            Log(LogLevel.Error, string.IsNullOrEmpty(message) ? ex.Message : message, memberName, sourceFilePath, sourceLineNumber, exceptionInfo);
        }

        private static void Log(LogLevel level, string message, string memberName, string sourceFilePath, int sourceLineNumber, object exceptionInfo = null)
        {
            // Write to console if the level is high enough.
            // Console output remains human-readable plain text.
            if (level >= _consoleLogLevel)
            {
                string consoleMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [{level.ToString().ToUpper()}] {message}";
                if (level >= LogLevel.Warning)
                {
                    consoleMessage += $" (at {Path.GetFileName(sourceFilePath)}:{sourceLineNumber})";
                }
                WriteToConsole(level, consoleMessage);
            }

            // Write to file if the level is high enough.
            if (!string.IsNullOrEmpty(_logFilePath) && level >= _fileLogLevel)
            {
                var logEntry = new LogEntry
                {
                    Timestamp = DateTime.UtcNow, // Use UTC for logs
                    Level = level.ToString(),
                    Message = message,
                    MemberName = memberName,
                    FilePath = sourceFilePath,
                    LineNumber = sourceLineNumber,
                    ExceptionInfo = exceptionInfo
                };
                WriteToFile(logEntry);
            }
        }

        private static void WriteToConsole(LogLevel level, string logEntry)
        {
            Console.ForegroundColor = level switch
            {
                LogLevel.Debug => ConsoleColor.Gray,
                LogLevel.Info => ConsoleColor.White,
                LogLevel.Warning => ConsoleColor.Yellow,
                LogLevel.Error => ConsoleColor.Red,
                _ => ConsoleColor.White
            };

            Console.WriteLine(logEntry);
            Console.ResetColor();
        }

        private static void WriteToFile(LogEntry logEntry)
        {
            try
            {
                // Serialize the entire LogEntry object to a single line of JSON.
                // Formatting.None ensures it's a compact single line.
                string jsonEntry = JsonConvert.SerializeObject(logEntry, Formatting.None);

                lock (_fileLock)
                {
                    File.AppendAllText(_logFilePath, jsonEntry + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"[LOGGER FILE ERROR] Failed to write to log file: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}