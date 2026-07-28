using System;
using System.IO;
using SIGEBI.Domain.Services;

namespace SIGEBI.Infrastructure.Logging
{
    // Implementacion concreta para registrar logs en archivo de texto plano
    public class FileLoggerService : ILoggerService
    {
        private readonly string _logFilePath;

        public FileLoggerService()
        {
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            _logFilePath = Path.Combine(logDirectory, $"sigebi_log_{DateTime.Now:yyyyMMdd}.txt");
        }

        public void LogInformation(string message)
        {
            WriteToFile($"[INFO] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }

        public void LogWarning(string message)
        {
            WriteToFile($"[WARN] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }

        public void LogError(string message, Exception ex = null)
        {
            var details = ex != null ? $" Exception: {ex.Message}" : "";
            WriteToFile($"[ERROR] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{details}");
        }

        private void WriteToFile(string logMessage)
        {
            try
            {
                File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
            }
            catch
            {
                // Ignorar excepciones de IO en ambiente de demostracion
            }
        }
    }
}
