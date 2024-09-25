using FluentValidation;
using Protocol.Publisher.Domain.Dtos;

namespace Protocol.Shared.Domain.Validators
{
    public class PublishProtocolDtoValidator : AbstractValidator<PublishProtocolDto>
    {
        public PublishProtocolDtoValidator()
        {
            RuleFor(x => x.ProtocolNumber)
                    .NotEmpty().WithMessage("O número do protocolo é obrigatório")
                    .GreaterThan(0).WithMessage("O número do protocolo deve ser maior que 0");

            RuleFor(x => x.ViaNumber)
                .GreaterThan(0).WithMessage("O número da via deve ser maior que 0");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório")
                .Length(11).WithMessage("O CPF deve ter 11 caracteres");

            RuleFor(x => x.Rg)
                .NotEmpty().WithMessage("O RG é obrigatório")
                .MinimumLength(7).WithMessage("O RG deve ter pelo menos 7 dígitos")
                .MaximumLength(11).WithMessage("O RG pode ter no máximo 11 dígitos");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório");

            RuleFor(x => x.Photo)
                .NotNull().WithMessage("A foto é obrigatória")
                .Must(file => file.Length > 0).WithMessage("A foto não pode estar vazia");
        }
    }
}
