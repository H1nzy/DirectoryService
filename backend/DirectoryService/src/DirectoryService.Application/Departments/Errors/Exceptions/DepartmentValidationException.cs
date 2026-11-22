using Shared;
namespace DirectoryService.Application;
public  class DepartmentValidationException : BadRequestException
{
    public DepartmentValidationException(Failure errors): base(errors)
    {
        
    }
}