namespace NetTestX.VSIX.Options.Validation;

/// <summary>
/// Represents a validation for an object
/// </summary>
public interface IValidation
{
    /// <summary>
    /// Validate whether the provided <paramref name="value"/> is in a valid state
    /// </summary>
    bool Validate(object value);
}
