using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load an existing Visio diagram from a file
        Diagram diagram = new Diagram("input.vsdx");

        // Prepare save options (e.g., save as VDX format)
        DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);

        // Save the diagram to a new file using the specified options
        diagram.Save("output.vdx", saveOptions);

        // Release unmanaged resources associated with the diagram
        diagram.Dispose();
    }
}
