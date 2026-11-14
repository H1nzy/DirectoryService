namespace DirectoryService.Application;

using FluentValidation;
using DirectoryService.Contracts;

public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentDto>
{
    public UpdateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Имя не может быть пустым")
                .WithErrorCode("department.name.is.space")
            .MaximumLength(100)
                .WithMessage("Имя не может быть длиннее 100 символов")
                .WithErrorCode("department.name.too.long");

        RuleFor(x => x.Slug)
            .NotEmpty()
                .WithMessage("Slug не может быть пустым")
                .WithErrorCode("department.slug.is.space");
    }
}