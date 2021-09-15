using Application.Contracts;
using NLog;

namespace Application.Services
{
    public class LoggerService : ILoggerService
    {
        //Libreria flexible para muchas plataformas net, muchas opciones de configuracion de logging
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
    }
}