using FluentValidation;
using DirectoryService.Contracts;
namespace DirectoryService.Application;

public class UpdateLocationValidator : AbstractValidator<UpdateLocationDto>
{
    public UpdateLocationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Имя не может быть пустым")
                .WithErrorCode("location.name.required")
            .MaximumLength(100)
                .WithMessage("Имя не может быть длиннее 100 символов")
                .WithErrorCode("location.name.too.long");

        RuleFor(x => x.Address)
            .NotNull()
                .WithMessage("Адрес не может быть пустым")
                .WithErrorCode("location.address.required")
            .SetValidator(new AddressValidator());
    }
}