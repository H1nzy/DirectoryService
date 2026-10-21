using FluentValidation;
using DirectoryService.Contracts;
namespace DirectoryService.Application;

public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя не может быть пустым")
            .MaximumLength(100).WithMessage("Имя не может быть длиннее 100 символов");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Адрес не может быть пустым")
            .SetValidator(new AddressValidator());
    }
}

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Город не может быть пустым")
            .MaximumLength(100).WithMessage("Город не может быть длиннее 100 символов");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Улица не может быть пустой")
            .MaximumLength(100).WithMessage("Улица не может быть длиннее 100 символов");

        RuleFor(x => x.House)
            .NotEmpty().WithMessage("Дом не может быть пустым")
            .MaximumLength(10).WithMessage("Дом не может быть длиннее 10 символов");

        RuleFor(x => x.Apartment)
            .MaximumLength(10).WithMessage("Квартира не может быть длиннее 10 символов");
    }
}