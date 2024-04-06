using System;

namespace NetTestX.VSIX.Options.Validation;

public class ProjectNameValidation : Validation<string>
{
    private const string FORBIDDEN_CHARS = "(/*-+_@&$#%){} ";
    
    public override bool Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;
        
        return value.AsSpan().IndexOfAny(FORBIDDEN_CHARS.AsSpan()) == -1;
    }
}
