using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load the source Visio diagram (any supported format, e.g., VSDX)
        Diagram diagram = new Diagram("input.vsdx");

        // Create save options for legacy VDX format
        DiagramSaveOptions saveOptions = new DiagramSaveOptions();
        saveOptions.SaveFormat = SaveFileFormat.Vdx;   // VDX = Visio XML format

        // Save the diagram. Hidden layers are not included in the VDX output,
        // so the resulting file contains only the visible layers.
        diagram.Save("output.vdx", saveOptions);
    }
}
