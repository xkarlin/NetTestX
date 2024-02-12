using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Generics.Constraints;

internal class CompositeConstraint(IEnumerable<IConstraint> constraints) : IConstraint
{
    public bool IsSatisfiedBy(ITypeSymbol type) => constraints.All(x => x.IsSatisfiedBy(type));
}
