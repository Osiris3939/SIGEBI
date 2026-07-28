using System;
using SIGEBI.Domain.Services;

namespace SIGEBI.Infrastructure.Logging
{
    // Implementacion concreta para registrar logs en la consola del sistema
    public class ConsoleLoggerService : ILoggerService
    {
        public void LogInformation(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[INFO] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
            Console.ResetColor();
        }

        public void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARN] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
            Console.ResetColor();
        }

        public void LogError(string message, Exception ex = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
            if (ex != null)
            {
                Console.WriteLine($"[DETAILS] {ex.Message}");
            }
            Console.ResetColor();
        }
    }
}
