using Shared;
namespace DirectoryService.Application;
public  class DepartmentValidationException : BadRequestException
{
    public DepartmentValidationException(Errors[] errors): base(errors)
    {
        
    }
}