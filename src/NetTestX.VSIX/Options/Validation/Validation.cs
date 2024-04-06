namespace NetTestX.VSIX.Options.Validation;

public abstract class Validation<T> : IValidation
{
    public abstract bool Validate(T value);

    public bool Validate(object value) => Validate((T)value);
}
