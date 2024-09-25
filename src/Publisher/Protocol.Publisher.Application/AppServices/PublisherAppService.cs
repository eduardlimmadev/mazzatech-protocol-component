using Protocol.Publisher.Application.AppServices.Interfaces;
using Protocol.Publisher.Domain.Dtos;
using Protocol.Publisher.Infrastructure.Repositories.Interfaces;
using Protocol.Publisher.Service.Services.Interfaces;
using Protocol.Publisher.Shared.FlowControl.Models;
using Protocol.Shared.Domain.Dtos;
using System.Xml.Linq;

namespace Protocol.Publisher.Application.AppServices
{
    public class PublisherAppService : IPublisherAppService
    {
        private readonly IRabbitMQRepository _rabbitMQRepository;
        private readonly IValidationService _validationService;
        private readonly IFileRepository _fileRepository;

        public PublisherAppService(IRabbitMQRepository rabbitMQRepository, IValidationService validationService, IFileRepository fileRepository)
        {
            _rabbitMQRepository = rabbitMQRepository;
            _validationService = validationService;
            _fileRepository = fileRepository;
        }

        public async Task<SimpleResult> PublishMessageAsync(PublishProtocolDto publishProtocol)
        {
            var validationPublishResult = await _validationService.ValidateAsync(publishProtocol);

            if (validationPublishResult.HasError)
                return SimpleResult.Fail(validationPublishResult.Errors);

            var fileUpload = await _fileRepository.UploadFile(publishProtocol.Photo);
            if (fileUpload == null)
                throw new Exception("An error occurred while trying to save the file");

            var protocol = new ProtocolDto
            {
                ProtocolNumber = publishProtocol.ProtocolNumber,
                ViaNumber = publishProtocol.ViaNumber,
                Cpf = publishProtocol.Cpf,
                Rg = publishProtocol.Rg,
                Name = publishProtocol.Name,
                MotherName = publishProtocol.MotherName,
                FatherName = publishProtocol.FatherName,
                PhotoId = (Guid)fileUpload
            };

            var validationResult = await _validationService.ValidateAsync(protocol);

            if (validationResult.HasError)
                return SimpleResult.Fail(validationResult.Errors);

            return _rabbitMQRepository.PublishMessage(protocol);
        }
    }
}
