using FluentValidation;
using Protocol.Publisher.Domain.Dtos;
using Protocol.Publisher.Service.Services.Interfaces;
using Protocol.Publisher.Shared.FlowControl.Enums;
using Protocol.Publisher.Shared.FlowControl.Models;
using Protocol.Shared.Domain.Dtos;

namespace Protocol.Publisher.Service.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IValidator<ProtocolDto> _protocolValidator;
        private readonly IValidator<PublishProtocolDto> _publishProtocolValidator;

        public ValidationService(IValidator<ProtocolDto> protocolValidator, IValidator<PublishProtocolDto> publishProtocolValidator)
        {
            _protocolValidator = protocolValidator;
            _publishProtocolValidator = publishProtocolValidator;
        }

        public async Task<SimpleResult> ValidateAsync(ProtocolDto protocol)
        {
            var isEmptyResult = IsEmpty(protocol);
            if (isEmptyResult.HasError)
                return isEmptyResult;

            var errors = new List<Error>();

            var validationResult = await _protocolValidator.ValidateAsync(protocol);
            foreach (var fluentValidationError in validationResult.Errors)
            {
                errors.Add(new Error(ErrorType.Business, fluentValidationError.ErrorCode, fluentValidationError.ErrorMessage));
            }

            if (errors.Any())
                return SimpleResult.Fail(errors);

            return SimpleResult.Success();
        }

        public async Task<SimpleResult> ValidateAsync(PublishProtocolDto protocol)
        {
            var isEmptyResult = IsEmpty(protocol);
            if (isEmptyResult.HasError)
                return isEmptyResult;

            var errors = new List<Error>();

            var validationResult = await _publishProtocolValidator.ValidateAsync(protocol);
            foreach (var fluentValidationError in validationResult.Errors)
            {
                errors.Add(new Error(ErrorType.Business, fluentValidationError.ErrorCode, fluentValidationError.ErrorMessage));
            }

            if (errors.Any())
                return SimpleResult.Fail(errors);

            return SimpleResult.Success();
        }

        private SimpleResult IsEmpty(ProtocolDto protocol)
        {
            if (protocol == null)
                return SimpleResult.Fail(new Error(ErrorType.Business, "NullError", "Item cannot be null"));

            return SimpleResult.Success();
        }

        private SimpleResult IsEmpty(PublishProtocolDto protocol)
        {
            if (protocol == null)
                return SimpleResult.Fail(new Error(ErrorType.Business, "NullError", "Item cannot be null"));

            return SimpleResult.Success();
        }
    }
}
