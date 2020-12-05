using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarCatalog.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsImplementingInterface(this Type obj, string interfaceName)
        {
            var x = obj.GetInterfaces().Any(i => i.IsGenericType && i.Name.Contains(interfaceName));
            return x;
        }
    }
}
