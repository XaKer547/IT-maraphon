using IT_maraphon.API.Validators.Models;

namespace IT_maraphon.API.Validators.Exceptions;

public sealed class ValidationException(List<ValidationError> errors) : Exception
{
    public List<ValidationError> Errors { get; } = errors;
}