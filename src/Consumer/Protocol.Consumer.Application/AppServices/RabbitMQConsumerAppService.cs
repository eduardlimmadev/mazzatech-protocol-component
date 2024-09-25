using Microsoft.Extensions.Logging;
using Protocol.Consumer.Application.AppServices.Interfaces;
using Protocol.Consumer.Infrastructure.Repositories.Interfaces;
using Protocol.Consumer.Service.Services.Interfaces;
using Protocol.Shared.Domain.Dtos;
using System.Text.Json;

namespace Protocol.Consumer.Application.AppServices
{
    public class RabbitMQConsumerAppService : IRabbitMQConsumerAppService
    {
        private readonly IProtocolRepository _protocolRepository;
        private readonly IValidationService _validationService;
        private readonly ILogger<RabbitMQConsumerAppService> _logger;

        public RabbitMQConsumerAppService(IProtocolRepository protocolRepository, IValidationService validationService, ILogger<RabbitMQConsumerAppService> logger)
        {
            _protocolRepository = protocolRepository;
            _validationService = validationService;
            _logger = logger;
        }

        public async Task<bool> HandleMessage(string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    _logger.LogInformation("RabbitConsumerAppService.ConsumeQueue - The message was not processed as it is empty or null. | Message consumed: {message}", message);
                    return false;
                }

                var protocol = JsonSerializer.Deserialize<ProtocolDto>(message);

                if (!_validationService.ValidateProtocol(protocol))
                {
                    _logger.LogInformation("RabbitConsumerAppService.ConsumeQueue - The message was not processes as it is invalid. | Message consumed: {message}", message);
                    return false;
                }

                var sent = await _protocolRepository.SendProtocol(protocol);

                _logger.LogInformation("RabbitConsumerAppService.ConsumeQueue - Message sent: {sent} | Message consumed: {message}", sent, message);

                return sent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitConsumerAppService.ConsumeQueue Error - Message consumed: {message}", message);
                return false;
            }
        }
    }
}
