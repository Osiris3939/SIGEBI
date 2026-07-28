using System;
using System.Collections.Generic;
using SIGEBI.Domain.Services;

namespace SIGEBI.Infrastructure.Logging
{
    // Demostracion de POLIMORFISMO: despacha llamadas a multiples servicios de logging
    public class CompositeLoggerService : ILoggerService
    {
        private readonly IEnumerable<ILoggerService> _loggers;

        public CompositeLoggerService(IEnumerable<ILoggerService> loggers)
        {
            _loggers = loggers ?? new List<ILoggerService>();
        }

        public void LogInformation(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.LogInformation(message);
            }
        }

        public void LogWarning(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.LogWarning(message);
            }
        }

        public void LogError(string message, Exception ex = null)
        {
            foreach (var logger in _loggers)
            {
                logger.LogError(message, ex);
            }
        }
    }
}
