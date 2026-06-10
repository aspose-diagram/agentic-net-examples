using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Lock the diagram styles to prevent editing
            // DocumentSettings.ProtectStyles expects a BOOL enum value
            diagram.DocumentSettings.ProtectStyles = BOOL.True;

            // Save the protected diagram to a VSDX file
            // Use the correct overload with a SaveFileFormat enum
            diagram.Save("ProtectedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }