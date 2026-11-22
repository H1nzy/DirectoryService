using System.Collections;
namespace Shared;

public class Failure : IEnumerable<Errors>
{
    private readonly List<Errors> _errors;
    public Failure(IEnumerable<Errors> errors)
    {
        _errors = [..errors];
    }
    public IEnumerator<Errors> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static implicit operator Failure(Errors[] errors) => new (errors);
    public static implicit operator Failure(Errors errors) => new ([errors]);
}