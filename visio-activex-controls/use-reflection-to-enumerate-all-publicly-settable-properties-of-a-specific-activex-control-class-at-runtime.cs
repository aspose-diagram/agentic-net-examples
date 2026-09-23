using System;
using System.Reflection;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // TODO: Replace MyActiveXControl with the actual ActiveX control class you want to inspect.
        Type activeXType = typeof(MyActiveXControl);

        // Retrieve all publicly settable properties.
        IEnumerable<PropertyInfo> settableProperties = GetPublicSettableProperties(activeXType);

        // Output the property names and their types.
        foreach (PropertyInfo prop in settableProperties)
        {
            Console.WriteLine($"{prop.Name} ({prop.PropertyType.Name})");
        }
    }

    /// <summary>
    /// Returns all instance properties of the specified type that have a public setter.
    /// </summary>
    /// <param name="type">The type to reflect over.</param>
    /// <returns>An enumerable of PropertyInfo objects representing publicly settable properties.</returns>
    static IEnumerable<PropertyInfo> GetPublicSettableProperties(Type type)
    {
        // Get all public instance properties.
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        foreach (PropertyInfo prop in properties)
        {
            // GetSetMethod(false) returns the setter only if it is public.
            MethodInfo setMethod = prop.GetSetMethod(false);
            if (setMethod != null && setMethod.IsPublic)
            {
                yield return prop;
            }
        }
    }
}

// ---------------------------------------------------------------------------
// Example ActiveX control class for demonstration purposes.
// Replace or remove this class when using a real ActiveX control.
// ---------------------------------------------------------------------------
public class MyActiveXControl
{
    // Publicly settable properties.
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsEnabled { get; set; }

    // Not publicly settable (private setter).
    public string Name { get; private set; }

    // Not public (private property).
    private int Hidden { get; set; }
}