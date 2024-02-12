using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generics.Constraints;

internal static class ConstraintHelper
{
    public static IEnumerable<IConstraint> GetTypeParameterConstraints(ITypeParameterSymbol typeParameter)
    {
        List<IConstraint> constraints = [..GetTypeParameterConstraintsWithoutTypes(typeParameter)];

        foreach (var constraintType in typeParameter.ConstraintTypes)
            constraints.Add(new TypeConstraint(constraintType));

        return constraints;
    }

    public static IEnumerable<IConstraint> GetTypeParameterConstraintsWithoutTypes(ITypeParameterSymbol typeParameter)
    {
        List<IConstraint> constraints = [];

        if (typeParameter.HasConstructorConstraint)
            constraints.Add(new ConstructorConstraint());

        if (typeParameter.HasUnmanagedTypeConstraint)
            constraints.Add(new UnmanagedTypeConstraint());

        if (typeParameter.HasValueTypeConstraint)
            constraints.Add(new ValueTypeConstraint());

        if (typeParameter.HasReferenceTypeConstraint)
            constraints.Add(new ReferenceTypeConstraint());

        return constraints;
    }
}
