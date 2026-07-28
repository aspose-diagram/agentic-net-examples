using System.IO;
using System;
using System.Linq;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        // Create a new diagram (or load an existing one)
        Diagram diagram = new Diagram();

        // Add a CheckBox ActiveX control to the first page
        // Parameters: control type, pinX, pinY, width (in inches), height (in inches)
        long shapeId = diagram.Pages[0].AddActiveXControl(ControlType.CheckBox, 2.0, 2.0, 1.0, 0.5);

        // The actual control object is not directly exposed by Aspose.Diagram,
        // but we can reflect on the control class type itself.
        Type controlType = typeof(CheckBoxActiveXControl);

        // Retrieve all public instance properties that have a setter (i.e., are settable)
        var settableProperties = controlType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                            .Where(p => p.CanWrite);

        Console.WriteLine($"Settable properties of {controlType.Name}:");
        foreach (var prop in settableProperties)
        {
            Console.WriteLine($"- {prop.Name} ({prop.PropertyType.Name})");
        }
    }
}
