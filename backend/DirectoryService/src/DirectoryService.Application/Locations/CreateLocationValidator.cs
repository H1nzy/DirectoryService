using FluentValidation;
using DirectoryService.Contracts;
namespace DirectoryService.Application;

public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Имя не может быть пустым")
            .WithErrorCode("location.name.is.space")
            .MaximumLength(100)
            .WithMessage("Имя не может быть длиннее 100 символов")
            .WithErrorCode("location.name.too.long");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Адрес не может быть пустым")
            .WithErrorCode("location.address.is.space")
            .SetValidator(new AddressValidator());
    }
}

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Город не может быть пустым")
            .WithErrorCode("location.address.city.is.space")
            .MaximumLength(100)
            .WithMessage("Город не может быть длиннее 100 символов")
            .WithErrorCode("location.address.city.too.long");

        RuleFor(x => x.Street)
            .NotEmpty()
                .WithMessage("Улица не может быть пустой")
                .WithErrorCode("location.address.street.is.space")
            .MaximumLength(100)
                .WithMessage("Улица не может быть длиннее 100 символов")
                .WithErrorCode("location.address.street.too.long");

        RuleFor(x => x.House)
            .NotEmpty()
                .WithMessage("Дом не может быть пустым")
                .WithErrorCode("location.address.house.is.space")
            .MaximumLength(10)
                .WithMessage("Дом не может быть длиннее 10 символов")
                .WithErrorCode("location.address.house.too.long");
        RuleFor(x => x.Apartment)
            .MaximumLength(10)
            .WithMessage("Квартира не может быть длиннее 10 символов")
            .WithErrorCode("location.address.apartment.too.long");
    }
}