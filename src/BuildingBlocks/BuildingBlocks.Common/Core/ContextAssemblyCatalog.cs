using Carter;
using System.Reflection;

namespace BuildingBlocks.Common.Core;

public sealed class ContextAssemblyCatalog(Assembly assembly)
        : DependencyContextAssemblyCatalog
{
    private readonly Assembly[] _assemblies = [assembly];

    public override IReadOnlyCollection<Assembly> GetAssemblies()
    {
        return _assemblies;
    }
}
