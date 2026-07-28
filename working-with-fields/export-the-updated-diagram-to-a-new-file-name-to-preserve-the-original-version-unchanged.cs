using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load the original Visio diagram from file
        Diagram diagram = new Diagram("original.vsdx");

        // TODO: Apply any required modifications to the diagram here
        // e.g., diagram.Pages[0].Shapes.Add(...);

        // Save the updated diagram to a new file, leaving the original unchanged
        diagram.Save("updated.vsdx", SaveFileFormat.Vsdx);
    }
}
