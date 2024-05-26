namespace NetTestX.VSIX.Options.Validation;

/// <summary>
/// Base class for <see cref="IValidation"/> implementations
/// </summary>
public abstract class Validation<T> : IValidation
{
    public abstract bool Validate(T value);

    public bool Validate(object value) => Validate((T)value);
}
