using System;

namespace Cms.Shared.Helpers;

public static class TypeHelper
{
    public static string NameToLowerInvariant(this Type type)
    {
        return type.Name.ToLowerInvariant();
    }
}
