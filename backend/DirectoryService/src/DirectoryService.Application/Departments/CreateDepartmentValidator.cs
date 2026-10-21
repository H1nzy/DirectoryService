namespace DirectoryService.Application;
using FluentValidation;
using DirectoryService.Contracts;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentDto>
{
     public CreateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя не может быть пустым")
            .MaximumLength(100).WithMessage("Имя не может быть длиннее 100 символов");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug не может быть пустым");
    }
}